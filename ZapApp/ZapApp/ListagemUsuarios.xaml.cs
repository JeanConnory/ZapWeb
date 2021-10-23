using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZapApp.Models;

namespace ZapApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemUsuarios : ContentPage
    {
        public ListagemUsuarios()
        {
            InitializeComponent();
        }
    }
    public class ListagemUsuarioViewModel
    {
        public List<Usuario> Usuarios { get; set; }

        public ListagemUsuarioViewModel()
        {
            Usuarios = MockUsuarios();
        }

        private List<Usuario> MockUsuarios()
        {
            return new List<Usuario>()
                {
                    new Usuario { Nome = "Jean Michael", Email = "jean@mail.com", Senha = "123456", IsOnline = false },
                    new Usuario { Nome = "Jessica Jamilly", Email = "jessica@mail.com", Senha = "654321", IsOnline = false },
                    new Usuario { Nome = "Dunha McGregor", Email = "dunha@mail.com", Senha = "987654", IsOnline = false }
                };
        }
    }
}