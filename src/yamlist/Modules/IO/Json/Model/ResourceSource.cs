using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class ResourceSource : Dictionary<string, dynamic>
    {
        [JsonProperty("ignore_paths")]
        public List<string> IgnorePaths { get; set; }
    }
}