using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class Speciality
    {
        public int ID { get; set; }
        public string SpecialityCode { get; set; }
        public string SpecialityName { get; set; }
        public string SpecialityShortName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class SpecialityResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
      
        public List<Speciality> data { get; set; } // Add this property
    }
}