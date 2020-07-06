using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class ResourceTypeSource
    {
        [JsonProperty("repository")]
        public string Repository { get; set; }
        
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }
}