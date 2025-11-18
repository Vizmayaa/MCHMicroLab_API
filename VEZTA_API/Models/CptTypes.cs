using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class CptTypes
    {
        public int ID { get; set; }
        public string CptType { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CptTypeResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
      
        public List<CptTypes> data { get; set; } // Add this property
    }
}