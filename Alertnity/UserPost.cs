using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity
{
    internal class UserPost
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
        public string Postcode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
