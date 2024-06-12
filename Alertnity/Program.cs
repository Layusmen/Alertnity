using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

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
           
            //Single User Posts.
            Console.WriteLine($"\nUser Post Information is being attached:\n");
            UserPost userPost = UserInputMethods.CreateUserPost(userInfo, 101, "Hi Everyone", "This is my first post!");
            UserPrintMethods.PrintUserPost(userPost);


            Console.WriteLine($"\nMultiple User General Information is being attached:\n");
            List<UserInfo> users = UserInputMethods.MulitpleUserInfo();
            UserPrintMethods.printMultipleUserInfo(users);
        }
    }
}
