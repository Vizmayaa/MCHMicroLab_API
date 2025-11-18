using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class LicenseFacInfo
    {
        public int Flag { get; set; }   
        public string Message { get; set; }
        public string CustomerName { get; set; }
        public string ProductKey { get; set; }
        
        public List<FacInfo> data { get; set; }
    }
    public class FacInfo
    {
        public int ID { get; set; }
        public string FacilityLicense { get; set; }
        public string FacilityName { get; set; }
        public string status { get; set; }    
        public string FacilityRegion { get; set; }
        public string PostOffice { get; set; }
        public string Expiry_Date { get; set; }

    }
    
}