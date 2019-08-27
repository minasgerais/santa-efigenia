using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sauron.Runner.Tasks
{
    public class DetailRawDataScheduledTask : RawDataScheduledTask
    {
        private const string SourceConfigKey = "SAURON_CRAWLER_DETAIL_SOURCE";
        private const string GlobalCollectionConfigKey = "SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION";
        private const string CollectionConfigKey = "SAURON_MONGO_DB_DATABASE_DETAIL_COLLECTION";

        protected override string Source { get => Configuration.TryGet(SourceConfigKey); }
        protected override string Collection { get => Configuration.TryGet(CollectionConfigKey); }

        public DetailRawDataScheduledTask(IConfiguration configuration, IWebCrawler<RawData> webCrawler, IRawDataRepository rawDataRepository,
            ILogger<RawDataScheduledTask> logger) : base(configuration, webCrawler, rawDataRepository, logger)
        { }

        public override async Task<List<IFilter>> ExtractFiltersAsync()
        {
            var result = new List<IFilter>();

            var rawData = await GetAllRawDataAsync();

            foreach (var data in rawData)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(data.RawContent);

                var nodes = htmlDoc.DocumentNode
                    .SelectNodes("//table/tbody/tr/td[@data-title='Detalhamento']/a");

                if (nodes != default)
                {
                    var hashTable = nodes
                        .Select(node => node.Attributes["data-codvereador"]?.Value)
                        .ToList();

                    foreach (var hash in hashTable)
                        result.Add(
                                Filter.Create()
                                    .AddParameter("data", data.Filter.Split('=').GetValue(1)) // TODO: remover o split por um regex.
                                    .AddParameter("codVereador", hash)
                            );
                }
            }

            return result;
        }

        protected override Task<List<RawData>> GetAllRawDataAsync()
        {
            return GetAllRawDataFromAnotherCollectionAsync(
                    Configuration.TryGet(GlobalCollectionConfigKey)
                );
        }
    }
}
