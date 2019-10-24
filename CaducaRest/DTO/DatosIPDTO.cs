using System;
namespace CaducaRest.DTO
{
    public class DatosIPDTO
    {
        public string ip { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string continent { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string time_zone { get; set; }
        public string postal_code { get; set; }
        public string org { get; set; }
        public string asn { get; set; }
        public string subdivision { get; set; }
        public object subdivision2 { get; set; }
    }
}
