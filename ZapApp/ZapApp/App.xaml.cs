using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZapApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ListagemUsuarios();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
