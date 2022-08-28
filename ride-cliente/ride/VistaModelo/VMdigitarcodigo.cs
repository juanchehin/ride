using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ride.VistaModelo;
using ride.Vistas.MenuPrincipal;
using Xamarin.Forms;

namespace ride.VistaModelo
{
    public class VMdigitarcodigo: BaseViewModel
    {
        #region VARIABLES
        string _txtcodigo;
        string mensajerecibido;
        #endregion
        #region CONSTRUCTOR
        public VMdigitarcodigo(INavigation navigation,string codigo)
            {
            Navigation=navigation;
            mensajerecibido=codigo;
        }
        #endregion
        #region OBJETOS
        public string Txtcodigo
            {
            get { return _txtcodigo; }
            set { SetValue(ref _txtcodigo,value); }
            }
        #endregion
        #region PROCESOS
        public async void Validarcodigo()
            {
            if(Txtcodigo==mensajerecibido)
                {
                Creararchivo();
                await Navigation.PushAsync(new Vmenuprincipal());
                }
            else
                {
                await DisplayAlert("Alerta","Código Incorrecto","OK");
                }
            }
        public void Creararchivo()
            {
            var ruta=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"auth.txt");
            StreamWriter sw;
            string estado = "1";
            try
                {
                if(File.Exists(ruta)==false)
                    {
                    sw=File.CreateText(ruta);
                    sw.WriteLine(estado);
                    sw.Flush();
                    sw.Close();
                    }
                else
                    {
                    File.Delete(ruta);
                    sw=File.CreateText(ruta);
                    sw.WriteLine(estado);
                    sw.Flush();
                    sw.Close();
                    }
                }
            catch (Exception)
                {

                throw;
                }

            }

        #endregion
        #region COMANDOS
        public ICommand Validarcommand => new Command(Validarcodigo);
       
        #endregion
    }
}
