using Elastic.Apm.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Apm.Tracing;
using Sauron.Scheduling.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sauron.Abstractions.Apm.Tracing;

namespace Sauron.Runner.Tasks
{
    public abstract class RawDataScheduledTask : ScheduledTask
    {
        private readonly IWebCrawler<RawData> _webCrawler;
        private readonly IRawDataRepository _rawDataRepository;
        private readonly ILogger _logger;
        private readonly IMonitor _monitor;

        protected IConfiguration Configuration { get; }

        protected override string Scheduler => "SAURON_TASK_SCHEDULER_DAYS_INTERVAL";

        protected abstract string Source { get; }

        protected abstract string Collection { get; }

        protected RawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<RawDataScheduledTask> logger, IMonitor monitor) : base(configuration, logger)
        {
            (Configuration, _webCrawler, _rawDataRepository, _logger, _monitor) = (configuration, webCrawler, rawDataRepository, logger, monitor);
        }

        protected abstract Task<List<IFilter>> ExtractFiltersAsync();

        public override async Task ExecuteAsync()
        {
            await _monitor.Start(Name, "ScheduledTask", async () =>
            {
                var filters = await ExtractFiltersAsync();

                foreach (var item in filters)
                    await ProcessFilterAsync(item);
            });
        }

        private Task ProcessFilterAsync(IFilter filter)
        {
            return _monitor.InspectMoment("ProcessFilterAsync", "AsyncMethod", async (moment) =>
            {
                _logger.Stamp("Extracting raw data.");
                var rawData = await ExtractRawDataAsync(filter);

                _logger.Stamp("APM logging raw data.");
                foreach (var (key, value) in rawData.ToDictionary())
                    moment.AddProperty(key, value);

                if (await AddRawDataIfNotExtistAsync(rawData))
                {
                    moment.AddProperty("Saved", "True");
                    _logger.Stamp($"Raw data saved:", rawData);
                }
                else
                {
                    moment.AddProperty("Saved", "False");
                    _logger.Stamp($"raw data already exists");
                }

                return moment;
            });
        }

        private Task<RawData> ExtractRawDataAsync(IFilter filter)
        {
            return _webCrawler.ExtractAsync(Source, filter);
        }

        private Task<bool> AddRawDataIfNotExtistAsync(RawData rawData)
        {
            return _rawDataRepository.AddIfNotExistsAsync(Collection, rawData);
        }

        protected virtual Task<List<RawData>> GetAllRawDataAsync()
        {
            return _rawDataRepository.GetAllAsync(Collection);
        }

        protected Task<List<RawData>> GetAllRawDataFromAnotherCollectionAsync(string collection)
        {
            return _rawDataRepository.GetAllAsync(collection);
        }
    }
}