using ride.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adondevamos : ContentPage
    {
        public Adondevamos()
        {
            InitializeComponent();
            BindingContext = new VMadondevamos(Navigation, Mapa);
        }
    }
}