using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class SpecimenNature
    {
        public int? ID { get; set; }

        public string SPECIMEN_NAME { get; set; }

    }

    public class SpecimenNatureResponse
    {
        public int flag { get; set; }

        public string Message { get; set; }

        public List<SpecimenNature> Data { get; set; }
    }


}