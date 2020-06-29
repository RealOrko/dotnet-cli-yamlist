using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Yaml.Model
{
    public class Pipeline
    {
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
        
        [JsonProperty("meta")]
        public Dictionary<string, dynamic> Meta { get; set; }
        
        [JsonProperty("resource_types")]
        public List<ResourceType> ResourceTypes { get; set; }
        
        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
        
        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }
    }
}