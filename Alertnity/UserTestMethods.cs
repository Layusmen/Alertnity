using System;
using System.Globalization;

namespace Alertnity
{
    internal class UserTestMethods
    {
        public static UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo()
            {
                ID = 01,
                Name = "Delex",
                FirstName = "Wale",
                LastName = "Alodimeji",
                PhoneNumber = "+448078911129",
                Postcode = "P04 879",
                Email = "getdelex@gmail.com",
                Permissions = Permissions.Admin,
                DateCreated = DateTime.Now.AddMinutes(1),
            };
            return userInfo;
        }
        public static Post CreateUserPost(UserInfo userInfo, int postID, string title, string content, string userName)
        {
            Post userPost = new Post()
            {
                ID = postID,
                User = userInfo,
                Title = title,
                Content = content,
                Timestamp = DateTime.Now
            };
            return userPost;
        }
        public static List<UserInfo> MulitpleUserInfo()
        {
            List<UserInfo> multipleUserInfo = new List<UserInfo>();

            UserInfo userInfo1 = new UserInfo()
            {
                ID = 01,
                Name = "Delex",
                FirstName = "Wale",
                LastName = "Alodimeji",
                PhoneNumber = "+4480789222999",
                Postcode = "P04 879",
                Email = "getdelex@gmail.com",
                Permissions = Permissions.Admin,
                DateCreated = DateTime.Now.AddMinutes(1),
            };
            multipleUserInfo.Add(userInfo1);

            UserInfo userInfo2 = new UserInfo()
            {
                ID = 011,
                Name = "Kunlex",
                FirstName = "Kunle",
                LastName = "Adis",
                PhoneNumber = "+448073922229",
                Postcode = "P04 879",
                Email = "getdelex@gmail.com",
                Permissions = Permissions.Admin,
                DateCreated = DateTime.Now.AddMinutes(1),
            };
            multipleUserInfo.Add(userInfo2);

            return multipleUserInfo;
        }
    }
}