using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rideDriver.Conexiones;
using rideDriver.Modelo;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using System.Data;
using System.IO;
using System.Net;

namespace rideDriver.Servicios
  {
  public class GoogleMapsApiService:IGoogleMapsApiService
    {
   
    private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
    private static GoogleMapsApiService _serviceClientInstance;
    private HttpClient client;
    public GoogleMapsApiService()
      {
      client=new HttpClient();
      client.BaseAddress=new Uri(ApiBaseAddress);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      }

    private HttpClient CreateClient()
      {
      var httpClient = new HttpClient
        {
        BaseAddress=new Uri(ApiBaseAddress)
        };
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      return httpClient;
      }
    public static GoogleMapsApiService ServiceClientInstance
      {
      get { if (_serviceClientInstance==null)
          _serviceClientInstance=new GoogleMapsApiService();
      return _serviceClientInstance;
        }
      }
    public async Task <GooglePlaceAutoCompleteResult> ApiPlaces(string text)
      {
      GooglePlaceAutoCompleteResult results = null;
      using (var httpclient= CreateClient())
        {
        var response = await httpclient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={Constantes.GoogleMapsApiKey}").ConfigureAwait(false);
        if(response.IsSuccessStatusCode)
          {
          var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          if(!string.IsNullOrWhiteSpace(json)&&json!="ERROR")
            {
            results=await Task.Run(()=>
            JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)).ConfigureAwait(false);
            
            }
          }
        }
      return results;
      }
    public async Task <GooglePlace> ApiPlacesDetails(string placeId)
      {
      GooglePlace result = null;
      using (var httpClient = CreateClient())
        {
        var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={Constantes.GoogleMapsApiKey}").ConfigureAwait(false);
        if(response.IsSuccessStatusCode)
          {
          var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          if(!string.IsNullOrWhiteSpace(json)&&json!="ERROR")
            {
            result=new GooglePlace(JObject.Parse(json));
            }
          }
        }
      return result;
      }
    #region MATRIX API
    public async Task<List<Position>>Cargarrutas(string porigen,string pdestino)
      {
      var googleDirection = await ServiceClientInstance.Direcciones(porigen,pdestino);
      if(googleDirection.Routes!=null&&googleDirection.Routes.Count>0)
        {
        var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
        return positions;
        }
      else
        {
        await App.Current.MainPage.DisplayAlert("Alerta","Sin rutas","Ok");
        return null;
        }
      }
    public async Task<GoogleDirection> Direcciones(string porigen,string pdestino)
      {
     
      GoogleDirection googleDirection = new GoogleDirection();

      var response = await client.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={porigen}&destination={pdestino}&key={Constantes.GoogleMapsApiKey}").ConfigureAwait(false);
      
        if(response.IsSuccessStatusCode)
        {
        var json= await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if(!string.IsNullOrWhiteSpace(json))
          {
          googleDirection=await Task.Run(()=>JsonConvert.DeserializeObject<GoogleDirection>(json)).ConfigureAwait(false);
          }
        }
      
        return googleDirection;


        }
    public async Task<Mgooglematrix> DibujarRutapuntoB(string porigen, string pdestino,Xamarin.Forms.GoogleMaps.Map map)
      {
      var parametrosmatrix = new Mgooglematrix();
      var pathcontent = await Cargarrutas(porigen,pdestino);
      map.Pins.Clear();
      map.Polylines.Clear();
      var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
      polyline.StrokeColor=Color.FromHex("#6B41CD");
      polyline.StrokeWidth=5;
      foreach(var p in pathcontent)
        {
        polyline.Positions.Add(p);
        }
    
      var pinOrigen = new Pin()
        {
        Label="Origen",
        Type=PinType.Place,
        Icon=(Device.RuntimePlatform==Device.Android) ? BitmapDescriptorFactory.FromBundle("puntoa.png") : BitmapDescriptorFactory.FromView(new Image() { Source="puntoa.png",WidthRequest=64,HeightRequest=64 }),
        Position=new Position(polyline.Positions.First().Latitude,polyline.Positions.First().Longitude),
        Tag="CirclePoint"
        };
      var pinDestino = new Pin()
        {
        Label="Destino",
        Type=PinType.Place,
        Icon=(Device.RuntimePlatform==Device.Android) ? BitmapDescriptorFactory.FromBundle("puntob.png") : BitmapDescriptorFactory.FromView(new Image() { Source="puntob.png",WidthRequest=32,HeightRequest=32 }),
        Position=new Position(polyline.Positions.Last().Latitude,polyline.Positions.Last().Longitude),
        Tag="CirclePoint"
        };
      map.Polylines.Add(polyline);
      map.Pins.Add(pinOrigen);
      map.Pins.Add(pinDestino);
      var _googleMatrix = new GoogleMatrix();
      _googleMatrix=await Calculardistanciatiempo(porigen,pdestino);
      parametrosmatrix.DistanciaValue=_googleMatrix.Rows[0].Elements[0].distance.value;
      string[] cadenaD = pdestino.Split(',');
      var ltdestino = cadenaD[0];
      var lgdestino = cadenaD[1];
      ltdestino=ltdestino.Replace(".",",");
      lgdestino=lgdestino.Replace(".",",");
      double destinoLat = Convert.ToDouble(ltdestino);
      double destinoLg = Convert.ToDouble(lgdestino);
      var posicion = new Position(destinoLat,destinoLg);
      var distancia = parametrosmatrix.DistanciaValue;
      var t = Task.Run(async delegate
         {
           await Task.Delay(100);
           });
      map.MoveToRegion(MapSpan.FromCenterAndRadius(posicion,Distance.FromMeters(distancia-distancia/3)));
      return parametrosmatrix;
      }
    public async Task<Mgooglematrix> DibujarRutapuntoA(string porigen,string pdestino,Xamarin.Forms.GoogleMaps.Map map)
      {
      var parametrosmatrix = new Mgooglematrix();
      var pathcontent = await Cargarrutas(porigen,pdestino);
     
      var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
      polyline.StrokeColor=Color.FromHex("#EF556E");
      polyline.StrokeWidth=5;
      foreach (var p in pathcontent)
        {
        polyline.Positions.Add(p);
        }

      var pinOrigen = new Pin()
        {
        Label="Conductor",
        Type=PinType.Place,
        Icon=(Device.RuntimePlatform==Device.Android) ? BitmapDescriptorFactory.FromBundle("punto0.png") : BitmapDescriptorFactory.FromView(new Image() { Source="punto0.png",WidthRequest=32,HeightRequest=32 }),
        Position=new Position(polyline.Positions.First().Latitude,polyline.Positions.First().Longitude),
        Tag="CirclePoint"
        };
      
      map.Polylines.Add(polyline);
      map.Pins.Add(pinOrigen);    
      var _googleMatrix = new GoogleMatrix();
      _googleMatrix=await Calculardistanciatiempo(porigen,pdestino);
      parametrosmatrix.DistanciaValue=_googleMatrix.Rows[0].Elements[0].distance.value;
      parametrosmatrix.Tiempo=_googleMatrix.Rows[0].Elements[0].duration.text;
      return parametrosmatrix;
      }
    public async Task<GoogleMatrix> Calculardistanciatiempo(string origen, string destino)
      {
      GoogleMatrix result= null;
      using (var httpClient = CreateClient())
        {
        var response = await httpClient.GetAsync($"api/distancematrix/json?origins={origen}&destinations={destino}&key={Constantes.GoogleMapsApiKey}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
          {
          var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
          if(!string.IsNullOrWhiteSpace(json)&&json!="ERROR")
            {
            result = await Task.Run(()=>
            JsonConvert.DeserializeObject<GoogleMatrix>(json)).ConfigureAwait(false);
            }
          }
        }
      return result;
      }
    #endregion
    }
  }
