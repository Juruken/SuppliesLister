using System;
using Newtonsoft.Json;

namespace SuppliesPriceLister.Core.Model
{
    public class Supply
    {
        public string Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("uom")]
        public string Unit { get; set; }
        [JsonProperty("priceInCents")]
        public decimal Price { get; set; }
        [JsonProperty("materialType")]
        public string MaterialType { get; set; }
        [JsonProperty("providerId")]
        public Guid? ProviderId { get; set; }
    }
}