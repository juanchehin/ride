using ride.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empezar : ContentPage
    {
        public Empezar()
        {
            InitializeComponent();
            BindingContext = new VMempezar(Navigation);
        }
    }
}