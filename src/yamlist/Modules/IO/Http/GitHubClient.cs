using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using yamlist.Modules.IO.Http.GitHubModel;
using yamlist.Modules.IO.Http.GitHubModel.Interfaces;
using yamlist.Modules.Versioning;

namespace yamlist.Modules.IO.Http
{
    public class GitHubClient
    {
        private static readonly HttpClient client;
        private static HttpClientHandler handler;

        static GitHubClient()
        {
            var args = CreateNewClient();
            client = args.Item2;
            handler = args.Item1;
        }

        public static List<ReleaseModel> GetReleases(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var uri = $"repos/{owner}/{repository}/releases";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<ReleaseModel>>(response);
        }

        public static ReleaseModel GetLatestRelease(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var uri = $"repos/{owner}/{repository}/releases/latest";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<ReleaseModel>(response);
        }

        public static List<TagModel> GetTags(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var uri = $"repos/{owner}/{repository}/tags";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<TagModel>>(response);
        }

        public static List<AssetModel> GetAssets(IHaveAssetsUrl assetsUrl)
        {
            if (assetsUrl == null) throw new ArgumentNullException(nameof(assetsUrl));

            var response = client
                .GetAsync(assetsUrl.AssetsUrl)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<AssetModel>>(response);
        }

        private static (HttpClientHandler, HttpClient) CreateNewClient(Action<HttpClientHandler> handlerCallback = null)
        {
            var httpHandler = new HttpClientHandler();
            handlerCallback?.Invoke(httpHandler);

            var httpClient = new HttpClient(httpHandler);
            httpClient.BaseAddress = new Uri("https://api.github.com");
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ghinstaller",
                Info.GetVersion()));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.inertia-preview+json"));

            CreateNewClientAuthorisationHeader(httpClient);

            return (httpHandler, httpClient);
        }

        private static void CreateNewClientAuthorisationHeader(HttpClient httpClient)
        {
            var token = Environment.GetEnvironmentVariable("GHI_TOKEN");
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static string Download(string url, string targetFileName)
        {
            var clientArgs = CreateNewClient(h => h.ClientCertificateOptions = ClientCertificateOption.Automatic);
            using var httpClient = clientArgs.Item2;
            httpClient.DefaultRequestHeaders.ExpectContinue = true;

            var fileInfo = new FileInfo($"{targetFileName}");
            using var file = File.Create(fileInfo.FullName);

            var response = clientArgs.Item2.GetAsync($"{url}", HttpCompletionOption.ResponseHeadersRead).GetAwaiter()
                .GetResult();
            response.EnsureSuccessStatusCode();

            var counter = 0;
            long? totalBytes = 0L;
            var totalBytesReadSoFar = 0;
            const int maxBytesReadableForBuffer = 8192;
            var buffer = new byte[maxBytesReadableForBuffer];
            using var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();

            while (true)
            {
                totalBytes = response.Content.Headers.ContentLength ?? 0L;

                var bytesRead = stream.Read(buffer, 0, maxBytesReadableForBuffer);
                if (bytesRead == 0) break;

                file.Write(buffer, 0, bytesRead);
                totalBytesReadSoFar += bytesRead;
                counter++;

                if (counter % 200 == 0) DownloadProgress(targetFileName, totalBytes, totalBytesReadSoFar);
            }

            DownloadProgress(targetFileName, totalBytes, totalBytesReadSoFar);
            System.Console.WriteLine("");

            return fileInfo.FullName;
        }

        private static void DownloadProgress(string targetFileName, long? totalBytes, int totalBytesReadSoFar)
        {
            System.Console.SetCursorPosition(0, System.Console.CursorTop);
            if (totalBytes.Value == 0)
            {
                System.Console.Write($"{targetFileName}: {totalBytesReadSoFar / 1024}/??? KB");
            }
            else
            {
                System.Console.Write($"{targetFileName}: {totalBytesReadSoFar / 1024}/{totalBytes / 1024} KB");
            }
        }

        private static T Deserialize<T>(HttpResponseMessage response, bool dumpContent = false)
        {
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (dumpContent) System.Console.WriteLine(content);

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Deserialize<ErrorModel>(content);
                System.Console.WriteLine($"{error.Message}, please see {error.Url}");
                return default;
            }

            return JsonSerializer.Deserialize<T>(content);
        }
    }
}