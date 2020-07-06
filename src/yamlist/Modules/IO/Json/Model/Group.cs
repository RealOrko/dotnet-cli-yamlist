using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("jobs")]
        public List<string> Jobs { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}