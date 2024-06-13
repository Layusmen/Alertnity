using System;


namespace Alertnity
{
    public class CrimeInfo
    {
        public DateTime ReportedDate { get; set; }
        public CrimeCategory Category { get; set; }
        public string CrimeOutcome { get; set; }
        public string CrimeDescription { get; set; }
        public ReportingParty Party { get; set; }
        public string CrimeID { get; set; }
    }
}
