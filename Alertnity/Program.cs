using System;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Alertnity.PoliceApi;
using Alertnity.PostcodeApi;
using CsvHelper;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Analysis;
using Alertnity.ArchiveDataAnalysis;
using System.Data;
using System.IO;

namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string directoryLink = Path.Combine(desktopPath, "Police");

            Console.WriteLine(directoryLink);

            string[] directoryFolders = Directory.GetDirectories(directoryLink, "**", SearchOption.AllDirectories);

            foreach (string folder in directoryFolders)
            {
                Console.WriteLine(folder);
            }
            Console.ReadLine();
        }

    }

}