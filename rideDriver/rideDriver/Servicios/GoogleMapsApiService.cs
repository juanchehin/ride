﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ride.Conexiones;
using rideDriver.Modelo;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace rideDriver.Servicios
{
    public class GoogleMapsApiService : IGoogleMapsApiService
    {
        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
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

        public async Task<GooglePlace> ApiPlacesDetails(string placeId)
        {
            GooglePlace result = null;
            using(var httpClient = CreateClient())
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
