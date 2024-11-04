using Alertnity.PostcodeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Alertnity
{
    public class UIMethods
    {
        public static void DisplayPostCodeConverterResponse(PostcodeConverter converter)
        {
            Console.WriteLine("Longitude: " + converter.Longitude);
            Console.WriteLine("Latitude: " + converter.Latitude);
            Console.WriteLine("............................");
        }

        public static void DisplayNullResponse()
        {
            Console.WriteLine("Response is null.");
        }

        public static void UrlPrint(string crimeUrl)
        {
             Console.WriteLine($"Checking data for: {crimeUrl}");
        }

        public static void MonthPrint(string month)
        {
            Console.WriteLine($"No data returned for {month}, continuing to next month.");
        }
        public static void SkippingFutureDatePrint(DateTime insertDate)
        {
           Console.WriteLine($"Skipping future date: {insertDate.ToString("yyyy-MM")}");
        }

        /// <summary>
        /// Month Error Log
        /// </summary>
        /// <param name="month">The month in which the error occured</param>
        /// <param name="ex">Exception for the error details.</param>
        public static void MonthErrorLog(string month, Exception ex)
        {
            Console.WriteLine($"Error fetching data for {month}: {ex.Message}");
        }

        /// <summary>
        /// Date Range Invalid Print
        /// </summary>
        public static void InvalidDateRangePrint()
        {
            Console.WriteLine("Invalid date range.");
        }
    }
}
