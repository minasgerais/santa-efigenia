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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sauron.Runner.Jobs
{
    public class SearchRawDataScheduledTask : ScheduledTask
    {
        private const string SourceConfigKey = "SAURON_CRAWLER_GLOBAL_SOURCE";
        private const string CollectionConfigKey = "SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION";
        private const string CrawlerInitialYearConfigKey = "SAURON_CRAWLER_INITIAL_YEAR";

        private readonly IConfiguration _configuration;
        private readonly IWebCrawler<RawData> _webCrawler;
        private readonly IRawDataRepository _rawDataRepository;
        private readonly ILogger _logger;

        protected override string Scheduler { get => "SAURON_TASK_SCHEDULER_DAYS_INTERVAL"; }

        public SearchRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<SearchRawDataScheduledTask> logger) : base(configuration)
        {
            (_configuration, _webCrawler, _rawDataRepository, _logger) = (configuration, webCrawler, rawDataRepository, logger);
        }

        public override async Task ExecuteAsync()
        {
            _logger.Stamp($"{nameof(SearchRawDataScheduledTask)} started.");

            var searchInterval = CalculateMonthYearInterval();

            foreach (var item in searchInterval)
            {
                _logger.Stamp("Extracting raw data.");
                var rawData = await ExtractRawDataAsync(item);

                _logger.Stamp($"Saving raw data:", rawData);
                await AddRawDataIfNotExtistAsync(rawData);
            }
        }

        private IEnumerable<string> CalculateMonthYearInterval()
        {
            var currentDate = DateTime.Now;
            var initialYear = int.Parse(_configuration.TryGet(CrawlerInitialYearConfigKey) ?? $"{currentDate.Year}");

            while (initialYear <= currentDate.Year)
            {
                for (int i = 1; i <= 12; ++i)
                {
                    if (initialYear == currentDate.Year && i > currentDate.Month)
                        break;

                    yield return $"{i.ToString().PadLeft(2, '0')}/{initialYear}";
                }

                ++initialYear;
            }
        }

        private Task<RawData> ExtractRawDataAsync(string monthYear)
        {
            var source = _configuration.TryGet(SourceConfigKey);
            var filter = Filter.Create().AddParameter("data", monthYear);
            return _webCrawler.ExtractAsync(source, filter);
        }

        private Task AddRawDataIfNotExtistAsync(RawData rawData)
        {
            return _rawDataRepository.AddIfNotExistsAsync(_configuration.TryGet(CollectionConfigKey), rawData);
        }
    }
}
