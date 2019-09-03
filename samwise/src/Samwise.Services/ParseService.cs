using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Models;
using Samwise.Abstractions.Parsers;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Services;
using Samwise.Repositories.MongoDB;

namespace Samwise.Services
{
    public class ParseService<TDataInput, TResult> : IParseService
    {
        private const string ConfigurationDatabaseCollectionSamwise = "SAMWISE_MONGO_DB_DATABASE_COLLECTION";
        private const string ConfigurationDatabaseCollectionSauron = "SAURON_MONGO_DB_DATABASE_COLLECTION";

        private readonly IParseData<TDataInput, TResult> _parseData;
        private readonly IDataRepository<SamwiseDataRepository> _samwiseDataRepository;
        private readonly IDataRepository<SauronDataRepository> _sauronDataRepository;
        private readonly HtmlDocument _htmlDocument;
        private readonly string _databaseCollectionSamwise;
        private readonly string _databaseCollectionSauron;

        public ParseService(IConfiguration configuration, IParseData<TDataInput, TResult> parseData,
            IDataRepository<SamwiseDataRepository> samwiseDataRepository,
            IDataRepository<SauronDataRepository> sauronDataRepository)
        {
            _parseData = parseData;
            _samwiseDataRepository = samwiseDataRepository;
            _sauronDataRepository = sauronDataRepository;
            _databaseCollectionSamwise = configuration.TryGet(ConfigurationDatabaseCollectionSamwise);
            _databaseCollectionSauron = configuration.TryGet(ConfigurationDatabaseCollectionSauron);
            _htmlDocument = new HtmlDocument();
        }

        public async Task ExecuteParseAsync()
        {
            var listRawData = await _sauronDataRepository.GetAllAsync<RawData>(_databaseCollectionSauron, lnq => lnq.Parsed == default);
        }
    }
}