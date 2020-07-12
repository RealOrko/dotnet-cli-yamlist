using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class Pipeline
    {
        [JsonProperty("anchors")]
        public Dictionary<string, dynamic> Anchors { get; set; }
        
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
        
        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }

        [JsonProperty("resource_types")]
        public List<ResourceType> ResourceTypes { get; set; }
        
        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }
}