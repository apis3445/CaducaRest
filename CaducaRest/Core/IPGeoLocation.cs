using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CaducaRest.DTO;

namespace CaducaRest.Core;

/// <summary>
/// Funciones para obtener los datos de una io de IPGeoLocation
/// </summary>
public class IPGeoLocation : IPLocation
{
    private const string apiKey = "ddd67e98951b456f8e08c5a6d4bb4aa8";
    private const string apiUrl = "https://api.ipgeolocation.io/ipgeo";

    /// <summary>
    /// Obtiene los datos de la ciudad de una ip
    /// </summary>
    /// <param name="ip">Ip de donde se conectan</param>
    /// <returns></returns>
    public async Task<DatosIPDTO> ObtenerDatosPorIpAsync(string ip)
    {
        HttpClient client = new HttpClient();
        DatosIPDTO datosIP = new DatosIPDTO();

        var respuesta = await client.GetStringAsync($"{apiUrl}/?apiKey={apiKey}&ip={ip}&fields=geo,continent_name,organization");
        var datosGeo = JsonSerializer.Deserialize<IPGeoLocationDTO>(respuesta);
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