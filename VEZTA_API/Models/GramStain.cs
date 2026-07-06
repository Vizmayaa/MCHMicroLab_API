using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class GramStainMaster
    {
        public int? ID { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class GramStainResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<GramStainMaster> Data { get; set; }
    }


}
