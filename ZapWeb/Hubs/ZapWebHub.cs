using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            }
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

                if(!connectionsId.Contains(connectionIdCurrent))
                {
                    connectionsId.Add(connectionIdCurrent);
                }
            }

            usuarioDB.ConnectionId = JsonConvert.SerializeObject(connectionsId);
            _banco.Usuarios.Update(usuarioDB);
            _banco.SaveChanges();
        }

        public async Task DelConnectionIdDoUsuario(Usuario usuario)
        {
            Usuario usuarioDB = await _banco.Usuarios.FindAsync(usuario.Id);

            if (usuarioDB.ConnectionId.Length > 0)
            {
                var connectionIdCurrent = Context.ConnectionId;

                List<string> connectionsId = JsonConvert.DeserializeObject<List<string>>(usuarioDB.ConnectionId);

                if (connectionsId.Contains(connectionIdCurrent))
                {
                    connectionsId.Remove(connectionIdCurrent);
                }

                usuarioDB.ConnectionId = JsonConvert.SerializeObject(connectionsId);
                _banco.Usuarios.Update(usuarioDB);
                _banco.SaveChanges();
            }
        }
    }
}
