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
using System.Data;


namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Monthly analysis
            //string directoryPath = @"C:\Users\ola\Desktop\Police";
            //Dictionary<string, int> TotalCrimeTypeCounts = MonthCrimeAnalysis.MonthCrimeCheckWithArchive(directoryPath);


            //Area
            //string directoryPath = @"C:\Users\ola\Desktop\Police";
            ////double Latitude = 50.848409;
            ////double Longitude = -1.088152;

            //var crimeCounts = ArchiveCrimeByRadius.CoordinateCrimeCheckFromArchive(directoryPath, Latitude, Longitude);

            ////
            //Console.Read();

            //string inputFile = @"C:\Users\ola\Desktop\text\2024-10\2024-10-hampshire-street.csv";
            //string outputFile = @"C:\Users\ola\Desktop\text\2024-11-hampshire-street.csv";
            //List<CrimeRecord> outputRecords = new();
            //using var reader = new StreamReader(inputFile);
            //using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            //var records = csv.GetRecords<dynamic>().ToList();
            //int i = 0;
            //foreach (var record in records)
            //{
            //    Console.WriteLine(record.CrimeType + " " + record.Longitude + " " + record.Latitude + " " + record.Location + " " +record.Outcome);
            //    i++;            
            //}

            //Console.WriteLine(value: $"Number of records: {records.Count()}");

            //Console.ReadKey();


        }

    }

}