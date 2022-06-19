using ride.Modelo;
using ride.Servicios;
using System;
using System.Collections.Generic;
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
          EnabledTxtorigen =false;
          EnabledTxtdestino=false;
          Selectorigen=false;
          Selectdestino=false;
        }

        #endregion
        #region OBJETOS
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
      //Fijarenmapa=false;
      }
    private void SelecionarDestino()
      {
      Selectorigen=false;
      Selectdestino=true;
      EnabledTxtorigen=false;
      EnabledTxtdestino=true;
      VisibleListdirec=true;
      //Fijarenmapa=false;
      }

        #endregion
        #region COMANDOS
        public ICommand SelectDireccioncommand => new Command<GooglePlaceAutoCompletePrediction>(async (p) => await SeleccionarDireccion(p));
        public ICommand SelectOrigencommand => new Command(SelecionarOrigen);
        public ICommand SelectDestinocommand => new Command(SelecionarDestino);
        public ICommand Buscarcommand => new Command<string>(async (p) => await Buscardirecciones(p));
        #endregion
    }
}
