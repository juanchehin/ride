using ride.Modelo;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
