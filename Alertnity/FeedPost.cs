using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity
{
    public class FeedPost
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Postcode { get; set; }
    }
}