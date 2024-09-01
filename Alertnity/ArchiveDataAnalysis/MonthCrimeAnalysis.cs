using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alertnity.ArchiveDataAnalysis
{
    public class MonthCrimeAnalysis
    {
        public static Dictionary<string, int> MonthCrimeCheckWithArchive(string directoryPath)
        {
            bool isSingleMonth = ArchiveCrimeByRadius.GetSearchType();
            (DateTime startDateTime, DateTime endDateTime) = ArchiveCrimeByRadius.GetDateRange(isSingleMonth);

            string firstKeyword = "hampshire";
            string secondKeyword = "street";

            var totalCrimeTypeCounts = new Dictionary<string, int>();

            try
            {
                string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

                if (subdirectories.Length > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);
                        ProcessSubdirectory(subdirectory, firstKeyword, secondKeyword, startDateTime, endDateTime, isSingleMonth, ref totalCrimeTypeCounts);
                    }

                    OutputCrimeTypeCounts(totalCrimeTypeCounts);
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
        private static DateTime GetSingleMonth()
        {
            DateTime singleMonthDate;
            string singleMonth;
            do
            {
                Console.Write("Please insert the month you want to search (e.g., 2013-01): ");
                singleMonth = Console.ReadLine();
            } while (!DateTime.TryParseExact(singleMonth, "yyyy-MM", null, DateTimeStyles.None, out singleMonthDate));

            return singleMonthDate;
        }

        private static (DateTime, DateTime) GetDateRangeFromUser()
        {
            DateTime startDateTime, endDateTime;

            string startDate, endDate;

            do
            {
                Console.Write("Please insert the start date (YYYY-MM): ");
                startDate = Console.ReadLine();
            } while (!DateTime.TryParseExact(startDate, "yyyy-MM", null, DateTimeStyles.None, out startDateTime));

            do
            {
                Console.Write("Please insert the end date (YYYY-MM): ");
                endDate = Console.ReadLine();
            } while (!DateTime.TryParseExact(endDate, "yyyy-MM", null, DateTimeStyles.None, out endDateTime) || endDateTime < startDateTime);

            return (startDateTime, endDateTime);
        }

        private static void ProcessSubdirectory(string subdirectory, string firstKeyword, string secondKeyword, DateTime startDateTime, DateTime endDateTime, bool isSingleMonth, ref Dictionary<string, int> totalCrimeTypeCounts)
        {
            string[] csvFiles = Directory.GetFiles(subdirectory, "*.csv");

            foreach (string csvFile in csvFiles)
            {
                string fileName = Path.GetFileName(csvFile);
                DateTime fileDate;

                if (DateTime.TryParseExact(fileName.Substring(0, 7), "yyyy-MM", null, DateTimeStyles.None, out fileDate))
                {
                    if (ArchiveCrimeByRadius.IsFileInRange(fileName, firstKeyword, secondKeyword, fileDate, startDateTime, endDateTime, isSingleMonth))
                    {
                        Console.WriteLine($"Processing file: {csvFile}");
                        ProcessCsvFile(csvFile, ref totalCrimeTypeCounts);
                    }
                }
            }
        }

        private static void ProcessCsvFile(string csvFile, ref Dictionary<string, int> totalCrimeTypeCounts)
        {
            try
            {
                var dataFrame = DataFrame.LoadCsv(csvFile);

                if (!dataFrame.Columns.Any(column => column.Name == "Crime type"))
                {
                    Console.WriteLine("The column 'Crime type' does not exist in the DataFrame.");
                    return;
                }

                var crimeTypeCounts = new Dictionary<string, int>();

                var crimeTypeColumn = dataFrame.Columns["Crime type"] as StringDataFrameColumn;

                foreach (var row in dataFrame.Rows)
                {
                    var crimeType = row["Crime type"].ToString();

                    if (crimeTypeCounts.ContainsKey(crimeType))
                    {
                        crimeTypeCounts[crimeType]++;
                    }
                    else
                    {
                        crimeTypeCounts[crimeType] = 1;
                    }
                }

                UpdateCrimeTypeCounts(ref totalCrimeTypeCounts, crimeTypeCounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {csvFile}: {ex.Message}");
            }
        }

        private static void UpdateCrimeTypeCounts(ref Dictionary<string, int> totalCrimeTypeCounts, Dictionary<string, int> crimeTypeCounts)
        {
            foreach (var crimeTypeCount in crimeTypeCounts)
            {
                if (totalCrimeTypeCounts.ContainsKey(crimeTypeCount.Key))
                {
                    totalCrimeTypeCounts[crimeTypeCount.Key] += crimeTypeCount.Value;
                }
                else
                {
                    totalCrimeTypeCounts[crimeTypeCount.Key] = crimeTypeCount.Value;
                }
            }
        }

        private static void OutputCrimeTypeCounts(Dictionary<string, int> totalCrimeTypeCounts)
        {
            Console.WriteLine("Aggregated Crime Type Counts:");
            foreach (var crimeTypeCount in totalCrimeTypeCounts)
            {
                Console.WriteLine($"{crimeTypeCount.Key}: {crimeTypeCount.Value}");
            }
        }

    }
}
