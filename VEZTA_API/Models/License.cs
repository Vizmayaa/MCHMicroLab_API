using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class License
    {
        public int ID { get; set; }

        public string PRODUCT_ID { get; set; }

        public string CUST_ID { get; set; }

        public string LICENSETYPE_ID { get; set; }

        //public DateTime? INSTALL_DATE { get; set; }
       
        //public DateTime? EXPIRY_DATE { get; set; }

        public string VALID_DAYS { get; set; }

        public string LICENSE_KEY { get; set; }
        
        public string PRODUCT_NAME { get; set; }
        public string LICENSETYPES { get; set; }
        public string CUST_NAME { get; set; }


        public int SERIAL_NO { get; set; }


        public class Customerss
        {
            public int ID { get; set; }

            public string CUST_NAME { get; set; }
        }
        public class Pro
        {
            public int ID { get; set; }

            public string PRODUCT_NAME { get; set; }
        }
        public class licenseTypess
        {
            public int ID { get; set; }

            public string LICENSETYPES { get; set; }
        }
    }
}