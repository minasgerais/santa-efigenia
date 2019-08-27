using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sauron.Repositories.MongoDB
{
    public class RawDataRepository : IRawDataRepository
    {
        private const string mongoAuthMech = "SCRAM-SHA-1";
        private const string MongoAdminDbN = "admin";
        private const string MongoUsername = "SAURON_MONGO_DB_USERNAME";
        private const string MongoPassword = "SAURON_MONGO_DB_PASSWORD";
        private const string MongoDatabase = "SAURON_MONGO_DB_DATABASE";
        private const string MongoConTcpIp = "SAURON_MONGO_DB_CONTCPIP";
        private const string MongoConnPort = "SAURON_MONGO_DB_CONNPORT";

        private readonly IMongoDatabase _mongoDatabase;

        public RawDataRepository(IConfiguration configuration)
        {
            var settings = new MongoClientSettings
            {
                Credential = new MongoCredential(
                    mongoAuthMech,
                    new MongoInternalIdentity(MongoAdminDbN, configuration.TryGet(MongoUsername)),
                    new PasswordEvidence(configuration.TryGet(MongoPassword))
                ),
                Server = new MongoServerAddress(configuration.TryGet(MongoConTcpIp), configuration.TryGet<int>(MongoConnPort))
            };

            var mongoClient = new MongoClient(settings);

            _mongoDatabase = mongoClient.GetDatabase(configuration.TryGet(MongoDatabase));
        }

        public Task<bool> AddAsync(string collectionName, RawData rawData)
        {
            return GetCollection(collectionName).InsertOneAsync(rawData)
                .ContinueWith(
                        (insertOneAsync) => (insertOneAsync.Exception == default)
                    );
        }

        public async Task<bool> AddIfNotExistsAsync(string collectionName, RawData rawData)
        {
            var result = await GetAsync(collectionName, rawData.Id);
            return (result == default) ? await AddAsync(collectionName, rawData) : false;
        }

        public Task DeleteAsync(string collectionName, string id)
        {
            return GetCollection(collectionName).DeleteOneAsync(doc => doc.Id == id);
        }

        public Task DeleteAsync(string collectionName, RawData rawData)
        {
            return DeleteAsync(collectionName, rawData.Id);
        }

        public Task<List<RawData>> GetAllAsync(string collectionName)
        {
            return GetCollection(collectionName).Find(doc => true).ToListAsync();
        }

        public Task<RawData> GetAsync(string collectionName, string id)
        {
            return GetCollection(collectionName).Find(doc => doc.Id == id).FirstOrDefaultAsync();
        }

        public Task SaveAsync(string collectionName, RawData rawData)
        {
            return GetAsync(collectionName, rawData.Id)
                .ContinueWith(
                        (getAsync) => (getAsync.Result == default)
                            ? AddAsync(collectionName, rawData) : UpdateAsync(collectionName, rawData)
                    );
        }

        public Task UpdateAsync(string collectionName, RawData rawData)
        {
            return GetCollection(collectionName).ReplaceOneAsync(doc => doc.Id == rawData.Id, rawData);
        }

        private IMongoCollection<RawData> GetCollection(string collectionName)
        {
            return _mongoDatabase.GetCollection<RawData>(collectionName);
        }
    }
}
