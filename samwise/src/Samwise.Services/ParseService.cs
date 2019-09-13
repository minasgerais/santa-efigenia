using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Models;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Services;
using Samwise.Repositories.MongoDB;

namespace Samwise.Services
{
    public class ParseService : IParseService<HtmlDocument, CamaraMunicipalCusteioParlamentar>
    {
        private const string ConfigurationDatabaseCollectionSamwise = "SAMWISE_MONGO_DB_DATABASE_COLLECTION";
        private const string ConfigurationDatabaseCollectionSauron = "SAURON_MONGO_DB_DATABASE_COLLECTION";

        private readonly IParseData<HtmlDocument, CamaraMunicipalCusteioParlamentar> _parseData;
        private readonly IDataRepository<SamwiseDataRepository> _samwiseDataRepository;
        private readonly IDataRepository<SauronDataRepository> _sauronDataRepository;
        private readonly string _databaseCollectionNameSamwise;
        private readonly string _databaseCollectionNameSauron;

        public ParseService(IConfiguration configuration, IParseData<HtmlDocument, CamaraMunicipalCusteioParlamentar> parseData,
            IDataRepository<SamwiseDataRepository> samwiseDataRepository,
            IDataRepository<SauronDataRepository> sauronDataRepository)
        {
            _parseData = parseData;
            _samwiseDataRepository = samwiseDataRepository;
            _sauronDataRepository = sauronDataRepository;
            _databaseCollectionNameSamwise = configuration.TryGet(ConfigurationDatabaseCollectionSamwise);
            _databaseCollectionNameSauron = configuration.TryGet(ConfigurationDatabaseCollectionSauron);
        }

        public async Task ExecuteParseAsync()
        {
            foreach (var rawDataDetail in await ListDetailsCusteiosParlamentares())
            {
                var camaraMunicipalCusteioParlamentarResult = ExtractCamaraMunicipalCusteioParlamentar(rawDataDetail);

                await SaveOrUpdateCamaraMunicipalCusteioParlamentar(camaraMunicipalCusteioParlamentarResult);
                await UpdateRawData(camaraMunicipalCusteioParlamentarResult, rawDataDetail);
            }
        }
        
        private Task<List<RawData>> ListDetailsCusteiosParlamentares() => 
            _sauronDataRepository.GetAllAsync<RawData>(_databaseCollectionNameSauron, lnq => lnq.Parsed == default);

        private CamaraMunicipalCusteioParlamentar ExtractCamaraMunicipalCusteioParlamentar(RawData rawData)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rawData.RawContent);
            var camaraMunicipalCusteioParlamentarExtracted = _parseData.ParseData(htmlDocument);

            return LoadCamaraMunicipalCusteioParlamentar(camaraMunicipalCusteioParlamentarExtracted, rawData.Id, rawData.Filter);
        }

        private CamaraMunicipalCusteioParlamentar LoadCamaraMunicipalCusteioParlamentar(CamaraMunicipalCusteioParlamentar camaraMunicipalCusteioParlamentar,
            string id, string filter) =>
            camaraMunicipalCusteioParlamentar
                .SetIdDocumentExtracted(id)
                .SetDateExtractedWithDateNow()
                .SetIdParliamentaryExtracted(filter);


        private Task SaveOrUpdateCamaraMunicipalCusteioParlamentar(CamaraMunicipalCusteioParlamentar camaraMunicipalCusteioParlamentar)
        {
            return _samwiseDataRepository.SaveOrUpdateAsync(_databaseCollectionNameSamwise, camaraMunicipalCusteioParlamentar, lnq => lnq.Id == camaraMunicipalCusteioParlamentar.Id);
        }

        private Task UpdateRawData(CamaraMunicipalCusteioParlamentar camaraMunicipalCusteioParlamentar, RawData rawData)
        {
            UpdateModelRawData(rawData, camaraMunicipalCusteioParlamentar);
            return SaveRawData(rawData);
        }

        private void UpdateModelRawData(RawData rawData, CamaraMunicipalCusteioParlamentar camaraMunicipalCusteioParlamentar)
            => rawData.SetDateParsed(camaraMunicipalCusteioParlamentar.ExtractionDate);

        private Task SaveRawData(RawData rawData) => _sauronDataRepository.UpdateAsync(_databaseCollectionNameSauron, rawData, lnq => lnq.Id == rawData.Id);
    }
}