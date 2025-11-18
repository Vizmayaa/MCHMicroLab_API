using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class UserLevels
    {
        public int ID { get; set; }

        public string LEVEL_NAME { get; set; }
       
        public bool IS_INACTIVE { get; set; }

        public List<UserRight> rights { get; set; }
      
    }
    public class UserRight
    {
        public int MODULE_ID { get; set; }
        
        public bool CAN_ADD { get; set; }
        
        public bool CAN_VIEW { get; set; }
        
        public bool CAN_MODIFY { get; set; }
        
        public bool CAN_COMMIT { get; set; }
        
        public bool CAN_DELETE { get; set; }
        
        public bool CAN_PRINT { get; set; }
    }
}
