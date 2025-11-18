using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class Stock
    {        
        public String productSKU { get; set; }         
        public string quantity { get; set; }           
    }
     
    public class StockResponse
    {        
        public string flag { get; set; }
        public string message { get; set; }        
    }
}