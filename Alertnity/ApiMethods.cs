using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Alertnity.PoliceApi;
using Alertnity.PostcodeApi;

namespace Alertnity
{
    public class ApiMethods
    {
        public static PostcodeApiResponse PostcodeApiReturnJson(string Url)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(Url);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                var postcodeApiResponseValue = JsonSerializer.Deserialize<PostcodeApiResponse>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return postcodeApiResponseValue;
            }
        }
        public static List<PostcodeConverter> SavePostcodeApiResponse(PostcodeApiResponse postcodeApiResponseValue)
        {

            List<PostcodeConverter> converters = new List<PostcodeConverter>();

            if (postcodeApiResponseValue != null && postcodeApiResponseValue.Result != null)
            {
                foreach (var result in postcodeApiResponseValue.Result)
                {
                    PostcodeConverter converter = new()
                    {
                        Latitude = result.latitude,
                        Longitude = result.longitude
                    };

                    UIMethods.DisplayPostCodeConverterResponse(converter);

                    converters.Add(converter);
                }
            }
            else
            {
                UIMethods.DisplayNullResponse();
            }
            return converters;
        }
        
        public static string CreatePolyParameter(List<PostcodeConverter> converters)
        {
            var polyParts = converters.Select(c => $"{c.Latitude},{c.Longitude}");
            return string.Join(":", polyParts);
        }        
        public static Outcome[] PoliceApiReturnJson(string Url)
        { 
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(Url);

                var result = client.GetAsync(endpoint).Result;

                if(result.StatusCode == HttpStatusCode.OK)
                {
                    var json = result.Content.ReadAsStringAsync().Result;

                    var crimeIncidents = JsonSerializer.Deserialize<Outcome[]>(json, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                    return crimeIncidents;
                }
                return null;
            }
        }
        public static List<CrimeInfo> ProcessCrimeIncidents(Outcome[] crimeIncidents)
        {
            List<CrimeInfo> crimeInfos = new List<CrimeInfo>();

            if (crimeIncidents != null)
            {
                foreach (var crimeIncident in crimeIncidents)
                {
                    CrimeInfo crimeInfo = new CrimeInfo
                    {
                        category = crimeIncident.category,
                        location = crimeIncident.location,
                        outcome_status = crimeIncident.outcome_status,
                        location_subtype = crimeIncident.location_subtype,
                        persistent_id = crimeIncident.persistent_id,
                        id = crimeIncident.id,
                        location_type = crimeIncident.location_type,
                        month = crimeIncident.month,
                    };
                    crimeInfos.Add(crimeInfo);
                }
            }
            else
            {
                UIMethods.DisplayNullResponse();
            }
            return crimeInfos;
        }
        public static List<CrimeInfo> CheckPostcodeCrimeRate(string insertPostcode, DateTime startDateTime, DateTime? endDateTime)
        {
            // Checking if endDateTime is null
            DateTime actualEndDate = endDateTime ?? startDateTime;

            // Getting the months between start and end dates
            List<string> months = GetMonthsBetween(startDateTime, actualEndDate);

            // Fetch nearest postcodes
            string url = $"https://api.postcodes.io/postcodes/{insertPostcode}/nearest?radius=600&limit=100";
            PostcodeApiResponse postcodeApiResponseValue = ApiMethods.PostcodeApiReturnJson(url);
            List<PostcodeConverter> converters = ApiMethods.SavePostcodeApiResponse(postcodeApiResponseValue);

            // Instance of the result list
            var allCrimeInfo = new List<CrimeInfo>();

            // Iterate through each month
            foreach (var month in months)
            {
                string poly = ApiMethods.CreatePolyParameter(converters);
                string crimeUrl = $"https://data.police.uk/api/crimes-street/all-crime?poly={poly}&date={month}";
                UIMethods.UrlPrint(crimeUrl);

                try
                {
                    // Make API call
                    Outcome[] crimeIncidents = ApiMethods.PoliceApiReturnJson(crimeUrl);

                    // continue with the next month if no result for the present month.
                    if (crimeIncidents == null || crimeIncidents.Length == 0)
                    {
                        UIMethods.MonthPrint(month);
                        continue;
                    }

                    // Process and add the crime incidents
                    var processedCrimeInfo = ApiMethods.ProcessCrimeIncidents(crimeIncidents);
                    allCrimeInfo.AddRange(processedCrimeInfo);
                }
                catch (Exception ex)
                {
                    // Log the error 
                    //Console.WriteLine($"Error fetching data for {month}: {ex.Message}");
                    UIMethods.MonthErrorLog(month, ex);
                    continue; 
                }
            }

            // Return all processed crime info, even if some months had no data
            return allCrimeInfo;
        }
        public static List<string> GetMonthsBetween(DateTime startDate, DateTime endDate)
        {
            var monthResult = new List<string>();

            if (startDate != null && endDate != null)
            {
                // Check correct start and end dates
                DateTime start = startDate < endDate ? startDate : endDate;
                DateTime end = startDate > endDate ? startDate : endDate;

                // Calculate total months between startDate and endDate
                int totalMonths = ((end.Year - start.Year) * 12) + (end.Month - start.Month);

                // Check through the months
                for (int i = 0; i <= totalMonths; i++)
                {
                    DateTime insertDate = start.AddMonths(i);

                    // Skip if the month is in the future
                    if (insertDate > DateTime.Now)
                    {
                        //Console.WriteLine($"Skipping future date: {insertDate.ToString("yyyy-MM")}");
                        UIMethods.SkippingFutureDatePrint(insertDate);
                        continue;
                    }

                    string formattedDate = insertDate.ToString("yyyy-MM");
                    monthResult.Add(formattedDate);
                }
            }
            else
            {
                //Console.WriteLine("Invalid date range.");
                UIMethods.InvalidDateRangePrint();
            }
            return monthResult;
        }
    }
}

