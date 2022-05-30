using Rg.Plugins.Popup.Services;
using ride.Datos;
using ride.Modelo;
using ride.Vistas.Registro;
using ride.Vistas.Reutilizables;
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
        Mpaises _selectPais;
        Mpaises _selectPaisDefault;
        public GoogleUser _googleuserRecibe { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMcompletarreg(INavigation navigation,GoogleUser _googleUserTrae)
        {
            Navigation = navigation;
            _googleuserRecibe = _googleUserTrae;
            ObtenerDataPaisXpais();
        }
        #endregion
        #region OBJETOS
        public Mpaises SelectPais
            {
            get { return _selectPais; }
            set { SetValue(ref _selectPais,value); }
            }
        public Mpaises SelectPaisDefault
            {
            get { return _selectPaisDefault; }
            set { SetValue(ref _selectPaisDefault,value); }
            }
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
        private void ObtenerDataPaisXpais()
            {
            var funcion = new Dpaises();
            SelectPaisDefault=funcion.MostrarpaisesXnombre("Argentina");
            SelectPais=funcion.MostrarpaisesXnombre("Argentina");
            }
        private void Irlistapaises()
            {
            var popup = new Listapaises();
            popup.BindingContext=this;
            Mostrarpaises();
            PopupNavigation.Instance.PushAsync(popup);
            }
        private void SeleccionarPais(Mpaises parametros)
            {
            SelectPais=parametros;
            }
        private void Confirmarpais()
            {
            SelectPaisDefault=SelectPais;
            PopupNavigation.Instance.PopAsync();
            }
        private void Cancelar()
            {
            PopupNavigation.Instance.PopAsync();
            }
        #endregion
        #region COMANDOS
        public ICommand Cancelarcommand => new Command(Cancelar);
        public ICommand Confirmarcommand => new Command(Confirmarpais);
        public ICommand SelectPaiscommand => new Command<Mpaises>(SeleccionarPais);
        public ICommand Irpaisescommand => new Command(Irlistapaises);
        public ICommand Siguientecommand => new Command(Enviarsms);
        // public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
