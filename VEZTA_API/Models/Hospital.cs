using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEZTA.Models
{
    public class HospitalResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<HospitalClass> Data { get; set; }
    }

    public class HospitalClass
    {
        public int ID { get; set; }
        public string HOSPITAL { get; set; }
        public bool IS_INACTIVE { get; set; }
    }
}
