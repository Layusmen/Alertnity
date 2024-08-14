using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alertnity.ArchiveDataAnalysis
{
    public class MonthCrimeAnalysis
    {
        public static Dictionary<string, int> MonthCrimeCheck(string directoryPath)
        {
            // Prompt user
            Console.WriteLine("Do you want to search by a single month or a date range?");
            Console.WriteLine("1. Single Month");
            Console.WriteLine("2. Date Range");

            string searchType = Console.ReadLine();
            DateTime startDateTime;
            DateTime endDateTime;
            bool isSingleMonth = searchType == "1";

            if (isSingleMonth)
            {
                // single month
                string singleMonth;
                do
                {
                    Console.Write("Please insert the month you want to search (e.g., 2013-01): ");
                    singleMonth = Console.ReadLine();
                } while (!DateTime.TryParseExact(singleMonth, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out startDateTime));
                endDateTime = startDateTime;
            }
            else
            {
                // Prompt for date range
                string startDate, endDate;

                do
                {
                    Console.Write("Please insert the start date (YYYY-MM): ");
                    startDate = Console.ReadLine();
                } while (!DateTime.TryParseExact(startDate, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out startDateTime));

                do
                {
                    Console.Write("Please insert the end date (YYYY-MM): ");
                    endDate = Console.ReadLine();
                } while (!DateTime.TryParseExact(endDate, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out endDateTime) || endDateTime < startDateTime);
            }

            string firstKeyword = "hampshire";
            string secondKeyword = "street";

            // Dictionary 
            var TotalCrimeTypeCounts = new Dictionary<string, int>();

            try
            {
                // Find all subdirectories
                string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

                if (subdirectories.Length > 0)
                {
                    //Console.WriteLine("Found subdirectories:");

                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);

                        // Find all CSV files 
                        string[] csvFiles = Directory.GetFiles(subdirectory, "*.csv");

                        foreach (string csvFile in csvFiles)
                        {
                            string fileName = Path.GetFileName(csvFile);

                            // date part from the filename (format YYYY-MM, e.g., 2013-01)
                            var fileDateStr = fileName.Substring(0, 7);

                            if (DateTime.TryParseExact(fileDateStr, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                            {
                                // Check if the file date is within the rwnage
                                bool isInDateRange = isSingleMonth ? fileDate == startDateTime : fileDate >= startDateTime && fileDate <= endDateTime;

                                if (isInDateRange &&
                                    fileName.Contains(firstKeyword, StringComparison.OrdinalIgnoreCase) &&
                                    fileName.Contains(secondKeyword, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine($"Processing file: {csvFile}");

                                    try
                                    {
                                        // CSV handling
                                        var dataFrame = DataFrame.LoadCsv(csvFile);

                                        // Check if the "Crime type" column exists
                                        if (!dataFrame.Columns.Any(column => column.Name == "Crime type"))
                                        {
                                            Console.WriteLine("The column 'Crime type' does not exist in the DataFrame.");
                                            continue;
                                        }

                                        // store in a dictionary
                                        var crimeTypeCounts = new Dictionary<string, int>();

                                        // Extract the "Crime type" column
                                        var crimeTypeColumn = dataFrame.Columns["Crime type"] as StringDataFrameColumn;

                                        // go through one b =y one
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

                                        // put count in dictionary
                                        foreach (var crimeTypeCount in crimeTypeCounts)
                                        {
                                            if (TotalCrimeTypeCounts.ContainsKey(crimeTypeCount.Key))
                                            {
                                                TotalCrimeTypeCounts[crimeTypeCount.Key] += crimeTypeCount.Value;
                                            }
                                            else
                                            {
                                                TotalCrimeTypeCounts[crimeTypeCount.Key] = crimeTypeCount.Value;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error reading file {csvFile}: {ex.Message}");
                                    }
                                }

                            }
                        }
                    }

                    // Output
                    Console.WriteLine("Aggregated Crime Type Counts:");
                    foreach (var crimeTypeCount in TotalCrimeTypeCounts)
                    {
                        Console.WriteLine($"{crimeTypeCount.Key}: {crimeTypeCount.Value}");
                    }
                    return TotalCrimeTypeCounts;
                }
                else
                {
                    Console.WriteLine("No subdirectories found.");
                    return new Dictionary<string, int>(); // 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directories: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }


    }
}
