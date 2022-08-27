using ride.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ride.VistaModelo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride.Vistas.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Esperarofertas : ContentPage
    {
        public Esperarofertas()
        {
            InitializeComponent();
            BindingContext = new VMesperarofertas(Navigation);
        }
    }
}