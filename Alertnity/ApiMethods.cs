﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Alertnity.PoliceApi;
using Alertnity.PostcodeApi;

namespace Alertnity
{
    internal class ApiMethods
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
                    PostcodeConverter converter = new PostcodeConverter
                    {
                        Latitude = result.Latitude,
                        Longitude = result.Longitude
                    };
                    converters.Add(converter);

                    Console.WriteLine("Longitude: " + result.Longitude);
                    Console.WriteLine("Latitude: " + result.Latitude);
                    Console.WriteLine("............................");
                }

            }
            else
            {
                Console.WriteLine("Response is null or nothing contained in the api.");
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

                var json = result.Content.ReadAsStringAsync().Result;

                var crimeIncidents = JsonSerializer.Deserialize<Outcome[]>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                return crimeIncidents;
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

                foreach (var crime in crimeInfos)
                {
                    Console.WriteLine($"Category: {crime.category}");
                    Console.WriteLine($"Location Type: {crime.location?.street.Id}");
                    Console.WriteLine($"Location:");
                    Console.WriteLine($"\tLatitude: {crime.location?.latitude}");
                    Console.WriteLine($"\tLongitude: {crime.location?.longitude}");
                    Console.WriteLine($"\tStreet: {crime.location?.street.Name}");
                    Console.WriteLine($"Outcome Status: {crime.outcome_status?.Category}");
                    Console.WriteLine($"Month: {crime.outcome_status?.Month}");
                    
                    Console.WriteLine("------------------");
                }
            }
            else
            {
                Console.WriteLine("Nothing found");
            }

            return crimeInfos;
        }

    }

}

