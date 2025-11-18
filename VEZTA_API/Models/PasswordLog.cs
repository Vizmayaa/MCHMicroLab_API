using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class PasswordLog
    {
        public int ID { get; set; }         
        public string PresentPassword { get; set; } 
        public string NewPassword { get; set; }
        public string ModifiedFrom { get; set; }
        public int PasswordRepeatCycle { get; set; }
    }
    public class PasswordLogResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<PasswordLog> data { get; set; }

    }
}