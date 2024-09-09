using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertnity.PostcodeApi;
using Alertnity.PoliceApi;
using Blazorise.Utilities;
using System.Text.Json;
using CsvHelper.TypeConversion;

namespace Alertnity.ArchiveDataAnalysis
{
    public static class ArchiveCrimeByRadius
    {
        public static PostcodeConverter GetPostcodeForArchiveData(string insertPostcode)
        {
            // URL for the API request
            var url = $"https://api.postcodes.io/postcodes/{insertPostcode}";

            // API response
            Result postcodeApiResponseValue = null;

            // converter to store the results
            PostcodeConverter converter = new PostcodeConverter();

            try
            {
                // HttpClient to make the API request
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(url);
                    var result = client.GetAsync(endpoint).Result;

                    // Ensure the request was successful
                    if (result.IsSuccessStatusCode)
                    {
                        var json = result.Content.ReadAsStringAsync().Result;

                        // Output the raw JSON for inspection
                        Console.WriteLine("Raw JSON Response:");
                        Console.WriteLine(json);


                        var Root = JsonSerializer.Deserialize<Root>(json, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                        postcodeApiResponseValue = Root?.result;


                        converter.Latitude = postcodeApiResponseValue.latitude;
                        converter.Longitude = postcodeApiResponseValue.longitude;
                        converter.PFA = postcodeApiResponseValue.pfa;

                        Console.WriteLine($"Latitude: {postcodeApiResponseValue?.latitude}");
                        Console.WriteLine($"Longitude: {postcodeApiResponseValue?.longitude}");
                        Console.WriteLine($"PFA: {postcodeApiResponseValue?.pfa}");
                    }
                    else
                    {
                        Console.WriteLine($"API request failed with status code: {result.StatusCode}");
                        return converter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while making the API request: {ex.Message}");
                return converter;  // Return an empty converter if an error occurs
            }

            return converter;
        }


        public static Dictionary<string, int> CoordinateCrimeCheckFromArchive(
    string directoryPath, PostcodeConverter converter, DateTime startDateTime, DateTime? endDateTime = null)
        {
            double radiusMeters = 300.0;

            // Use startDateTime as both start and end date if endDateTime is null
            DateTime actualEndDate = endDateTime ?? new DateTime(startDateTime.Year, startDateTime.Month, DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month));

            var totalCrimeTypeCounts = new Dictionary<string, int>();
            int totalWithinRangeCount = 0;
            double? latitude = converter.Latitude;
            double? longitude = converter.Longitude;
            string? adminDistrict = converter.PFA;
            string removeKeyword = "street";

            try
            {
                string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

                if (subdirectories.Length > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);
                        ProcessSubdirectory(subdirectory, adminDistrict, removeKeyword, startDateTime, actualEndDate,
                            (double)latitude, (double)longitude, radiusMeters, ref totalWithinRangeCount, ref totalCrimeTypeCounts);
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
        private static void ProcessSubdirectory(
    string subdirectory,
    string adminDistrict,
    string secondKeyword,
    DateTime startDateTime,
    DateTime endDateTime,
    double Latitude,
    double Longitude,
    double radiusMeters,
    ref int totalWithinRangeCount,
    ref Dictionary<string, int> totalCrimeTypeCounts)
        {
            string[] csvFiles = Directory.GetFiles(subdirectory, "*.csv");

            foreach (string csvFile in csvFiles)
            {
                string fileName = Path.GetFileName(csvFile);
                DateTime fileDate;

                // Parse the file date from the file name
                if (DateTime.TryParseExact(fileName.Substring(0, 7), "yyyy-MM", null, DateTimeStyles.None, out fileDate))
                {
                    // Check if the file falls within the date range and matches keywords
                    if (IsFileInRange(fileName, adminDistrict, secondKeyword, fileDate, startDateTime, endDateTime))
                    {
                        Console.WriteLine($"Processing file: {csvFile}");

                        // Process the CSV file if it matches the range and keywords
                        ProcessCsvFile(csvFile, Latitude, Longitude, radiusMeters, ref totalWithinRangeCount, ref totalCrimeTypeCounts);
                    }
                }
            }
        }

        public static bool IsFileInRange(string fileName, string adminDistrict, string secondKeyword, DateTime fileDate, DateTime startDateTime, DateTime endDateTime)
        {
            // Compare only year and month
            bool isDateInRange = (fileDate.Year > startDateTime.Year ||
                                 (fileDate.Year == startDateTime.Year && fileDate.Month >= startDateTime.Month)) &&
                                 (fileDate.Year < endDateTime.Year ||
                                 (fileDate.Year == endDateTime.Year && fileDate.Month <= endDateTime.Month));

            Console.WriteLine($"File: {fileName} | File Date: {fileDate:yyyy-MM} | Range: {startDateTime:yyyy-MM} - {endDateTime:yyyy-MM} | Is Date In Range: {isDateInRange}");

            bool containsAdminDistrict = string.IsNullOrEmpty(adminDistrict) || fileName.Contains(adminDistrict, StringComparison.OrdinalIgnoreCase);
            bool containsSecondKeyword = string.IsNullOrEmpty(secondKeyword) || fileName.Contains(secondKeyword, StringComparison.OrdinalIgnoreCase);

            return isDateInRange && containsAdminDistrict && containsSecondKeyword;
        }

        public class SafeDoubleConverter : DoubleConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                // If the field is empty or whitespace, return 0.0 (or any default value)
                if (string.IsNullOrWhiteSpace(text))
                {
                    return 0.0;
                }

                // Otherwise, convert the text to double
                return base.ConvertFromString(text, row, memberMapData);
            }
        }
        private static void ProcessCsvFile(string csvFile, double Latitude, double Longitude, double radiusMeters, ref int totalWithinRangeCount, ref Dictionary<string, int> totalCrimeTypeCounts)
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
                    // check records having no or invalid  coordinates
                    var records = csv.GetRecords<CrimeRecord>()
                                     .Where(r => r.Latitude != 0.0 && r.Longitude != 0.0) 
                                     .ToList();

                    foreach (var record in records)
                    {
                        double distance = GeoCalculator.HaversineDistance(Latitude, Longitude, record.Latitude, record.Longitude);
                        if (distance <= radiusMeters)
                        {
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