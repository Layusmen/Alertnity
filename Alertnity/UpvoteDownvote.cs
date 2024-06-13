using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alertnity
{
    public class UpvoteDownvote
    {
        //tracking the counts of upvotes and downvotes
        public int Upvotes { get; private set; }
        public int Downvotes { get; private set; }

        public UpvoteDownvote()
        {
            Upvotes = 0;
            Downvotes = 0;
        }

        public void Upvote()
        {
            Upvotes++;
        }

        public void Downvote()
        {
            Downvotes++;
        }

        public int GetTotalScore()
        {
            return Upvotes - Downvotes;
        }
    }
}
