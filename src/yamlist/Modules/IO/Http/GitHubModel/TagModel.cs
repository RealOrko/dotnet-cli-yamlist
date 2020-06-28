using System.Text.Json.Serialization;

namespace yamlist.Modules.IO.Http.GitHubModel
{
    public class TagModel
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("zipball_url")] public string ZipBallUrl { get; set; }

        [JsonPropertyName("tarball_url")] public string TarBallUrl { get; set; }

        [JsonPropertyName("commit")] public CommitModel Commit { get; set; }
    }
}