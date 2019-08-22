using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using System.Threading.Tasks;

namespace Sauron.Repositories.MongoDB
{
    public class SearchRawDataRepository : IRawDataRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public SearchRawDataRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            _mongoDatabase = mongoClient.GetDatabase(configuration["SAURON_MONGO_DB_DATABASE"]);
        }

        public Task AddAsync(string collectionName, RawData rawData)
        {
            return GetCollection(collectionName).InsertOneAsync(rawData);
        }

        public Task<RawData> GetAsync(string collectionName, string id)
        {
            return GetCollection(collectionName).FindAsync(data => data.Id == id)
                .ContinueWith(
                        (findAsync) => findAsync.Result.FirstOrDefault()
                    );
        }

        private IMongoCollection<RawData> GetCollection(string collectionName)
        {
            return _mongoDatabase.GetCollection<RawData>(collectionName);
        }
    }
}
