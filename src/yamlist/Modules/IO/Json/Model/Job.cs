using System.Collections.Generic;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Model
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

        [JsonProperty("merge_call")]
        public MergeCall MergeCall { get; set; }

        public override string ToString()
        {
            if (MergeCall == null)
            {
                return $"{nameof(Name)}: {Name}, {nameof(Serial)}: {Serial}";
            }
            
            return $"{nameof(MergeCall)}: {MergeCall}";
        }
    }
}