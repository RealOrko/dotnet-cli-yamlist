using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    // Need custom serializer
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

    // Need custom serialiser
    public class ResourceSource : Dictionary<string, string>
    {
        [JsonProperty("ignore_paths")]
        public List<string> IgnorePaths { get; set; }
    }
}