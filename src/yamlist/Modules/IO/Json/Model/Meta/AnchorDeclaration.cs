using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json.Model.Meta
{
    public class AnchorDeclaration
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        public override string ToString()
        {
            return $"{nameof(Method)}: {Method}";
        }
    }
}