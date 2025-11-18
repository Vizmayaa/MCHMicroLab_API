using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class UserReport
    {
        public int ID { get; set; }       
        public int USER_ID { get; set; }     
        public string REPORT_ID { get; set; }
        public DateTime? CREATED_TIME { get; set; }      
        public DateTime? MODIFIED_TIME { get; set; }
        public string USER_REPORT_NAME { get; set; }
        public List<USERREPORT_COLUMNS> columns { get; set; }
        public List<USERREPORT_PARAMETERS> parameters { get; set; }
        public List<ReportAdvanceFilterColumns> advancefilter {get; set; }
}
    public class USERREPORT_COLUMNS
    {
        public int ID { get; set; }  
        //public int USER_REPORT_ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ToolTip { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public string Summary { get; set; }
        public string Visibility { get; set; }

    }
   
    public class USERREPORT_PARAMETERS
    {
        public int ID { get; set; }
        //public int USER_REPORT_ID { get; set; }
        public string SEARCH_ON { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string ENCOUNTER_TYPE { get; set; }
        public string FACILITY_ID { get; set; }
        public string SENDER_ID { get; set; }
        public string RECEIVER_ID { get; set; }
        public string CLINICIAN { get; set; }

    }
    public class UserReportResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
 

    }
}
