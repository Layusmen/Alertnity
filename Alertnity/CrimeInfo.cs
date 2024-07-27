using System;


namespace Alertnity
{
    public class CrimeInfo
    {
        public DateTime? ReportedDate { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Category { get; set; }
        public string CrimeOutcome { get; set; }
        public string? CrimeDescription { get; set; }
        public string ReportingParty { get; set; }
        public string? CrimeID { get; set; }
    }
}
