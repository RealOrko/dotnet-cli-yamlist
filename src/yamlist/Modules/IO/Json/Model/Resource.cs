using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class Resource
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("check_every")]
        public string CheckEvery { get; set; }
        
        [JsonProperty("source")]
        public ResourceSource Source { get; set; }
    }
}