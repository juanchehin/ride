using ride.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DigitarCodigo : ContentPage
    {
        public DigitarCodigo(string codigo)
        {
            InitializeComponent();
            BindingContext=new VMdigitarcodigo(Navigation,codigo);
            }
        }
    }