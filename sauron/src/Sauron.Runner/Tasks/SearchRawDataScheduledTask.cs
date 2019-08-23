using Microsoft.Extensions.Configuration;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers;
using Sauron.Scheduling.Tasks;
using System;
using System.Threading.Tasks;

namespace Sauron.Runner.Jobs
{
    public class SearchRawDataScheduledTask : ScheduledTask
    {
        private const string ResourceName = "SAURON_CRAWLER_SEARCH_RESOURCE";
        private const string CollectionName = "SAURON_MONGO_DB_DATABASE_SEARCH_COLLECTION";

        private readonly IConfiguration _configuration;
        private readonly IWebCrawler<RawData> _webCrawler;
        private readonly IRawDataRepository _rawDataRepository;

        public SearchRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository)
        {
            (_configuration, _webCrawler, _rawDataRepository) = (configuration, webCrawler, rawDataRepository);
        }

        public override async Task ExecuteAsync()
        {
            var source = _configuration.TryGet(ResourceName);
            var collectionName = _configuration.TryGet(CollectionName);

            var filter = Filter.Create()
                .AddParameter("paginaRequerida", 1)
                .AddParameter("data", DateTime.Now.ToString("MM/yyyy"));

            var rawData = await _webCrawler.ExtractAsync(source, filter);

            await _rawDataRepository.AddAsync(collectionName, rawData);
        }
    }
}
