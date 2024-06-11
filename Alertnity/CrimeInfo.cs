using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity
{
    public class CrimeInfo
    {
        public DateTime Timestamp { get; set; }
        public DateTime ReportedDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string StreetName { get; set; }
        public string Postcode { get; set; }
        public CrimeCategory Category { get; set; }
        public string CrimeOutcome { get; set; }
        public string CrimeDescription { get; set; }
        public ReportingParty Party { get; set; }

    }
}
