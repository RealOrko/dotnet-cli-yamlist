using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model.Meta
{
    public class MergeCall
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("method")]
        public string Method { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Method)}: {Method}";
        }
    }
}