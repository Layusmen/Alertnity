using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alertnity.Program;

namespace Alertnity
{
    public class PostcodeApiResponse
    {
        public int Status { get; set; }
        public Result[] Result { get; set; }
    }
}
