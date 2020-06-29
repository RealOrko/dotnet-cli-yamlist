using Newtonsoft.Json;

namespace yamlist.Modules.IO.Yaml.Model
{
    public class ResourceType
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source")]
        public ResourceTypeSource Source { get; set; }
    }
}