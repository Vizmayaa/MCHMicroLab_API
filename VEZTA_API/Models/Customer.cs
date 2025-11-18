using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class Country
    {
        public int ID { get; set; }

        public string COUNTRY_NAME { get; set; }

    }
    public class Customer
    {
        public int ID { get; set; }

        public string CUST_CODE { get; set; }

        public string CUST_NAME { get; set; }

        public string CONTACT_NAME { get; set; }

        public string ADDRESS1 { get; set; }

        public string ADDRESS2 { get; set; }

        public string ADDRESS3 { get; set; }

        public string ZIP { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }

        public string COUNTRY_ID { get; set; }

        public String COUNTRY_NAME { get; set; }

        public string PHONE { get; set; }

        public string EMAIL { get; set; }

        public DateTime? REGD_DATE { get; set; }
      

        public string VAT_REGN_NO { get; set; }
    }
   
}