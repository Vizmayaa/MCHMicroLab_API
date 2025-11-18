using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class CptMaster
    {
        public int ID { get; set; }
        public int CPTTypeID { get; set; }
        public string CPTCode { get; set; }
        public string CPTShortName { get; set; }
        public string CPTName { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        public bool IsDeleted { get; set; }
        public string CPTType { get; set; }
    }
    public class CptMasterResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        
        public List<CptMaster> data { get; set; } // Add this property
    }

}