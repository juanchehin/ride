using ride.Modelo;
using ride.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Reutilizables
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listapaises : ContentPage
    {
        public Listapaises()
        {
            var parametros = new GoogleUser();
            parametros.Apellido = "-";
            parametros.Name = "-";
            parametros.Email = "-";
            InitializeComponent();
            BindingContext = new VMcompletarreg(Navigation,parametros);
        }
    }
}