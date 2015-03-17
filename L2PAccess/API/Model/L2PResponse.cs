using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2PAccess.API.Model
{
    public class L2PResponse<T>
    {
        public List<T> dataSet { get; set; }
        public bool Status { get; set; }
        public string errorDescription { get; set; }
        public string errorId { get; set; }
    }
}
