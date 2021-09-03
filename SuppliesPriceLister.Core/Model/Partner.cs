using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuppliesPriceLister.Core.Model
{
    public class Partner
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("partnerType")]
        public string Type { get; set; }
        [JsonProperty("partnerAddress")]
        public string Address { get; set; }
        [JsonProperty("supplies")]
        public IEnumerable<Supply> Supplies { get; set; }
    }
}