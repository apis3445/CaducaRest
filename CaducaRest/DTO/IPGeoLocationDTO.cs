using System;
using Newtonsoft.Json;

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
        [JsonProperty("ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Country Code 2
        /// </summary>
        [JsonProperty("country_code2")]
        public string CountryCode2 { get; set; }

        /// <summary>
        /// Country Code 3
        /// </summary>
        [JsonProperty("country_code3")]
        public string CountryCode3 { get; set; }

        /// <summary>
        /// Nombre del país
        /// </summary>
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [JsonProperty("state_prov")]
        public string StateProv { get; set; }

        /// <summary>
        /// Distrito
        /// </summary>
        [JsonProperty("district")]
        public string District { get; set; }

        /// <summary>
        /// Ciudad
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Código Postal
        /// </summary>
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// Latitud
        /// </summary>
        [JsonProperty("latitude")]
        public Double Latitude { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        [JsonProperty("longitude")]
        public Double Longitude { get; set; }

        /// <summary>
        /// Nombre del continent
        /// </summary>
        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }

        /// <summary>
        /// Organización
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

    }
}
