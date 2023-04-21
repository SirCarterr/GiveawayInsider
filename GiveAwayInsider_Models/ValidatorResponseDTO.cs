using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class ValidatorResponseDTO
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("mx")]
        public bool Mx { get; set; }

        [JsonProperty("disposable")]
        public bool Disposable { get; set; }

        [JsonProperty("did_you_mean")]
        public string? DidYouMean { get; set; }
    }
}
