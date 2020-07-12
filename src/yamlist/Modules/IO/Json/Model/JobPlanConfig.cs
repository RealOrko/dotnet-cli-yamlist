using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class JobPlanConfig    
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }
        
        [JsonProperty("inputs")]
        public List<JobPlanConfigInputOutput> Inputs { get; set; }
        
        [JsonProperty("outputs")]
        public List<JobPlanConfigInputOutput> Outputs { get; set; }
        
        [JsonProperty("run")]
        public JobPlanConfigRun Run { get; set; }

        public override string ToString()
        {
            return $"{nameof(Platform)}: {Platform}, {nameof(Inputs)}: {Inputs}, {nameof(Outputs)}: {Outputs}, {nameof(Run)}: {Run}";
        }
    }
}