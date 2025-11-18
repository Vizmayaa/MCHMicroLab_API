using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEZTA.Models
{
    public class InvestigationResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<InvestigationClass> Data { get; set; }
    }

    public class InvestigationClass
    {
        public int ID { get; set; }
        public string INVESTIGATION { get; set; }
        public bool IS_INACTIVE { get; set; }
    }
}
