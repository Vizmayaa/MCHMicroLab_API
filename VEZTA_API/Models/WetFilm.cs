using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class WetFilmMaster
    {
        public int? ID { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class WetFilmResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public List<WetFilmMaster> Data { get; set; }
    }


}
