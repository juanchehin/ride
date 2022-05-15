using ride.Vistas.MenuPrincipal;
using ride.Vistas.Registro;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ride
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Vmenuprincipal();
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
