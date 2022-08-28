using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using ride.Modelo;

namespace ride.Servicios
  {
  public interface IGoogleMapsApiService
    {
    Task<GooglePlaceAutoCompleteResult> ApiPlaces(string text);
    Task<GooglePlace> ApiPlacesDetails(string placeId);
        Task<GoogleMatrix> Calculardistanciatiempo(string origen, string destino);


    }
}
