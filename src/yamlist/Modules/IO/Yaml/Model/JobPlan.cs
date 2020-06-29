using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Yaml.Model
{
    public class JobPlan 
    {
        [JsonProperty("get")]
        public string Get { get; set; }
        
        [JsonProperty("put")]
        public string Put { get; set; }
        
        [JsonProperty("attempts")]
        public int Attempts { get; set; }
        
        [JsonProperty("passed")]
        public List<string> Passed { get; set; }
        
        [JsonProperty("trigger")]
        public bool Trigger { get; set; }
        
        [JsonProperty("task")]
        public string Task { get; set; }
        
        [JsonProperty("file")]
        public string File { get; set; }
        
        [JsonProperty("params")]
        public Dictionary<string, dynamic> Params { get; set; }
        
        [JsonProperty("input_mapping")]
        public Dictionary<string, string> InputMapping { get; set; }
        
        [JsonProperty("output_mapping")]
        public Dictionary<string, string> OutputMapping { get; set; }
        
        [JsonProperty("in_parallel")]
        public List<JobPlan> InParallel { get; set; }
    }
}