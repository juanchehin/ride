using rideDriver.Vistas.Navegacion;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rideDriver
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pedidos());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
