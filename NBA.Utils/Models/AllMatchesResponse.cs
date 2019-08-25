using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA.Utils.Models
{
    public class AllMatchesResponse
    {
        [JsonProperty("data")]
        public List<Match> data { get; set; }

        [JsonProperty("meta")]
        public Meta meta { get; set; }
    }
}