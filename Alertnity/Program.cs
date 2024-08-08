using System;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Alertnity.PoliceApi;
using Alertnity.PostcodeApi;



namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string directoryPath = @"C:\Users\ola\OneDrive\Desktop\Police";
            string searchPattern = "2014*";

            string[] subdirectories = Directory.GetDirectories(directoryPath,searchPattern);

            if (subdirectories.Length > 0)
            {
                Console.WriteLine("Found subdirectories:");
                foreach (string subdirectory in subdirectories)
                {
                    Console.WriteLine(subdirectory);
                }
            }
            else
            {
                Console.WriteLine("No subdirectories found.");
            }
         //   else
         //   {
         //       Console.WriteLine("No files found.");
         //   }
        }
    }
}