﻿using rideDriver.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rideDriver.Vistas.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pedidos : ContentPage
    {
        public Pedidos()
        {
            InitializeComponent();
            BindingContext=new VMpedidos(Navigation);
        }
    }
}