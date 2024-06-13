using System;

namespace Alertnity
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public Permissions UserPermissions { get; set; }
        public DateTime DateCreated { get; set; }   
    }
}
