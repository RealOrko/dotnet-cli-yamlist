using System.Text.Json.Serialization;

namespace yamlist.Modules.IO.Http.GitHub.Model
{
    public class ErrorModel
    {
        [JsonPropertyName("message")] public string Message { get; set; }

        [JsonPropertyName("documentation_url")]
        public string Url { get; set; }
    }
}