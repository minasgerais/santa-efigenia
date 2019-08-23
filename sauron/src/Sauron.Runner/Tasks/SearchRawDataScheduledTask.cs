using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers;
using Sauron.Runner.Extensions;
using Sauron.Scheduling.Tasks;
using System;
using System.Threading.Tasks;

namespace Sauron.Runner.Jobs
{
    public class SearchRawDataScheduledTask : EveryMonthScheduledTask
    {
        private const string ResourceName = "SAURON_CRAWLER_GLOBAL_SOURCE";
        private const string CollectionName = "SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION";

        private readonly IConfiguration _configuration;
        private readonly IWebCrawler<RawData> _webCrawler;
        private readonly IRawDataRepository _rawDataRepository;
        private readonly ILogger _logger;

        public SearchRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler,
            IRawDataRepository rawDataRepository, ILogger<SearchRawDataScheduledTask> logger)
        {
            (_configuration, _webCrawler, _rawDataRepository, _logger) = (configuration, webCrawler, rawDataRepository, logger);
        }

        public override async Task ExecuteAsync()
        {
            _logger.Stamp($"{nameof(SearchRawDataScheduledTask)} started.");

            var source = _configuration.TryGet(ResourceName);
            var collectionName = _configuration.TryGet(CollectionName);
            var filter = Filter.Create().AddParameter("data", DateTime.Now.ToString("MM/yyyy"));

            _logger.Stamp("Extracting raw data.");
            var rawData = await _webCrawler.ExtractAsync(source, filter);

            _logger.Stamp($"Saving raw data:", rawData);
            await _rawDataRepository.AddIfNotExistsAsync(collectionName, rawData);
        }
    }
}
