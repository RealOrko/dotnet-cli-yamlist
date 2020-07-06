using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Converters;

namespace yamlist.Modules.IO.Json.Model
{
    [JsonConverter(typeof(JobPlanConverter))]
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