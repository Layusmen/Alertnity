using System;

namespace Alertnity
{
    internal class Post
    {
        public int ID { get; set; }
        
        public UserInfo User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }


    }
}