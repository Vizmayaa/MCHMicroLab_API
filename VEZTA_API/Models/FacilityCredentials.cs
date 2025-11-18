using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class FacilityCredentials
    {
        public int ID { get; set; }
        public int FacilityID { get; set; }
        public int PostOfficeID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
    public class FacilityMain
    {
        public int ID { get; set; }
        public string FacilityLicense { get; set; }
        public string FacilityName { get; set; }
        public int? PostOfficeID { get; set; }
        public string Postoffice { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? LastModified_Time { get; set; }
        public int Flag { get; set; }
        public string message { get; set; }
        //public List<CptTypes> CptTypes { get; set; }
    }
}