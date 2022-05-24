using ride.Modelo;
using ride.Vistas.Registro;
using System.Windows.Input;
using Xamarin.Forms;
using static ride.Modelo.GoogleUser;

namespace ride.VistaModelo
{
    public class VMcrearcuenta : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        private readonly IGoogleManager _googleManager;
        GoogleUser googleuserObtiene = new GoogleUser();
        #endregion
        #region CONSTRUCTOR
        public VMcrearcuenta(INavigation navigation)
        {
            Navigation = navigation;
            _googleManager = DependencyService.Get<IGoogleManager>();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS        
        public void LoguearseConGmail()
        {
            _googleManager.Login(OnLoginComplete);
        }
        public async void OnLoginComplete(GoogleUser googleuserTrae, string message)
        {
            if (googleuserTrae != null)
            {
                googleuserObtiene = googleuserTrae;
                string[] cadena = googleuserObtiene.Name.Split(' ');
                googleuserObtiene.Name = cadena[0];
                googleuserObtiene.Apellido = cadena[1];
                await Navigation.PushAsync(new CompletarRegistro(googleuserObtiene));
            }
            else
            {
                await DisplayAlert("Message", message, "OK");
            }
        }

        #endregion
        #region COMANDOS
        public ICommand Gmailcommand => new Command(LoguearseConGmail);
        #endregion
    }
}
