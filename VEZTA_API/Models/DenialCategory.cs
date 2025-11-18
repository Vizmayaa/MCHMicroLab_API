using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class DenialCategory
    {
        public int ID { get; set; }
        public string DenialCategorys { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class DenialCategoryResponse
    {
        public string flag { get; set; }
        public string message { get; set; }     
        public List<DenialCategory> data { get; set; } // Add this property
    }
}