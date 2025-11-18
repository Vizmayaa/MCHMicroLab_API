using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class ClinicianCategory
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
    }
    public class ClinicianCategoryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<ClinicianCategory> data { get; set; }
    }
}