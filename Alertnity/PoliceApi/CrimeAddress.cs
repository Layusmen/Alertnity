using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity.PoliceApi
{
    public class CrimeAddress
    {
        public string latitude { get; set; }
        public StreetName streetName { get; set; }
        public string longitude { get; set; }
    }
}
