using System.Text.Json.Serialization;

namespace yamlist.Modules.IO.Http.GitHubModel
{
    public class AssetModel
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; }

        [JsonPropertyName("browser_download_url")]
        public string DownloadUrl { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(DownloadUrl)}: {DownloadUrl}";
        }
    }
}