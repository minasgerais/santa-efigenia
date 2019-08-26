using RestSharp;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Factories;
using Sauron.Abstractions.Models;
using Sauron.Crawlers.Extensions;
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
                    var rawContent = getResponseAsync.Result.GetContent();
                    return RawDataFactory.CreateRawData(GetUrl(source), filter, rawContent);
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

        private string GetUrl(string source) => $"{_restClient.BaseUrl}/{source}";
    }
}
