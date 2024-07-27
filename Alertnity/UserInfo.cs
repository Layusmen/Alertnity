using System;

namespace Alertnity
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public Permissions Permissions { get; set; }
        public DateTime DateCreated { get; set; }   
    }
}
