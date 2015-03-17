using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2PAccess.API.Model
{
    public class Course
    {
        public string uniqueid { get; set; }
        public string semester { get; set; }
        public string courseTitle { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public int itemId { get; set; }
    }
}
