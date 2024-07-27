using System;

namespace Alertnity
{
    internal class UserPrintMethods
    {
        public static void PrintUserInfo(UserInfo userInfo)
        {
            Console.WriteLine("ID: " + userInfo.ID);
            Console.WriteLine("User: " + userInfo.Name);
            Console.WriteLine("FirstName: " + userInfo.FirstName);
            Console.WriteLine("LastName: " + userInfo.LastName);
            Console.WriteLine("PhoneNumber: " + userInfo.PhoneNumber);
            Console.WriteLine("Postcode: " + userInfo.Postcode);
            Console.WriteLine("Email: " + userInfo.Email);
            Console.WriteLine("Permissions: " + userInfo.Permissions);
            Console.WriteLine("DateCreated: " + userInfo.DateCreated);
        }
        public static void PrintUserPost(Post post)
        {
            Console.WriteLine("ID: " + post.ID);
            Console.WriteLine("User: " + post.User);
            Console.WriteLine("Title: " + post.Title);
            Console.WriteLine("Content: " + post.Content);
            Console.WriteLine("Timestamp: " + post.Timestamp);
        }

        public static void printMultipleUserInfo(List<UserInfo> users)
        {
            int i = 0;
            foreach (var user in users)
            {
                Console.WriteLine($"\nUser 1{1+i} General information:\n");
                Console.WriteLine($"ID: {user.ID}");
                Console.WriteLine($"User: {user.Name}");
                Console.WriteLine($"FirstName: {user.FirstName}");
                Console.WriteLine($"LastName: {user.LastName}");
                Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                Console.WriteLine($"Postcode: {user.Postcode}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Permissions: {user.Permissions}");
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
            Console.WriteLine("reportingParty: " + crimeInfo.ReportingParty);
        }  
    }
}
