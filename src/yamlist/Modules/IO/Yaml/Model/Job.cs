using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Yaml.Model
{
    public class Job
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("serial")]
        public bool Serial { get; set; }
        
        [JsonProperty("plan")]
        public List<JobPlan> Plan { get; set; }
        
        [JsonProperty("serial_groups")]
        public List<string> SerialGroups { get; set; }
    }
}