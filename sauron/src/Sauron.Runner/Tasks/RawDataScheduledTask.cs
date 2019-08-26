using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Runner.Extensions;
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
            ILogger<RawDataScheduledTask> logger) : base(configuration)
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
            Stamp($"{GetType().Name} started.");

            var filters = await ExtractFiltersAsync();

            foreach (var item in filters)
            {
                Stamp("Extracting raw data.");
                var rawData = await ExtractRawDataAsync(item);

                if (await AddRawDataIfNotExtistAsync(rawData))
                {
                    Stamp($"Raw data saved:", rawData);
                }
                else
                {
                    Stamp($"raw data already exists");
                }
            }

            Stamp($"{GetType().Name} ended.");
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
