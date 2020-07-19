using System.Collections.Generic;
using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model
{
    public class ResourceTypeSource : Dictionary<string, dynamic>
    {
        [JsonProperty("insecure_registries")]
        public List<string> Insecure_Registries { get; set; }
    }
}