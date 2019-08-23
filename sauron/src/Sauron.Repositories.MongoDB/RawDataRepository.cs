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
        private const string MongoDatabase = "SAURON_MONGO_DB_DATABASE";

        private readonly IMongoDatabase _mongoDatabase;

        public RawDataRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            _mongoDatabase = mongoClient.GetDatabase(configuration.TryGet(MongoDatabase));
        }

        public Task AddAsync(string collectionName, RawData rawData)
        {
            return GetCollection(collectionName).InsertOneAsync(rawData);
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
            return GetCollection(collectionName).FindAsync(doc => true)
                .ContinueWith(
                        //TODO: improve this
                        (findAsync) => findAsync.Result.ToList()
                    );
        }

        public Task<RawData> GetAsync(string collectionName, string id)
        {
            return GetCollection(collectionName).FindAsync(doc => doc.Id == id)
                .ContinueWith(
                        (findAsync) => findAsync.Result.FirstOrDefault()
                    );
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
