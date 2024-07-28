using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alertnity.Program;

namespace Alertnity.PoliceApi
{
    public class Outcome
    {
        public string category { get; set; }
        public string location_type { get; set; }
        public Location location { get; set; }
        public string context { get; set; }
        public Outcome_Status outcome_status { get; set; }
        public string persistent_id { get; set; }
        public int id { get; set; }
        public string location_subtype { get; set; }
        public string month { get; set; }
    }
}
