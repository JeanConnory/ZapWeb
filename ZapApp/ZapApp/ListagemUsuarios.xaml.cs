using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZapApp.Models;
using ZapApp.Services;

namespace ZapApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemUsuarios : ContentPage
    {
        public ListagemUsuarios()
        {
            InitializeComponent();

            Sair.Clicked += async (sender, args) =>
            {
                UsuarioManager.DelUsuarioLogado();

                await ZapWebService.GetInstance().Sair(UsuarioManager.GetUsuarioLogado());
                UsuarioManager.DelUsuarioLogado();

                App.Current.MainPage = new Inicio();
            };

            Task.Run(async() => { await ZapWebService.GetInstance().ObterListaUsuarios(); });
        }
    }

    public class ListagemUsuarioViewModel : INotifyPropertyChanged
    {
        private List<Usuario> _usuarios;

        public List<Usuario> Usuarios
        {
            get
            {
                return _usuarios;
            }
            set
            {
                _usuarios = value;
                NotifyPropertyChanged(nameof(Usuarios));
            }
        }

        public ListagemUsuarioViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}