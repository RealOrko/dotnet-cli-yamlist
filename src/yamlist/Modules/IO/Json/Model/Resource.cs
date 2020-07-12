using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Model.Meta;

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

        [JsonProperty("anchor_call")]
        public AnchorCall SourceAnchorCall { get; set; }
    }
}