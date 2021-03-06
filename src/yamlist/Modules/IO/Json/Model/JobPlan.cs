using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Converters;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Model
{
    [JsonConverter(typeof(JobPlanConverter))]
    public class JobPlan 
    {
        [JsonProperty("try")]
        public JobPlan Try { get; set; }
        
        [JsonProperty("task")]
        public string Task { get; set; }
        
        [JsonProperty("config")]
        public JobPlanConfig Config { get; set; }
        
        [JsonProperty("set_pipeline")]
        public string SetPipeline { get; set; }
        
        [JsonProperty("var_files")]
        public List<string> VarFiles { get; set; }
        
        [JsonProperty("get")]
        public string Get { get; set; }
        
        [JsonProperty("put")]
        public string Put { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("attempts")]
        public int Attempts { get; set; }
        
        [JsonProperty("passed")]
        public List<string> Passed { get; set; }
        
        [JsonProperty("trigger")]
        public string Trigger { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }
        
        [JsonProperty("file")]
        public string File { get; set; }
        
        [JsonProperty("input_mapping")]
        public Dictionary<string, string> InputMapping { get; set; }
        
        [JsonProperty("output_mapping")]
        public Dictionary<string, string> OutputMapping { get; set; }
        
        [JsonProperty("in_parallel")]
        public List<JobPlan> InParallel { get; set; }

        [JsonProperty("do")]
        public List<JobPlan> Do { get; set; }
        
        [JsonProperty("params")]
        public Dictionary<string, dynamic> Params { get; set; }

        [JsonProperty("get_params")]
        public Dictionary<string, dynamic> GetParams { get; set; }

        [JsonProperty("put_params")]
        public Dictionary<string, dynamic> PutParams { get; set; }

        [JsonProperty("ensure")]
        public JobPlanEnsure Ensure { get; set; }

        [JsonProperty("merge_call")]
        public MergeCall MergeCall { get; set; }
        
        [JsonProperty("anchor_call")]
        public AnchorCall AnchorCall { get; set; }

        [JsonProperty("config_anchor_call")]
        public AnchorCall ConfigAnchorCall { get; set; }
        
        [JsonProperty("config_anchor_declaration")]
        public AnchorDeclaration ConfigAnchorDeclaration { get; set; }

        [JsonProperty("task_anchor_declaration")]
        public AnchorDeclaration TaskAnchorDeclaration { get; set; }



        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (InParallel != null && InParallel.Count > 0)
            {
                stringBuilder.Append("Parallel");
                return stringBuilder.ToString();
            }

            if (!string.IsNullOrEmpty(Task))
            {
                stringBuilder.Append($"{nameof(Task)}: {Task},");
            }
            
            if (!string.IsNullOrEmpty(Get))
            {
                stringBuilder.Append($"{nameof(Get)}: {Get},");
            }
            
            if (!string.IsNullOrEmpty(Put))
            {
                stringBuilder.Append($"{nameof(Put)}: {Put},");
            }
            
            if (!string.IsNullOrEmpty(File))
            {
                stringBuilder.Append($"{nameof(File)}: {File},");
            }
            
            stringBuilder.Append($"{nameof(Trigger)}: {Trigger},");
            
            if (Attempts > 0)
            {
                stringBuilder.Append($"{nameof(Attempts)}: {Attempts},");
            }
            
            if (Passed != null && Passed.Count > 0)
            {
                stringBuilder.Append($"{nameof(Passed)}: {string.Join(",", Passed)},");
            }

            return stringBuilder.ToString();
        }
    }
}