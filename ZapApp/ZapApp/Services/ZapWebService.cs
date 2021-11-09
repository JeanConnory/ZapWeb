using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZapApp.Models;

namespace ZapApp.Services
{
    public class ZapWebService
    {
        private static HubConnection _connection;

        private static ZapWebService _instance;

        private ZapWebService()
        {
            _connection.On<bool, Usuario, string>("ReceberLogin", (sucesso, usuario, msg) =>
            {
                if (sucesso)
                {
                    UsuarioManager.SetUsuarioLogado(usuario);
                    Task.Run(async() => { await Entrar(usuario); });
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage = new ListagemUsuarios();
                    });
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var inicioPage = ((Inicio)App.Current.MainPage);
                        var loginPage = ((Login)inicioPage.Children[0]);
                        loginPage.SetMensagem(msg);
                    });
                }

                _connection.On<List<Usuario>>("ReceberListaUsuarios", (usuarios) =>
                {
                    if(App.Current.MainPage.GetType() == typeof(ListagemUsuarios))
                    {
                        var usuarioLogado = usuarios.Find(u => u.Id == UsuarioManager.GetUsuarioLogado().Id);
                        usuarios.Remove(usuarioLogado);

                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            ((ListagemUsuarioViewModel)App.Current.MainPage.BindingContext).Usuarios = usuarios;
                        });
                    }
                });

            });

            _connection.On<bool, Usuario, string>("ReceberCadastro", (sucesso, usuario, msg) =>
            {
                if(sucesso)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var inicioPage = ((Inicio)App.Current.MainPage);
                        var loginPage = ((Cadastro)inicioPage.Children[1]);
                        loginPage.SetMensagem(msg, false);
                    });
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        var inicioPage = ((Inicio)App.Current.MainPage);
                        var loginPage = ((Cadastro)inicioPage.Children[1]);
                        loginPage.SetMensagem(msg, true);
                    });
                }
            });
        }

        public static ZapWebService GetInstance()
        {
            if(_connection == null)
            {
                _connection = new HubConnectionBuilder().WithUrl("https://zapwebapiteste.azurewebsites.net/ZapWebHub").Build();
            }
            if(_connection.State == HubConnectionState.Disconnected)
            {
                _connection.StartAsync();
            }
            _connection.Closed += async (error) =>
            {
                await Task.Delay(5000);
                await _connection.StartAsync();
            };

            if (_instance == null)
            {
                _instance = new ZapWebService();
            }

            return _instance;
        }

        public async Task Login(Usuario usuario)
        {
            await _connection.InvokeAsync("Login", usuario);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            await _connection.InvokeAsync("Cadastrar", usuario);
        }

        public async Task Sair(Usuario usuario)
        {
            await _connection.InvokeAsync("DelConnectionIdDoUsuario", usuario);
        }

        public async Task Entrar(Usuario usuario)
        {
            await _connection.InvokeAsync("AddConnectionIdDoUsuario", usuario);
        }

        public async Task ObterListaUsuarios()
        {
            await _connection.InvokeAsync("ObterListaUsuarios");
        }
    }
}
