using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using rideDriver.Modelo;

namespace rideDriver.Servicios
  {
  public interface IGoogleMapsApiService
    {
    Task<GooglePlaceAutoCompleteResult> ApiPlaces(string text);
    Task<GooglePlace> ApiPlacesDetails(string placeId);
    Task<GoogleMatrix> Calculardistanciatiempo(string origen,string destino);
    Task<Mgooglematrix> DibujarRutapuntoB(string origen,string destino,Xamarin.Forms.GoogleMaps.Map map);
    Task<Mgooglematrix> DibujarRutapuntoA(string origen,string destino,Xamarin.Forms.GoogleMaps.Map map);


    }
}
