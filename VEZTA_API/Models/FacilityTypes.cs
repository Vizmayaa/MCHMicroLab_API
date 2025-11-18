using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class FacilityTypes
    {
        public int ID { get; set; }
        public string FacilityType { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class FacilityTypeResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
       
        public List<FacilityTypes> data { get; set; } // Add this property
    }
}