using System.Collections.Generic;

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
