using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity.ArchiveDataAnalysis
{
    public static class GeoCalculator
    {
        public const double EarthRadiusMiles = 3958.8; 
        public static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Degrees to radians conversions
            double DegreesToRadians(double degrees) => degrees * (Math.PI / 180.0);

            //Differences calculations
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            // Convert latitude degrees to radians
            double rlat1 = DegreesToRadians(lat1);
            double rlat2 = DegreesToRadians(lat2);

            // Haversine Formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(rlat1) * Math.Cos(rlat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));

            return EarthRadiusMiles * c * 1609.34;
        }
    }
    
}

