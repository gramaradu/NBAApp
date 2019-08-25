using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA.Utils.Models
{
    public class AllPlayersResponse
    {
            [JsonProperty("data")]
            public List<Player> Data { get; set; }

            [JsonProperty("meta")]
           public Meta Meta { get; set; }
    }
}