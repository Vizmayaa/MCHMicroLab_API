using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
   
    public class Facility
    {
        public int ID { get; set; }
        public string FacilityLicense { get; set; }
        public string FacilityName { get; set; }
        public int FacilityRegionID { get; set; }
        public string FacilityRegion { get; set; }
        public int FacilityTypeID { get; set; }
        public string FacilityType { get; set; }
        public int FacilityGroupID { get; set; }
        public string FacilityGroup { get; set; }
        public string FacilityAddress { get; set; }
        public int PostOfficeID { get; set; }
        public string Postoffice { get; set; }
        public string IsDeleted { get; set; }
    }

    public class FacilityMasterResponse
    {
        public string flag { get; set; }
        public string message { get; set; }    
        public List<Facility> data { get; set; } // Add this property
    }


}