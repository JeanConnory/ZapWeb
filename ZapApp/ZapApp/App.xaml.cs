using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZapApp.Services;

namespace ZapApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Inicio();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Task.Run(async() => { await ZapWebService.GetInstance().Sair(UsuarioManager.GetUsuarioLogado()); });
        }

        protected override void OnResume()
        {
            Task.Run(async () => { await ZapWebService.GetInstance().Entrar(UsuarioManager.GetUsuarioLogado()); });
        }
    }
}