using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZapWeb.Database;
using ZapWeb.Models;

namespace ZapWeb.Hubs
{
    public class ZapWebHub : Hub
    {
        private BancoContext _banco;

        public ZapWebHub(BancoContext banco)
        {
            _banco = banco;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var usuario = _banco.Usuarios.FirstOrDefault(a => a.ConnectionId.Contains(Context.ConnectionId));
            if (usuario != null)
            {
                await DelConnectionIdDoUsuario(usuario);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            bool IsExistUser = _banco.Usuarios.Where(a => a.Email == usuario.Email).Count() > 0;

            if (IsExistUser)
            {
                await Clients.Caller.SendAsync("ReceberCadastro", false, null, "E-mail já cadastrado!");
            }
            else
            {
                _banco.Usuarios.Add(usuario);
                _banco.SaveChanges();

                await Clients.Caller.SendAsync("ReceberCadastro", true, usuario, "Usuário cadastrado com sucesso!");
            }
        }

        public async Task Login(Usuario usuario)
        {
            var usuarioDb = _banco.Usuarios.FirstOrDefault(a => a.Email == usuario.Email && a.Senha == usuario.Senha);

            if (usuarioDb == null)
            {
                await Clients.Caller.SendAsync("ReceberLogin", false, null, "E-mail ou Senha não encontrados!");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceberLogin", true, usuarioDb, null);
                usuarioDb.IsOnline = true;
                _banco.Usuarios.Update(usuarioDb);
                _banco.SaveChanges();
                await NotificarMudancaNaListaUsuarios();
            }
        }

        public async Task Logout(Usuario usuario)
        {
            var usuarioDb = _banco.Usuarios.Find(usuario.Id);

            usuarioDb.IsOnline = false;
            _banco.Usuarios.Update(usuarioDb);
            _banco.SaveChanges();
            await DelConnectionIdDoUsuario(usuarioDb);
            await NotificarMudancaNaListaUsuarios();
        }

        public async Task AddConnectionIdDoUsuario(Usuario usuario)
        {
            Usuario usuarioDB = await _banco.Usuarios.FindAsync(usuario.Id);
            List<string> connectionsId = null;

            var connectionIdCurrent = Context.ConnectionId;

            if (usuarioDB.ConnectionId == null)
            {
                connectionsId = new List<string>();
                connectionsId.Add(connectionIdCurrent);
            }
            else
            {
                connectionsId = JsonConvert.DeserializeObject<List<string>>(usuarioDB.ConnectionId);

                if (!connectionsId.Contains(connectionIdCurrent))
                {
                    connectionsId.Add(connectionIdCurrent);
                }
            }

            usuarioDB.IsOnline = true;
            usuarioDB.ConnectionId = JsonConvert.SerializeObject(connectionsId);
            _banco.Usuarios.Update(usuarioDB);
            _banco.SaveChanges();
            await NotificarMudancaNaListaUsuarios();

            //Adicionar as ConnectionsId dos grupos de conversa desse usuário no SignalR
            var grupos = _banco.Grupos.Where(a => a.Usuarios.Contains(usuarioDB.Email));

            foreach (var connectionId in connectionsId)
            {
                foreach (var grupo in grupos)
                {
                    await Groups.AddToGroupAsync(connectionId, grupo.Nome);
                }
            }
        }

        public async Task DelConnectionIdDoUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                Usuario usuarioDB = await _banco.Usuarios.FindAsync(usuario.Id);
                List<string> connectionsId = null;

                if (usuarioDB.ConnectionId.Length > 0)
                {
                    var connectionIdCurrent = Context.ConnectionId;

                    connectionsId = JsonConvert.DeserializeObject<List<string>>(usuarioDB.ConnectionId);

                    if (connectionsId.Contains(connectionIdCurrent))
                    {
                        connectionsId.Remove(connectionIdCurrent);
                    }

                    usuarioDB.ConnectionId = JsonConvert.SerializeObject(connectionsId);

                    if (connectionsId.Count <= 0)
                    {
                        usuarioDB.IsOnline = false;
                    }

                    _banco.Usuarios.Update(usuarioDB);
                    _banco.SaveChanges();
                    await NotificarMudancaNaListaUsuarios();

                    //Remoção das ConnectionsId dos grupos de conversa desse usuário no SignalR
                    var grupos = _banco.Grupos.Where(a => a.Usuarios.Contains(usuarioDB.Email));

                    foreach (var connectionId in connectionsId)
                    {
                        foreach (var grupo in grupos)
                        {
                            await Groups.RemoveFromGroupAsync(connectionId, grupo.Nome);
                        }
                    }
                }
            }
        }

        public async Task NotificarMudancaNaListaUsuarios()
        {
            var usuarios = _banco.Usuarios.ToList();
            await Clients.All.SendAsync("ReceberListaUsuarios", usuarios);
        }

        public async Task ObterListaUsuarios()
        {
            var usuarios = _banco.Usuarios.ToList();
            await Clients.Caller.SendAsync("ReceberListaUsuarios", usuarios);
        }

        public async Task CriarOuAbrirGrupo(string emailUserUm, string emailUserDois)
        {
            string nomeGrupo = CriarNomeGrupo(emailUserUm, emailUserDois);
            Grupo grupo = _banco.Grupos.FirstOrDefault(a => a.Nome == nomeGrupo);

            if (grupo == null)
            {
                grupo = new Grupo();
                grupo.Nome = nomeGrupo;
                grupo.Usuarios = JsonConvert.SerializeObject(new List<string>()
                {
                    emailUserUm,
                    emailUserDois
                });

                _banco.Grupos.Add(grupo);
                await _banco.SaveChangesAsync();
            }

            //Adicionou as ConnectionsId dos grupos de conversa desse usuário no SignalR
            List<string> emails = JsonConvert.DeserializeObject<List<string>>(grupo.Usuarios);
            List<Usuario> usuarios = new List<Usuario>()
            {
                _banco.Usuarios.First(a => a.Email == emails[0]),
                _banco.Usuarios.First(a => a.Email == emails[1])
            };

            foreach (var usuario in usuarios)
            {
                var connectionsId = JsonConvert.DeserializeObject<List<string>>(usuario.ConnectionId);
                foreach (var connectionId in connectionsId)
                {
                    await Groups.AddToGroupAsync(connectionId, nomeGrupo);
                }
            }

            var mensagens = _banco.Mensagens.Where(a => a.NomeGrupo == nomeGrupo).OrderBy(a => a.DataCriacao).ToList();

            for (int i = 0; i < mensagens.Count; i++)
            {
                mensagens[i].Usuario = JsonConvert.DeserializeObject<Usuario>(mensagens[i].UsuarioJson);
            }

            await Clients.Caller.SendAsync("AbrirGrupo", nomeGrupo, mensagens);
        }

        public async Task EnviarMensagem(Usuario usuario, string msg, string nomeGrupo)
        {
            Grupo grupo = _banco.Grupos.FirstOrDefault(a => a.Nome == nomeGrupo);

            if (!grupo.Usuarios.Contains(usuario.Email))
            {
                throw new Exception("Usuário não pertence ao grupo!");
            }

            Mensagem mensagem = new Mensagem();
            mensagem.NomeGrupo = nomeGrupo;
            mensagem.Texto = msg;
            mensagem.DataCriacao = DateTime.Now;
            mensagem.UsuarioId = usuario.Id;
            mensagem.UsuarioJson = JsonConvert.SerializeObject(usuario);
            mensagem.Usuario = usuario;

            _banco.Mensagens.Add(mensagem);
            _banco.SaveChanges();

            await Clients.Group(nomeGrupo).SendAsync("ReceberMensagem", mensagem, nomeGrupo);
        }

        private string CriarNomeGrupo(string emailUserUm, string emailUserDois)
        {
            List<string> lista = new List<string>() { emailUserUm, emailUserDois };
            var listaOrdenada = lista.OrderBy(a => a).ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var item in listaOrdenada)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }
    }
}
