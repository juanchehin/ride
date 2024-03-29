﻿using ride.Vistas.MenuPrincipal;
using ride.Vistas.Navegacion;
using ride.Vistas.Registro;
using ride.Vistas.Reutilizables;
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

            MainPage = new NavigationPage(new Adondevamos()); 
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
