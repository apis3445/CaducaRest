using System;
using Newtonsoft.Json;

namespace CaducaRest.DTO
{
    public class IPGeoLocationDTO
    {

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("country_code2")]
        public string CountryCode2 { get; set; }

        [JsonProperty("country_code3")]
        public string CountryCode3 { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("state_prov")]
        public string StateProv { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("latitude")]
        public Double Latitude { get; set; }

        [JsonProperty("longitude")]
        public Double Longitude { get; set; }

        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

    }
}
