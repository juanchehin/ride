using ride.Datos;
using ride.Modelo;
using ride.Vistas.Registro;
using System;
using System.Collections.Generic;
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
        string _txtnumero;
        List<Mpaises> _listapaises;
        public GoogleUser _googleuserRecibe { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMcompletarreg(INavigation navigation,GoogleUser _googleUserTrae)
        {
            Navigation = navigation;
            _googleuserRecibe = _googleUserTrae;
            Mostrarpaises();
        }
        #endregion
        #region OBJETOS
        public string Txtnumero
        {
            get { return _txtnumero; }
            set { SetValue(ref _txtnumero, value); }
        }
        public List<Mpaises> Listapaises
        {
            get { return _listapaises; }
            set { SetValue(ref _listapaises, value); }
        }
        #endregion
        #region PROCESOS
        public async void Enviarsms()
        {
            try
            {
                #region Generar codigo aleatorio
                Random generador = new Random();
                String randomsms = generador.Next(0,9999).ToString("D4");

                #endregion
                var accountSid = "ACe031aa5545b40dba5952caa476bf7e45";
                var authToken = "AuthToken";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(new PhoneNumber(Txtnumero));
                messageOptions.MessagingServiceSid = "";
                messageOptions.Body = "Hola, usa este codigo para validar tu codigo en ride" + randomsms;

                var message = MessageResource.Create(messageOptions);
                await Navigation.PushAsync(new DigitarCodigo(randomsms));
                Console.WriteLine(message.Body);
            }
            catch (Exception ex)
            {
                // await DisplayAlert("Alerta", ex.Message); 
            }

        }
        public void Mostrarpaises()
        {
            var funcion = new Dpaises();
            Listapaises = funcion.Mostrarpaises();
        }
        #endregion
        #region COMANDOS
        public ICommand Siguientecommand => new Command(Enviarsms);
        // public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
