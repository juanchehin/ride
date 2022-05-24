using ride.Modelo;
using ride.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletarRegistro : ContentPage
    {
            public CompletarRegistro(GoogleUser parametros)
            {
                InitializeComponent();
            BindingContext = new VMcrearcuenta(Navigation);
            // BindingContext = new VMcrearcuenta(Navigation, parametros);
        }
    }
}