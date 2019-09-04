using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sauron.Abstractions.Apm.Tracing;

namespace Sauron.Runner.Tasks
{
    public class GlobalRawDataScheduledTask : RawDataScheduledTask
    {
        private const string CrawlerInitialYearConfigKey = "SAURON_CRAWLER_INITIAL_YEAR";
        private const string SourceConfigKey = "SAURON_CRAWLER_GLOBAL_SOURCE";
        private const string CollectionConfigKey = "SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION";

        protected override string Source => Configuration.TryGet(SourceConfigKey);

        protected override string Collection => Configuration.TryGet(CollectionConfigKey);

        protected override string Name => "GLOBAL RAW DATA EXTRACTOR";

        public GlobalRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<RawDataScheduledTask> logger, IMonitor monitor) : base(configuration, webCrawler, rawDataRepository, logger, monitor)
        {
        }

        protected override Task<List<IFilter>> ExtractFiltersAsync()
        {
            IEnumerable<IFilter> ExtractFilter()
            {
                var currentDate = DateTime.Now;
                var year = int.Parse(Configuration.TryGet(CrawlerInitialYearConfigKey) ?? $"{currentDate.Year}");

                while (year <= currentDate.Year)
                {
                    for (var i = 1; i <= 12; ++i)
                    {
                        if (year == currentDate.Year && i > currentDate.Month)
                            break;

                        yield return Filter.Create().AddParameter("data", $"{i.ToString().PadLeft(2, '0')}/{year}");
                    }

                    ++year;
                }
            }

            return Task.Run(() => ExtractFilter().ToList());
        }
    }
}