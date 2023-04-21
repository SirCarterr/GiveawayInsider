using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class GiveawayDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("worth")]
        public string Worth { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("open_giveaway_url")]
        public string Open_Giveaway_Url { get; set; }

        [JsonProperty("published_date")]
        public string Published_Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("platforms")]
        public string Platforms { get; set; }

        [JsonProperty("end_date")]
        public string End_Date { get; set; }

        [JsonProperty("users")]
        public long Users { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("gamerpower_url")]
        public string? Gamer_power_Url { get; set; }

        [JsonProperty("open_giveaway")]
        public string Open_Giveaway { get; set; }
    }
}
