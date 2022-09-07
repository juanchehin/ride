using rideDriver.Modelo;
using rideDriver.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rideDriver.Vistas.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Negociar : ContentPage
    {
    public Negociar(Mpedidos parametros)
      {
      InitializeComponent();
      BindingContext=new VMnegociar(Navigation,mapa,parametros);
      }
    }
}