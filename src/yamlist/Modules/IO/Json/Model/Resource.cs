using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class Resource
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("source")]
        public dynamic Source { get; set; }
    }
}