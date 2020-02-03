using System;
using System.Net.Http;
using System.Threading.Tasks;
using CaducaRest.DTO;
using Newtonsoft.Json;

namespace CaducaRest.Core
{
    public class IPGeoLocation : IPLocation
    {
        private const string apiKey = "ddd67e98951b456f8e08c5a6d4bb4aa8";
        private const string apiUrl = "https://api.ipgeolocation.io/ipgeo";
        public async Task<DatosIPDTO> ObtenerDatosPorIpAsync(string ip)
        {
            HttpClient client = new HttpClient();
            DatosIPDTO datosIP = new DatosIPDTO(); 

            var respuesta = await client.GetStringAsync($"{apiUrl}/apiKey={apiKey}&ip={ip}&fields=geo,continent_name,organization,time_zone.name");
            var datosGeo = JsonConvert.DeserializeObject<IPGeoLocationDTO>(respuesta);
            datosIP.city = datosGeo.City;
            datosIP.country = datosGeo.CountryName;
            datosIP.continent = datosGeo.ContinentName;
            datosIP.country_code = datosGeo.CountryCode2;
            datosIP.latitude = datosGeo.Latitude;
            datosIP.longitude = datosGeo.Longitude;
            datosIP.postal_code = datosGeo.Zipcode;
            datosIP.subdivision = datosGeo.StateProv;
            datosIP.subdivision2 = datosGeo.District;
            datosIP.org = datosGeo.Organization;
            return datosIP;
        }
    }
}
