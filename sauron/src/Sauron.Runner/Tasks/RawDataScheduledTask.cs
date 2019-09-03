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

namespace Sauron.Runner.Tasks
{
    public abstract class RawDataScheduledTask : ScheduledTask
    {
        private readonly IWebCrawler<RawData> _webCrawler;
        private readonly IRawDataRepository _rawDataRepository;
        private readonly ILogger _logger;

        protected IConfiguration Configuration { get; }

        protected override string Scheduler { get => "SAURON_TASK_SCHEDULER_DAYS_INTERVAL"; }

        protected abstract string Source { get; }

        protected abstract string Collection { get; }

        protected RawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<RawDataScheduledTask> logger) : base(configuration, logger)
        {
            (Configuration, _webCrawler, _rawDataRepository, _logger) = (configuration, webCrawler, rawDataRepository, logger);
        }

        public abstract Task<List<IFilter>> ExtractFiltersAsync();

        protected void Stamp(string message)
        {
            _logger.Stamp(message);
        }

        protected void Stamp<T>(string message, T obj)
        {
            _logger.Stamp(message, obj);
        }

        public override async Task ExecuteAsync()
        {
            await Transaction.Capture(Name, "ScheduledTask", async (currentTransaction) =>
            {
                var filters = await ExtractFiltersAsync();

                foreach (var item in filters)
                    await ProcessFilterAsync(item, currentTransaction);
            });
        }

        private Task ProcessFilterAsync(IFilter filter, ITransaction transaction)
        {
            return transaction.CaptureSpan("ProcessFilterAsync", "AsyncMethod", async (span) =>
            {
                Stamp("Extracting raw data.");
                var rawData = await ExtractRawDataAsync(filter);

                Stamp("APM logging raw data.");
                foreach (var kv in rawData.ToDictionary())
                    span.Context.Labels[kv.Key] = kv.Value;

                if (await AddRawDataIfNotExtistAsync(rawData))
                {
                    Stamp($"Raw data saved:", rawData);
                    span.Context.Labels["Saved"] = "True";
                }
                else
                {
                    span.Context.Labels["Saved"] = "False";
                    Stamp($"raw data already exists");
                }
            });
        }

        protected Task<RawData> ExtractRawDataAsync(IFilter filter)
        {
            return _webCrawler.ExtractAsync(Source, filter);
        }

        protected Task<bool> AddRawDataIfNotExtistAsync(RawData rawData)
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
