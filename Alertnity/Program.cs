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
using Blazorise;
using CsvHelper.Configuration;

namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directoryLink = Path.Combine(desktopPath, "Police");

            Console.WriteLine(directoryLink);

            string[] directoryFolders = Directory.GetDirectories(directoryLink, "**", SearchOption.AllDirectories);

            List<CrimeRecord> csvFileContent = new();

            foreach (string folder in directoryFolders)
            {
                Console.WriteLine(folder);
                string[] csvFiles = Directory.GetFiles(folder, "*.csv");

                foreach(string csvFile in csvFiles)
                {

                    Console.WriteLine(csvFile);
                    using var reader = new StreamReader(csvFile);
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,
                         MissingFieldFound = null,
                    };
                    using var csv = new CsvReader(reader, config);
                    var records = csv.GetRecords<CrimeRecord>();

                    foreach (var record in records)
                    {
                        Console.WriteLine( record.month + " " + record.Latitude + " " + record.Latitude);

                        csvFileContent.Add(record);
                    }
                }
            }

            

            Console.ReadLine();
        }

    }

}