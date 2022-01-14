using System;
using System.Text.Json.Serialization;

namespace CaducaRest.DTO
{
    /// <summary>
    /// Datos regresados por el servicio IPGeoLocation
    /// </summary>
    public class IPGeoLocationDTO
    {

        /// <summary>
        /// Ip
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Country Code 2
        /// </summary>
        [JsonPropertyName("country_code2")]
        public string CountryCode2 { get; set; }

        /// <summary>
        /// Country Code 3
        /// </summary>
        [JsonPropertyName("country_code3")]
        public string CountryCode3 { get; set; }

        /// <summary>
        /// Nombre del país
        /// </summary>
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [JsonPropertyName("state_prov")]
        public string StateProv { get; set; }

        /// <summary>
        /// Distrito
        /// </summary>
        [JsonPropertyName("district")]
        public string District { get; set; }

        /// <summary>
        /// Ciudad
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Código Postal
        /// </summary>
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// Latitud
        /// </summary>
        [JsonPropertyName("latitude")]
        public Double Latitude { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        [JsonPropertyName("longitude")]
        public Double Longitude { get; set; }

        /// <summary>
        /// Nombre del continent
        /// </summary>
        [JsonPropertyName("continent_name")]
        public string ContinentName { get; set; }

        /// <summary>
        /// Organización
        /// </summary>
        [JsonPropertyName("organization")]
        public string Organization { get; set; }

    }
}
