using CsvHelper.Configuration;
using CsvHelper;
using System;
//using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alertnity.PostcodeApi;
using Alertnity.PoliceApi;
using Blazorise.Utilities;
using System.Text.Json;
using CsvHelper.TypeConversion;
using System.ComponentModel.DataAnnotations;

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
            PostcodeConverter converter = new();

            try
            {
                // HttpClient to make the API request
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(url);
                    var result = client.GetAsync(endpoint).Result;

                    // Check successful request
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
                return converter;
            }

            return converter;
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

        public static List<CrimeRecord> ProcessDirectory(PostcodeConverter converter, string directoryPath, DateTime startDateTime, DateTime? endDateTime)
        {
            double radiusMeters = 300.0;
            double Latitude = converter.Latitude;
            double Longitude = converter.Longitude;
            string? adminDistrict = converter.PFA;
            string secondKeyword = "street";

            int year = startDateTime.Year;
            int month = startDateTime.Month;
            int lastDay = DateTime.DaysInMonth(year, month);
            DateTime actualEndDate = endDateTime ?? new DateTime(year, month, lastDay);
            List<CrimeRecord> outputRecords = new();
            try
            {
                string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

                if (subdirectories.Length > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);
                        string[] csvFiles = Directory.GetFiles(subdirectory, "*.csv");
                        foreach (string csvFile in csvFiles)
                        {
                            string fileName = Path.GetFileName(csvFile);
                            DateTime fileDate;

                            // Parse the file date from the file name
                            if (DateTime.TryParseExact(fileName.Substring(0, 7), "yyyy-MM", null, DateTimeStyles.None, out fileDate))
                            {
                                // Check if the file is in the date range and matche keywords
                                if (ArchiveCrimeByRadius.IsFileInRange(fileName, adminDistrict, secondKeyword, fileDate, startDateTime, actualEndDate))
                                {
                                    Console.WriteLine($"Processing file: {csvFile}");

                                    // Process the CSV file if it matches the range and keywords
                                    try
                                    {
                                        using (var reader = new StreamReader(csvFile))
                                        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                                        {
                                            HeaderValidated = null,
                                            MissingFieldFound = null,
                                        }))
                                        {

                                            // See if records is having no or invalid  coordinates
                                            var records = csv.GetRecords<CrimeRecord>()
                                                             .Where(r => r.Latitude != 0.0 && r.Longitude != 0.0);

                                            foreach (var record in records)
                                            {
                                                double distance = GeoCalculator.HaversineDistance(Latitude, Longitude, record.Latitude, record.Longitude);
                                                if (distance <= radiusMeters)
                                                {
                                                    outputRecords.Add(record);
                                                    Console.WriteLine(distance);
                                                }
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
                }
                else
                {
                    Console.WriteLine("No subdirectories found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directories: {ex.Message}");

            }
            return outputRecords;
        }

    }
   
}