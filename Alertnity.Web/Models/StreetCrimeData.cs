namespace Alertnity.Web.Models
{
    public class StreetCrimeData
    {
        public string? StreetName { get; set; }
        public int CrimeCount { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Crimes { get; set; }
        public Dictionary<string, int>? CrimeCountsByType { get; set; }
    }
}
