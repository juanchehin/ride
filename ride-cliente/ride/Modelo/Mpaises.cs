using ride.VistaModelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ride.Modelo
{
    public class Mpaises: BaseViewModel
    {
        private string _iconourl;
        private string _pais;
        private string _codigopais;
        public string Iconourl
        {
            get { return _iconourl; }
            set { SetValue(ref _iconourl, value); }
        }
        public string Pais
        {
            get { return _pais; }
            set { SetValue(ref _pais, value); }

        }
        public string CodigoPais
        {
            get { return _codigopais; }
            set { SetValue(ref _codigopais, value); }

        }

        public string Codigopais { get; internal set; }
    }
}
