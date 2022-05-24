using ride.Modelo;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Xamarin.Forms;

namespace ride.VistaModelo
{
    internal class VMcompletarreg: BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        public GoogleUser _googleuserRecibe { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMcompletarreg(INavigation navigation,GoogleUser _googleUserTrae)
        {
            Navigation = navigation;
            _googleuserRecibe = _googleUserTrae;
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            // set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public async void Enviarsms()
        {
            try
            {
                var accountSid = "id";
                var authToken = "[AuthToken]";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(new PhoneNumber("NumDestino"));
                messageOptions.MessagingServiceSid = "serviceID";
                messageOptions.Body = "Hola, este es un mensaje desde el service-chehin";

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
            }
            catch (Exception ex)
            {
                // await DisplayAlert("Alerta", ex.Message); 
            }

        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand Siguientecommand => new Command(Enviarsms);
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
