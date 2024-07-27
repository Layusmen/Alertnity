using System;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;



namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter postcode:");
            string insertPostcode = Console.ReadLine();

            Console.WriteLine("Enter Date  int this format 2017-01:");
            string insertDate = Console.ReadLine();

            //Fetching Nearest Postcodes to be able to calculate Community Crime Rate, used 200m radius and 20 postcode limit
            var Url = $"https://api.postcodes.io/postcodes/{insertPostcode}/nearest?radius=200&limit=20";
            PostcodeApiResponse postcodeApiResponseValue = ApiMethods.PostcodeApiReturnJson(Url);

            //Save all Longitude and Latitude Into an instance of List<PostcodeConverter>

            List<PostcodeConverter> converters = ApiMethods.SavePostcodeApiResponse(postcodeApiResponseValue);

            //Now Insert the postcodeApiResponseValue into Police API
            //First convert the response
            string poly = ApiMethods.CreatePolyParameter(converters);
            //Insert the converted response into the API Url
            Url = $"https://data.police.uk/api/crimes-street/all-crime?poly={poly}&date={insertDate}";

            Console.WriteLine(Url);


            Outcome[] crimeIncidents = ApiMethods.PoliceApiReturnJson(Url);



            /*
            //Single User Information
            Console.WriteLine($"\nUser General Information is being attached:\n");
            UserInfo userInfo = UserInputMethods.CreateUserInfo();
            UserPrintMethods.PrintUserInfo(userInfo);

            //Multiple User General Information
            Console.WriteLine($"\nMultiple User General Information is being attached:\n");
            List<UserInfo> users = UserInputMethods.MulitpleUserInfo();
            UserPrintMethods.printMultipleUserInfo(users);

            //Single User Posts.
            Console.WriteLine($"\nUser Post Information is being attached:\n");
            Post userPost = UserInputMethods.CreateUserPost(userInfo, 101, "Hi Everyone", "This is my first post!");
            UserPrintMethods.PrintUserPost(userPost);

            //Crime Reporter
            Console.WriteLine($"\nInformation about the reported crime:\n");
            CrimeInfo crimeInfo = UserInputMethods.CheckCrimeInfo();
            UserPrintMethods.PrintCrimeInfo(crimeInfo);

            //Crime Address
            Console.WriteLine($"\nInformation about the crime address:\n"); 
            CrimeAddress crimeAddress =UserInputMethods.CheckCrimeAddress();
            UserPrintMethods.PrintCrimeAddress(crimeAddress);

            //Print Converted Postcode
            Console.WriteLine($"\nInformation about the converted postcode:\n");
            PostcodeConverter postcodeConverter = UserInputMethods.CrimeAddressPostcodeConverter(crimeAddress);
            UserPrintMethods.PrintPostcodeInLongitudeAndLatitude(postcodeConverter);

            //Upvote Dummy Calculation
            UpvoteDownvote vote = new UpvoteDownvote();

            vote.Upvote();
            vote.Upvote();
            vote.Downvote();

            Console.WriteLine("Upvotes: " + vote.Upvotes);
            Console.WriteLine("Downvotes: " + vote.Downvotes);
            Console.WriteLine("Total Score: " + vote.GetTotalScore());
            */

        }
}
}