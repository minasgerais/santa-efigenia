using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sauron.Runner.Tasks
{
    public class GlobalRawDataScheduledTask : RawDataScheduledTask
    {
        private const string CrawlerInitialYearConfigKey = "SAURON_CRAWLER_INITIAL_YEAR";
        private const string SourceConfigKey = "SAURON_CRAWLER_GLOBAL_SOURCE";
        private const string CollectionConfigKey = "SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION";

        protected override string Source { get => Configuration.TryGet(SourceConfigKey); }
        protected override string Collection { get => Configuration.TryGet(CollectionConfigKey); }

        public GlobalRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<RawDataScheduledTask> logger) : base(configuration, webCrawler, rawDataRepository, logger)
        { }

        public override async Task ExecuteAsync()
        {
            Stamp($"{nameof(GlobalRawDataScheduledTask)} started.");

            Stamp("Extracting filters.");
            var filters = ExtractFilter();

            foreach (var item in filters)
            {
                Stamp("Extracting raw data.");
                var rawData = await ExtractRawDataAsync(item);

                Stamp($"Saving raw data:", rawData);
                await AddRawDataIfNotExtistAsync(rawData);
            }

            Stamp($"ended.");
        }

        private IEnumerable<IFilter> ExtractFilter()
        {
            var currentDate = DateTime.Now;
            var initialYear = int.Parse(Configuration.TryGet(CrawlerInitialYearConfigKey) ?? $"{currentDate.Year}");

            while (initialYear <= currentDate.Year)
            {
                for (int i = 1; i <= 12; ++i)
                {
                    if (initialYear == currentDate.Year && i > currentDate.Month)
                        break;

                    yield return Filter.Create().AddParameter("data", $"{i.ToString().PadLeft(2, '0')}/{initialYear}");
                }

                ++initialYear;
            }
        }
    }
}
