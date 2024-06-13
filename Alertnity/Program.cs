using System;

namespace Alertnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            UserPost userPost = UserInputMethods.CreateUserPost(userInfo, 101, "Hi Everyone", "This is my first post!");
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
            PostcodeConverter postcodeConverter = UserInputMethods.ConvertPostcodeToLongitudeAndLatitude(crimeAddress);
            UserPrintMethods.PrintPostcodeInLongitudeAndLatitude(postcodeConverter);

            //Upvote Dummy Calculation
            UpvoteDownvote vote = new UpvoteDownvote();

            vote.Upvote();
            vote.Upvote();
            vote.Downvote();

            Console.WriteLine("Upvotes: " + vote.Upvotes);
            Console.WriteLine("Downvotes: " + vote.Downvotes);
            Console.WriteLine("Total Score: " + vote.GetTotalScore());
        }
    }
}
