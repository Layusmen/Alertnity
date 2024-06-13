using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace Alertnity{    internal class UserInputMethods    {
        public static UserInfo CreateUserInfo()        {            UserInfo userInfo = new UserInfo()            {                UserID = 01,                UserName = "Delex",                FirstName = "Wale",                LastName = "Alodimeji",                PhoneNumber = "+448078911129",                Postcode = "P04 879",                Email = "getdelex@gmail.com",                UserPermissions = Permissions.Admin,                DateCreated = DateTime.Now.AddMinutes(1),            };            return userInfo;        }        public static UserPost CreateUserPost(UserInfo userInfo, int postID, string title, string content)        {            UserPost userPost = new UserPost()            {                PostID = postID,                UserID = userInfo.UserID,                UserName = userInfo.UserName,                Title = title,                Content = content,                Postcode = userInfo.Postcode,                Timestamp = DateTime.Now            };            return userPost;        }        public static List<UserInfo> MulitpleUserInfo()        {            List<UserInfo> multipleUserInfo = new List<UserInfo>();            UserInfo userInfo1 = new UserInfo()            {
                UserID = 01,
                UserName = "Delex",                FirstName = "Wale",                LastName = "Alodimeji",                PhoneNumber = "+4480789222999",                Postcode = "P04 879",                Email = "getdelex@gmail.com",                UserPermissions = Permissions.Admin,                DateCreated = DateTime.Now.AddMinutes(1),            };
            multipleUserInfo.Add(userInfo1);            UserInfo userInfo2 = new UserInfo()            {                UserID = 011,                UserName = "Kunlex",                FirstName = "Kunle",                LastName = "Adis",                PhoneNumber = "+448073922229",                Postcode = "P04 879",                Email = "getdelex@gmail.com",                UserPermissions = Permissions.Admin,                DateCreated = DateTime.Now.AddMinutes(1),            };            multipleUserInfo.Add(userInfo2);            return multipleUserInfo;        }        public static CrimeInfo CheckCrimeInfo()        {            CrimeInfo crimeinfo = new CrimeInfo()            {                ReportedDate = DateTime.Now.AddMinutes(1),                CrimeID = "01",                Category = CrimeCategory.Assault,                CrimeOutcome = "Charged",                CrimeDescription = "The accussed was found bougling houses",                Party = ReportingParty.Anonymous            };            return crimeinfo;        }        public static CrimeAddress CheckCrimeAddress()        {            CrimeAddress crimeAddress = new CrimeAddress()            {
                Longitude = 290098089,                Latitude = -2.00002901,                StreetName = "Angerstein",                Postcode = "LE218H",                ApproximateLocation = "Domino Pissa"            };            return crimeAddress;        }

        public static PostcodeConverter ConvertPostcodeToLongitudeAndLatitude(CrimeAddress crimeAddress)
        {
            PostcodeConverter postcodeConverter = new PostcodeConverter()
            {
                Postcode = crimeAddress.Postcode,
                Longitude = crimeAddress.Longitude,
                Latitude = crimeAddress.Latitude
            };
            return postcodeConverter;
        }
       
    }
}