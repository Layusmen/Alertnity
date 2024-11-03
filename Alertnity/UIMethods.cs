using Alertnity.PostcodeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Response is null or nothing contained in the API.");
        }

    }
}
