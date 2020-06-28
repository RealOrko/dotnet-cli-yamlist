using System.Text.Json.Serialization;

namespace yamlist.Modules.IO.Http.GitHubModel
{
    public class CommitModel
    {
        [JsonPropertyName("sha")] public string Sha1 { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; }
    }
}