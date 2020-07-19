using System.Collections.Generic;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Model
{
    public class JobPlanConfigImageResourceSource : Dictionary<string, dynamic>
    {
    }
    
    public class JobPlanConfigImageResource
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("source")] 
        public JobPlanConfigImageResourceSource Source { get; set; }
    }
    
    public class JobPlanConfig    
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }
        
        [JsonProperty("inputs")]
        public List<JobPlanConfigInputOutput> Inputs { get; set; }
        
        [JsonProperty("outputs")]
        public List<JobPlanConfigInputOutput> Outputs { get; set; }
        
        [JsonProperty("image_resource")]
        public JobPlanConfigImageResource ImageResource { get; set; }

        [JsonProperty("run")]
        public JobPlanConfigRun Run { get; set; }
        
        [JsonProperty("params")]
        public Dictionary<string, dynamic> Params { get; set; }
        
        [JsonProperty("params_anchor")]
        public AnchorDeclaration ParamsAnchorDeclaration { get; set; }

        public override string ToString()
        {
            return $"{nameof(Platform)}: {Platform}, {nameof(Inputs)}: {Inputs}, {nameof(Outputs)}: {Outputs}, {nameof(Run)}: {Run}";
        }
    }
}