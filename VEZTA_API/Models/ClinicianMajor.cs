using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class ClinicianMajor
    {
        public int ID { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ClinicianMajorResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        
        public List<ClinicianMajor> data { get; set; }
    }
}