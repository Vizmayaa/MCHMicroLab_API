using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEZTA.Models
{
    public class DepartmentResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<DepartmentClass> Data { get; set; }
    }

    public class DepartmentClass
    {
        public int ID { get; set; }
        public string DEPARTMENT { get; set; }
        public bool IS_INACTIVE { get; set; }
    }
}
