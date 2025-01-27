using Alertnity.PoliceApi;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alertnity.ArchiveDataAnalysis.ArchiveCrimeByRadius;
using CsvHelper.TypeConversion;


namespace Alertnity.ArchiveDataAnalysis
{

    public class CrimeRecord
    {
            [Name("Latitude"), TypeConverter(typeof(SafeDoubleConverter))]
            public double Latitude { get; set; }

            [Name("Longitude"), TypeConverter(typeof(SafeDoubleConverter))]
            public double Longitude { get; set; }
            public Location location { get; set; }  
            [Name("Crime type")]
            public string CrimeType { get; set; }

            [Name("LSOA name")]
            public string LSOAName { get; set; }

            [Name("Location")]
            public string Street { get; set; }

            [Name("Last outcome category")]
            public string outcome_status { get; set; }
        
            [Name("Crime ID")] 
            public string CrimeID { get; set; }

            [Name("date")]
            public string month { get; set; }
    }
    public class SafeDoubleConverter : DoubleConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // for empty field return 0.0/default value)
            if (string.IsNullOrWhiteSpace(text))
            {
                return Constants.INVALID_VALUE;
            }

            // convert the text to double
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
