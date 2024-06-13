using System;

namespace Alertnity
{
    internal class UserPrintMethods
    {
        public static void PrintUserInfo(UserInfo userInfo)
        {
            Console.WriteLine("UserID: " + userInfo.UserID);
            Console.WriteLine("UserName: " + userInfo.UserName);
            Console.WriteLine("FirstName: " + userInfo.FirstName);
            Console.WriteLine("LastName: " + userInfo.LastName);
            Console.WriteLine("PhoneNumber: " + userInfo.PhoneNumber);
            Console.WriteLine("Postcode: " + userInfo.Postcode);
            Console.WriteLine("Email: " + userInfo.Email);
            Console.WriteLine("UserPermissions: " + userInfo.UserPermissions);
            Console.WriteLine("DateCreated: " + userInfo.DateCreated);
        }
        public static void PrintUserPost(UserPost userPost)
        {
            Console.WriteLine("PostID: " + userPost.PostID);
            Console.WriteLine("UserID: " + userPost.UserID);
            Console.WriteLine("UserName: " + userPost.UserName);
            Console.WriteLine("Title: " + userPost.Title);
            Console.WriteLine("Content: " + userPost.Content);
            Console.WriteLine("Postcode: " + userPost.Postcode);
            Console.WriteLine("Timestamp: " + userPost.Timestamp);
        }

        public static void printMultipleUserInfo(List<UserInfo> users)
        {
            int i = 0;
            foreach (var user in users)
            {
                Console.WriteLine($"\nUser 1{1+i} General information:\n");
                Console.WriteLine($"UserID: {user.UserID}");
                Console.WriteLine($"UserName: {user.UserName}");
                Console.WriteLine($"FirstName: {user.FirstName}");
                Console.WriteLine($"LastName: {user.LastName}");
                Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                Console.WriteLine($"Postcode: {user.Postcode}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"UserPermissions: {user.UserPermissions}");
                Console.WriteLine($"DateCreated: {user.DateCreated}");
                Console.WriteLine();
            }
        }


        public static void PrintCrimeInfo(CrimeInfo crimeInfo)
        {
            Console.WriteLine("ReportedDate: " + crimeInfo.ReportedDate);
            Console.WriteLine("CrimeID: " + crimeInfo.CrimeID);
            Console.WriteLine("CrimeCategory: " + crimeInfo.Category);
            Console.WriteLine("CrimeOutcome " + crimeInfo.CrimeOutcome);
            Console.WriteLine("CrimeDescription: " + crimeInfo.CrimeDescription);
            Console.WriteLine("reportingParty: " + crimeInfo.Party);
        }


        public static void PrintCrimeAddress(CrimeAddress crimeAddress)
        {
            Console.WriteLine("Longitude: " + crimeAddress.Longitude);
            Console.WriteLine("Latitude: " + crimeAddress.Latitude);    
            Console.WriteLine("StreetName: " + crimeAddress.StreetName);
            Console.WriteLine("Postcode: " + crimeAddress.Postcode);
            Console.WriteLine("ApproximateLocation: " + crimeAddress.ApproximateLocation);
        }

        public static void PrintPostcodeInLongitudeAndLatitude(PostcodeConverter postcodeConverter)
        {
            Console.WriteLine("Postcode: " + postcodeConverter.Postcode);
            Console.WriteLine("Longitude: " + postcodeConverter.Longitude);
            Console.WriteLine("Latitude: " + postcodeConverter.Latitude);
            
        }
    }
}
