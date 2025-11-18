using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEZTA.Models
{
    public class AntibioticResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<AntibioticClass> Data { get; set; }
    }

    public class AntibioticClass
    {
        public int ID { get; set; }
        public string ANTIBIOTIC { get; set; }
        public string CLASS_NAME { get; set; }
        public int CLASS_ID { get; set; }
        public bool IS_INACTIVE { get; set; }
        public int DISPLAY_ORDER { get; set; }
        public int ANTIBIOTIC_GROUP_ID { get; set; }
        public string ANTIBIOTIC_GROUP_NAME { get; set; }

    }
}
