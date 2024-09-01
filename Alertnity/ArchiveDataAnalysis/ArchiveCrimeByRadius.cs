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
    public static class ArchiveCrimeByRadius
    {
        public static Dictionary<string, int> CoordinateCrimeCheckFromArchive(string directoryPath, double inputLatitude, double inputLongitude)
        {
            double radiusMeters = 300.0;

            bool isSingleMonth = GetSearchType();

            (DateTime startDateTime, DateTime endDateTime) = GetDateRange(isSingleMonth);
            var totalCrimeTypeCounts = new Dictionary<string, int>();
            int totalWithinRangeCount = 0;

            string firstKeyword = "hampshire";
            string secondKeyword = "street";

            try
            {
                string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

                if (subdirectories.Length > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);
                        ProcessSubdirectory(subdirectory, firstKeyword, secondKeyword, startDateTime, endDateTime, isSingleMonth, inputLatitude, inputLongitude, radiusMeters, ref totalWithinRangeCount, ref totalCrimeTypeCounts);
                    }

                    OutputResults(totalCrimeTypeCounts, totalWithinRangeCount, radiusMeters);
                    return totalCrimeTypeCounts;
                }
                else
                {
                    Console.WriteLine("No subdirectories found.");
                    return new Dictionary<string, int>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directories: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }

        public static bool GetSearchType()
        {
            Console.WriteLine("Do you want to search by a single month or a date range?");
            Console.WriteLine("1. Single Month");
            Console.WriteLine("2. Date Range");
            string searchType = Console.ReadLine();
            return searchType == "1";
        }

        public static (DateTime, DateTime) GetDateRange(bool isSingleMonth)
        {
            DateTime startDateTime, endDateTime;

            if (isSingleMonth)
            {
                startDateTime = GetSingleMonth();
                endDateTime = startDateTime;
            }
            else
            {
                (startDateTime, endDateTime) = GetDateRange();
            }
            return (startDateTime, endDateTime);
        }

        private static DateTime GetSingleMonth()
        {
            DateTime singleMonthDate;
            string singleMonth;
            do
            {
                Console.Write("Please insert the month to search (e.g., 2013-01): ");
                singleMonth = Console.ReadLine();
            } while (!DateTime.TryParseExact(singleMonth, "yyyy-MM", null, DateTimeStyles.None, out singleMonthDate));

            return singleMonthDate;
        }

        private static (DateTime, DateTime) GetDateRange()
        {
            DateTime startDateTime, endDateTime;

            string startDate, endDate;

            do
            {
                Console.Write("Insert the start date (YYYY-MM): ");
                startDate = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(startDate, "yyyy-MM", null, DateTimeStyles.None, out startDateTime));

            do
            {
                Console.Write("Insert the end date (YYYY-MM): ");
                endDate = Console.ReadLine();
            } while (!DateTime.TryParseExact(endDate, "yyyy-MM", null, DateTimeStyles.None, out endDateTime) || endDateTime < startDateTime);

            return (startDateTime, endDateTime);
        }

        private static void ProcessSubdirectory(string subdirectory, string firstKeyword, string secondKeyword, DateTime startDateTime, DateTime endDateTime, bool isSingleMonth, double inputLatitude, double inputLongitude, double radiusMeters, ref int totalWithinRangeCount, ref Dictionary<string, int> totalCrimeTypeCounts)
        {
            string[] csvFiles = Directory.GetFiles(subdirectory, "*.csv");

            foreach (string csvFile in csvFiles)
            {
                string fileName = Path.GetFileName(csvFile);
                DateTime fileDate;

                if (DateTime.TryParseExact(fileName.Substring(0, 7), "yyyy-MM", null, DateTimeStyles.None, out fileDate))
                {
                    if (IsFileInRange(fileName, firstKeyword, secondKeyword, fileDate, startDateTime, endDateTime, isSingleMonth))
                    {
                        Console.WriteLine($"Processing file: {csvFile}");
                        ProcessCsvFile(csvFile, inputLatitude, inputLongitude, radiusMeters, ref totalWithinRangeCount, ref totalCrimeTypeCounts);
                    }
                }
            }
        }

        public static bool IsFileInRange(string fileName, string firstKeyword, string secondKeyword, DateTime fileDate, DateTime startDateTime, DateTime endDateTime, bool isSingleMonth)
        {
            return (isSingleMonth ? fileDate == startDateTime : fileDate >= startDateTime && fileDate <= endDateTime) &&
                   fileName.Contains(firstKeyword, StringComparison.OrdinalIgnoreCase) &&
                   fileName.Contains(secondKeyword, StringComparison.OrdinalIgnoreCase);
        }

        private static void ProcessCsvFile(string csvFile, double inputLatitude, double inputLongitude, double radiusMeters, ref int totalWithinRangeCount, ref Dictionary<string, int> totalCrimeTypeCounts)

        {
            try
            {
                using (var reader = new StreamReader(csvFile))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null,
                }))
                {
                    csv.Context.RegisterClassMap<CrimeRecordMap>();
                    var records = csv.GetRecords<CrimeRecord>().ToList();

                    var lsoaRecords = records.Where(r => r.LSOAName != null && r.LSOAName.Contains("Portsmouth", StringComparison.OrdinalIgnoreCase));

                    foreach (var record in lsoaRecords)
                    {
                        double distance = GeoCalculator.HaversineDistance(inputLatitude, inputLongitude, record.Latitude, record.Longitude);
                        Console.WriteLine($"Distance to record: {distance} meters");

                        if (distance <= radiusMeters)
                        {
                            Console.WriteLine($"Within Range -> Latitude: {record.Latitude}, Longitude: {record.Longitude}, Distance: {distance} meters");
                            totalWithinRangeCount++;
                            UpdateCrimeTypeCounts(ref totalCrimeTypeCounts, record.CrimeType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {csvFile}: {ex.Message}");
            }
        }

        private static void UpdateCrimeTypeCounts(ref Dictionary<string, int> crimeTypeCounts, string crimeType)
        {
            if (crimeTypeCounts.ContainsKey(crimeType))
            {
                crimeTypeCounts[crimeType]++;
            }
            else
            {
                crimeTypeCounts[crimeType] = 1;
            }
        }

        private static void OutputResults(Dictionary<string, int> totalCrimeTypeCounts, int totalWithinRangeCount, double radiusMeters)
        {
            Console.WriteLine("Aggregated Crime Type Counts:");
            foreach (var crimeTypeCount in totalCrimeTypeCounts)
            {
                Console.WriteLine($"{crimeTypeCount.Key}: {crimeTypeCount.Value}");
            }

            Console.WriteLine($"Total number of longitude and latitude points within {radiusMeters} meters: {totalWithinRangeCount}");
        }
    }
}