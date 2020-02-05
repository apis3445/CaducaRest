namespace CaducaRest.DTO
{
    /// <summary>
    /// Dastos de localización de una ip
    /// </summary>
    public class DatosIPDTO
    {
        /// <summary>
        /// Ip 
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// País
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Código del país
        /// </summary>
        public string country_code { get; set; }
        /// <summary>
        /// Ciudad
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Continente
        /// </summary>
        public string continent { get; set; }
        /// <summary>
        /// Latitud
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// Longitud
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// Zona horaria
        /// </summary>
        public string time_zone { get; set; }
        /// <summary>
        /// Código Postal
        /// </summary>
        public string postal_code { get; set; }
        /// <summary>
        /// Organizatión
        /// </summary>
        public string org { get; set; }
        public string asn { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public string subdivision { get; set; }
        /// <summary>
        /// Subdivisión 2
        /// </summary>
        public object subdivision2 { get; set; }
    }
}
