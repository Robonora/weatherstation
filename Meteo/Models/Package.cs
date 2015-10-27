using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meteo.Models
{
    public class Package
    {
        [JsonProperty("future")]
        public List<JsonCard> Future { get; set; }
        [JsonProperty("past")]
        public List<JsonCard> Past { get; set; }
        [JsonProperty("all")]
        public List<JsonCard> All { get; set; }
        [JsonProperty("today")]
        public TodayCard Today { get; set; }

    }
}