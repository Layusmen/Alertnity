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
    public static class CsvFilesHandling
    {
        public static List<CrimeRecord> csvFileRecords()
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

                foreach (string csvFile in csvFiles)
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
                        Console.WriteLine(record.month + " " + record.Latitude + " " + record.Latitude);

                        csvFileContent.Add(record);
                    }
                }
            }


            Console.ReadLine();
            return csvFileContent;

        }
    }
}
