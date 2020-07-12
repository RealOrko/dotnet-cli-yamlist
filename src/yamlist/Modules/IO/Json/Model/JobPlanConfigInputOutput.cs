using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class JobPlanConfigInputOutput
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}