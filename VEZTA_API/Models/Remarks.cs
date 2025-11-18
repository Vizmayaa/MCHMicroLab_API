using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEZTA.Models
{
    public class RemarksResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<RemarksClass> Data { get; set; }
    }

    public class RemarksClass
    {
        public int ID { get; set; }
        public string REMARKS { get; set; }
    }
}
