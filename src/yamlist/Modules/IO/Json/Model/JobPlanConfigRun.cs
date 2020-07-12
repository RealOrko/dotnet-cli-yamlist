using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class JobPlanConfigRun    
    {
        [JsonProperty("args")]
        public List<string> Args { get; set; }
        
        [JsonProperty("path")]
        public string Path { get; set; } 
    }
}