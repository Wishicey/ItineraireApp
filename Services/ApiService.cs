using ItineraireApp.Constants;
using ItineraireApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ItineraireApp.Services
{
    class ApiService
    {
        //Url loopback ~127.0.0.1:55462
        public const string sUrlBase = "http://10.0.2.2:55462";
        public static async Task<List<VilleTypeResponse>> GetVillesAsync()
        {
            try
            {
                HttpClient icClient = new HttpClient();
                string sUrl = sUrlBase + "/api/Villes";
                var icResponse = await icClient.GetAsync(sUrl).ConfigureAwait(continueOnCapturedContext:false);
                icResponse.EnsureSuccessStatusCode();
                string sResponseAsString = await icResponse.Content.ReadAsStringAsync();
                List<VilleTypeResponse> cListVille = System.Text.Json.JsonSerializer.Deserialize<List<VilleTypeResponse>>(sResponseAsString);
                return cListVille;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ItineraireTypeResponse>> GetItineraireIdWithVilleAsync(HttpClient icClient, int iIdVille)
        {
            try
            {
                string sUrl = sUrlBase + "/api/Ville/" + iIdVille;
                var icResponse = await icClient.GetAsync(sUrl).ConfigureAwait(continueOnCapturedContext: false);
                icResponse.EnsureSuccessStatusCode();
                string sResponseAsString = await icResponse.Content.ReadAsStringAsync();
                List<ItineraireTypeResponse> cItineraire = System.Text.Json.JsonSerializer.Deserialize<List<ItineraireTypeResponse>>(sResponseAsString);
                return cItineraire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ItineraireTypeResponse>> GetItineraireWithVilleAsync(int iIdVille)
        {
            try
            {
                HttpClient icClient = new HttpClient();
                List<ItineraireTypeResponse> cItineraire = GetItineraireIdWithVilleAsync(icClient, iIdVille).Result;
                List<ItineraireTypeResponse> cListItineraires = new List<ItineraireTypeResponse>();
                foreach (ItineraireTypeResponse icItineraire in cItineraire)
                {
                    string sUrl = sUrlBase + "/api/Itineraires/" + icItineraire.idItineraire;
                    var icResponse = await icClient.GetAsync(sUrl).ConfigureAwait(continueOnCapturedContext: false);
                    icResponse.EnsureSuccessStatusCode();
                    string sResponseAsString = await icResponse.Content.ReadAsStringAsync();
                    foreach (ItineraireTypeResponse icItineraireUnique in System.Text.Json.JsonSerializer.Deserialize<List<ItineraireTypeResponse>>(sResponseAsString))
                    {
                        cListItineraires.Add(icItineraireUnique);
                    };
                }
                return cListItineraires;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static async Task<GoogleMapsRoute.Root> GetPolylineMapsDirections(List<string> cListVillesName) 
        {
            try
            {
                HttpClient icClient = new HttpClient();
                string sUrlGoogle = "https://maps.googleapis.com/maps/api/directions/json?origin=Paris,FR&destination=Marseille,FR&waypoints=";
                for(int i = 0; i < 2 ;i++)
                {
                    sUrlGoogle += cListVillesName[i + 1] + ",FR|";
                }
                //On supprime le dernier caractère
                sUrlGoogle = sUrlGoogle.Remove(sUrlGoogle.Length - 1);
                sUrlGoogle += "&key=";
                var icResponse = await icClient.GetAsync(sUrlGoogle).ConfigureAwait(continueOnCapturedContext: false);
                icResponse.EnsureSuccessStatusCode();
                string sResponseAsString = await icResponse.Content.ReadAsStringAsync();
                Polyline icPolyline = new Polyline();
                GoogleMapsRoute.Root icGoogleMaps = System.Text.Json.JsonSerializer.Deserialize<GoogleMapsRoute.Root>(sResponseAsString);
                
                return icGoogleMaps;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
