using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity.ArchiveDataAnalysis
{

    public class CrimeRecord
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CrimeType { get; set; }
        public string LSOAName { get; set; } // New property for LSOA name
    }

}
