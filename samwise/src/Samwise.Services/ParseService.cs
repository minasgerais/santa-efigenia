using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Samwise.Abstractions.Models;
using Samwise.Abstractions.Parsers;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Services;
using Samwise.Repositories.MongoDB;

namespace Samwise.Services
{
    public class ParseService<TDataInput, TResult>: IParseService
    {
        private const string SamwiseDatabaseCollectionSamwiseDatabaseCollection = "SAMWISE_MONGO_DB_DATABASE_COLLECTION";
        
        private readonly IParseData<TDataInput, TResult> _parseData;
        private readonly IDataRepository<SamwiseDataRepository> _dataRepository;
        private readonly IConfiguration _configuration;

        public ParseService(IConfiguration configuration, IParseData<TDataInput, TResult> parseData, IDataRepository<SamwiseDataRepository> dataRepository) =>
            (_configuration, _parseData, _dataRepository) = (configuration, parseData, dataRepository);

        public Task ExecuteParseAsync()
        {
            throw new NotImplementedException();
            //var listRawData = _dataRepository.GetAllAsync<RawData>()
        }
    }
}