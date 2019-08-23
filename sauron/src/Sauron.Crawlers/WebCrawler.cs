using RestSharp;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sauron.Crawlers
{
    public class WebCrawler : IWebCrawler<RawData>
    {
        private readonly IRestClient _restClient;

        public WebCrawler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<RawData> ExtractAsync(string source, IFilter filter)
        {
            return GetResponseAsync(_restClient, source, filter)
                .ContinueWith((getResponseAsync) =>
                {
                    return new RawData
                    {
                        Id = BuildRawDataId(source, filter),
                        Url = GetUrl(source),
                        Filter = filter.AsQueryString(),
                        Visited = DateTimeOffset.Now,
                        RawContent = getResponseAsync.Result.Content
                    };
                });
        }

        private Task<IRestResponse> GetResponseAsync(IRestClient cliente, string source, IFilter filter)
        {
            return Task.Run(() =>
            {
                var request = new RestRequest(source, Method.POST);

                foreach (var item in filter)
                    request.AddParameter(item.Key, item.Value);

                return cliente.Execute(request);
            });
        }

        private string BuildRawDataId(string source, IFilter filter)
        {
            var url = GetUrl(source);
            var query = filter.AsQueryString();
            return GetMd5Hash(!string.IsNullOrWhiteSpace(query) ? $"{url}?{query}" : url);
        }

        private string GetUrl(string source) => $"{_restClient.BaseUrl}/{source}";

        private string GetMd5Hash(string input)
        {
            using (var provider = MD5.Create())
            {
                return string.Join("",
                        provider
                            .ComputeHash(Encoding.UTF8.GetBytes(input))
                            .Select(s => s.ToString("x2"))
                    );
            }
        }
    }
}
