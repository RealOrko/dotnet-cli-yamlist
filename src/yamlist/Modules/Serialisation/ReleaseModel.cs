using System;
using System.Text.Json.Serialization;
using yamlist.Modules.Serialisation.Interfaces;

namespace yamlist.Modules.Serialisation
{
    public class ReleaseModel : IHaveAssetsUrl
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("draft")]
        public bool Draft { get; set; }
        
        [JsonPropertyName("prerelease")]
        public bool Prerelease { get; set; }
        
        [JsonPropertyName("body")]
        public string Body { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("assets_url")]
        public string AssetsUrl { get; set; }
        
        [JsonPropertyName("zipball_url")]
        public string ZipBallUrl { get; set; }

        [JsonPropertyName("tarball_url")]
        public string TarBallUrl { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        public override string ToString()
        {
            return $"{nameof(TagName)}: {TagName}, {nameof(Url)}: {Url}, {nameof(AssetsUrl)}: {AssetsUrl}";
        }
    }
}