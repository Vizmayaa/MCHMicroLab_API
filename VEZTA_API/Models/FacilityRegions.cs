using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class FacilityRegions
    {
        public int ID { get; set; }
        public string FacilityRegion { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
       
    }
    public class FacilityRegionResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
       
        public List<FacilityRegions> data { get; set; } 
    }
}