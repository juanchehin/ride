using ride.Modelo;
using ride.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ride.VistaModelo
{
  public class VMadondevamos : BaseViewModel
    {
        #region VARIABLES
        List<GooglePlaceAutoCompletePrediction> _listadirecciones;
        private readonly IGoogleMapsApiService _googleMapsApi = new GoogleMapsApiService();
        string _txtorigen;
        string _txtdestino;
	    double ltOrigen = 0;
	    double lgOrigen = 0;
	    double ltDestino = 0;
	    double lgDestino = 0;
	    bool _selectorigen;
	    bool _selectdestino;
	    bool _enabledTxtorigen;
	    bool _enabledTxtdestino;
        bool _visibleListdirec;
        bool _fijarenmapa;
        bool _visibleOfertar;
        string _txttarifaEmerg;
        string _txttarifa;
        string _txtbuscador;
        Pin punto = new Pin();
        Xamarin.Forms.GoogleMaps.Map mapa;
        #endregion
        #region CONSTRUCTOR
        public VMadondevamos(INavigation navigation,Xamarin.Forms.GoogleMaps.Map mapareferencia)
        {
          Navigation=navigation;
          mapa = mapareferencia;
      mapa.PropertyChanged+=Mapa_PropertyChanged;
          EnabledTxtorigen =false;
          EnabledTxtdestino=false;
          Selectorigen=false;
          Selectdestino=false;
      Fijarenmapa=false;
        }

        #endregion
        #region OBJETOS
    public string Txtbuscador
      {
      get { return _txtbuscador; }
      set { SetValue(ref _txtbuscador,value); }
      }
    public string TxttarifaEmerg
      {
      get { return _txttarifaEmerg; }
      set { SetValue(ref _txttarifaEmerg,value); }
      }
    public string Txttarifa
      {
      get { return _txttarifa; }
      set { SetValue(ref _txttarifa,value); }
      }
    public bool VisibleOfertar
      {
      get { return _visibleOfertar; }
      set { SetValue(ref _visibleOfertar,value); }
      }
    public bool Fijarenmapa
      {
      get { return _fijarenmapa; }
      set { SetValue(ref _fijarenmapa,value); }
      }
    public bool VisibleListdirec
      {
      get { return _visibleListdirec; }
      set { SetValue(ref _visibleListdirec,value); }
      }
    public bool Selectorigen
      {
      get { return _selectorigen; }
      set { SetValue(ref _selectorigen,value); }

      }
    public bool Selectdestino
      {
      get { return _selectdestino; }
      set { SetValue(ref _selectdestino,value); }
      }
    public bool EnabledTxtdestino
      {
      get { return _enabledTxtdestino; }
      set { SetValue(ref _enabledTxtdestino,value); }
      }
    public bool EnabledTxtorigen
      {
      get { return _enabledTxtorigen; }
      set { SetValue(ref _enabledTxtorigen,value); }
      }
    public string Txtorigen
      {
      get { return _txtorigen; }
      set { SetValue(ref _txtorigen,value); }
      }
    public string Txtdestino
      {
      get { return _txtdestino; }
      set { SetValue(ref _txtdestino,value); }
      }
     public List<GooglePlaceAutoCompletePrediction> Listadirecciones
     {
        get { return _listadirecciones; }
        set { SetValue(ref _listadirecciones, value); }
     }
        #endregion
        #region PROCESOS
    private void Agregartarifa()
      {
      Txttarifa=TxttarifaEmerg;
      Cerrarofertar();
      }
    private void Cerrarofertar()
      {
      VisibleOfertar=false;
      }
    private void VerOfertar()
      {
      VisibleOfertar=true;
      }
    [Obsolete]
    private async void Mapa_PropertyChanged(object sender,System.ComponentModel.PropertyChangedEventArgs e)
      {
      if (Fijarenmapa==false)
        {
        return;
        }
      var m = (Xamarin.Forms.GoogleMaps.Map)sender;
      if (m.VisibleRegion!=null)
        {
        if (Selectorigen==true)
          {
          Txtorigen=await ObtenerDireccion(m.VisibleRegion.Center.Latitude,m.VisibleRegion.Center.Longitude);
          ltOrigen=m.VisibleRegion.Center.Latitude;
          lgOrigen=m.VisibleRegion.Center.Longitude;
          }
        if (Selectdestino==true)
          {
          Txtdestino=await ObtenerDireccion(m.VisibleRegion.Center.Latitude,m.VisibleRegion.Center.Longitude);
          ltDestino=m.VisibleRegion.Center.Latitude;
          lgDestino=m.VisibleRegion.Center.Longitude;
          }
        }
      }
    private async Task<string> ObtenerDireccion(double lt,double lg)
      {
      try
        {
        Geocoder geoCoder = new Geocoder();
        Position position = new Position(lt,lg);
        IEnumerable<string> direcciones = await geoCoder.GetAddressesForPositionAsync(position);
        string direccion = direcciones.FirstOrDefault();
        return direccion;
        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        return ex.Message;
        }
      }
    private void FijarenMapa()
      {
      Fijarenmapa=true;
      VisibleListdirec=false;
      EnabledTxtorigen=false;
      EnabledTxtdestino=false;
      }
    public async Task PinActual()
      {
      punto=new Pin()
        {
        Label="Tu ubicación",
        Type=PinType.Place,
        Icon=(Device.RuntimePlatform==Device.Android) ? BitmapDescriptorFactory.FromBundle("pinactual.png") : BitmapDescriptorFactory.FromView(new Image() { Source="pinactual.png",WidthRequest=64,HeightRequest=64 }),
        Position=new Position(0,0)

        };
      mapa.Pins.Add(punto);
      await GeolocalizacionActual();
      }
      public async Task GeolocalizacionActual()
      {
      try
        {
        var location = await Geolocation.GetLastKnownLocationAsync();
        if (location==null)
          {
          location=await Geolocation.GetLocationAsync(new GeolocationRequest
            {
            DesiredAccuracy=GeolocationAccuracy.High,
            Timeout=TimeSpan.FromSeconds(30)
            });

          }
        if (location!=null)
          {
          ltOrigen=location.Latitude;
          lgOrigen=location.Longitude;
          Txtorigen="Tu ubicación";
          var posicion = new Position(ltOrigen,lgOrigen);
          punto.Position=new Position(ltOrigen,lgOrigen);
          mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion,Distance.FromMeters(500)));
          }
        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        }
      }

        // ==================
        // Busca direcciones que ingresa el usuario desde la API de google
        // ==================
        private async Task Buscardirecciones(string buscador)
        {
            var places = await _googleMapsApi.ApiPlaces(buscador);  // Trae los lugares desde la API de google maps
            var placeResults = places.AutoCompletePlaces;
            Console.WriteLine("placeResults : ", placeResults);
            if (placeResults != null && placeResults.Count > 0)
            {
                Listadirecciones = new List<GooglePlaceAutoCompletePrediction>(placeResults);
            }
      }


    private async Task SeleccionarDireccion(GooglePlaceAutoCompletePrediction parametros)
      {
      var coordenadas = await _googleMapsApi.ApiPlacesDetails(parametros.PlaceId);
      if (coordenadas!=null)
        {
        if (Selectorigen==true)
          {
          ltOrigen=coordenadas.Latitude;
          lgOrigen=coordenadas.Longitude;
          Txtorigen=coordenadas.Name;
          }
        if (Selectdestino==true)
          {
          ltDestino=coordenadas.Latitude;
          lgDestino=coordenadas.Longitude;
          Txtdestino=coordenadas.Name;
          }
        VisibleListdirec=false;
        }
      }
    private void SelecionarOrigen()
      {
      Selectorigen=true;
      Selectdestino=false;
      EnabledTxtorigen=true;
      EnabledTxtdestino=false;
      VisibleListdirec=true;
            Fijarenmapa = false;
        }
    private void SelecionarDestino()
      {
      Selectorigen=false;
      Selectdestino=true;
      EnabledTxtorigen=false;
      EnabledTxtdestino=true;
      VisibleListdirec=true;
            Fijarenmapa = false;
        }

        #endregion
        #region COMANDOS
    public ICommand Agregartarifacommand => new Command(Agregartarifa);
    public ICommand Cerrarofertarcommand => new Command(Cerrarofertar);
    public ICommand Verofertarcommand => new Command(VerOfertar);
        public ICommand Fijarenmapacommand => new Command(FijarenMapa);
        public ICommand SelectDireccioncommand => new Command<GooglePlaceAutoCompletePrediction>(async (p) => await SeleccionarDireccion(p));
        public ICommand SelectOrigencommand => new Command(SelecionarOrigen);
        public ICommand SelectDestinocommand => new Command(SelecionarDestino);
        public ICommand Buscarcommand => new Command<string>(async (p) => await Buscardirecciones(p));
        #endregion
    }
}
