using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class ClinicianProfession
    {
        public int ID { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
    }
    public class ClinicianProfessionResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
     
        public List<ClinicianProfession> data { get; set; }
    }
}