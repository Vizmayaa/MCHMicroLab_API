using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class InsuranceCompany
    {
        public int ID { get; set; }
      
        public string InsuranceID { get; set; }
        public int ClassificationID { get; set; }
        public string Classification { get; set; }

        public string InsuranceName { get; set; }
        
        public string InsuranceShortName { get; set; }
        public bool IsInActive { get; set; }
         public bool IsDeleted { get; set; }

    }
    public class InsuranceCompanyResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
       
        public List<InsuranceCompany> data { get; set; }
    }
}