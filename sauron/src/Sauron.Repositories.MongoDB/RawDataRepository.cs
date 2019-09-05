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
        private const string AuthMech = "SCRAM-SHA-1";
        private const string AdminDbN = "admin";
        private const string Username = "SAURON_MONGO_DB_USERNAME";
        private const string Password = "SAURON_MONGO_DB_PASSWORD";
        private const string Database = "SAURON_MONGO_DB_DATABASE";
        private const string ConTcpIp = "SAURON_MONGO_DB_CONTCPIP";
        private const string ConnPort = "SAURON_MONGO_DB_CONNPORT";

        private readonly IMongoDatabase _mongoDatabase;

        public RawDataRepository(IConfiguration configuration)
        {
            var settings = new MongoClientSettings
            {
                Credential = new MongoCredential(
                    AuthMech,
                    new MongoInternalIdentity(AdminDbN, configuration.TryGet(Username)),
                    new PasswordEvidence(configuration.TryGet(Password))
                ),
                Server = new MongoServerAddress(configuration.TryGet(ConTcpIp), configuration.TryGet<int>(ConnPort))
            };

            var mongoClient = new MongoClient(settings);

            _mongoDatabase = mongoClient.GetDatabase(configuration.TryGet(Database));
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
