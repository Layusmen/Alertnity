using System;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Alertnity.PoliceApi;
using Alertnity.PostcodeApi;
using CsvHelper;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Analysis;
using Alertnity.ArchiveDataAnalysis;


namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\ola\Desktop\Police";
            

            Dictionary<string, int> TotalCrimeTypeCounts = MonthCrimeAnalysis.MonthCrimeCheck(directoryPath);
            

            

            Console.Read();



        }

    }

}