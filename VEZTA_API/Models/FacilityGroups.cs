using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class FacilityGroups
    {
        public int ID { get; set; }
        public string FacilityGroup { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
       
    }
    public class FacilityGroupResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
       
        public List<FacilityGroups> data { get; set; } 
    }
}