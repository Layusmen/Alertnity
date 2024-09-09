using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alertnity.ArchiveDataAnalysis.ArchiveCrimeByRadius;

namespace Alertnity.ArchiveDataAnalysis
{
    public class CrimeRecordMap : ClassMap<CrimeRecord>
    {
        public CrimeRecordMap()
        {
            //Map(m => m.Latitude).Name("Latitude");
            //Map(m => m.Longitude).Name("Longitude");
            //Map(m => m.CrimeType).Name("Crime type");
            //Map(m => m.LSOAName).Name("LSOA name");

            Map(m => m.Latitude).Name("Latitude").TypeConverter<SafeDoubleConverter>();
            Map(m => m.Longitude).Name("Longitude").TypeConverter<SafeDoubleConverter>();
            Map(m => m.CrimeType).Name("Crime type");
            Map(m => m.LSOAName).Name("LSOA name");
        }
    }
}