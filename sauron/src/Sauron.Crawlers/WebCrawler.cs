using RestSharp;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using Sauron.Crawlers.Extensions;
using System;
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
                    var rawContent = getResponseAsync.Result.Content;

                    return new RawData
                    {
                        Id = BuildRawDataId(source, filter, rawContent),
                        Url = GetUrl(source),
                        Filter = filter.AsQueryString(),
                        Visited = DateTimeOffset.Now,
                        RawContent = rawContent
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

        private string BuildRawDataId(string source, IFilter filter, string rawContent)
        {
            return $"{GetUrl(source)}{filter.AsQueryString()}{rawContent}".GetCrc();
        }

        private string GetUrl(string source) => $"{_restClient.BaseUrl}/{source}";
    }
}
