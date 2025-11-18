using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class DenialTypes
    {
        public int ID { get; set; }
        public string DenialType { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class DenialTypeResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
     
        public List<DenialTypes> data { get; set; } // Add this property
    }
}