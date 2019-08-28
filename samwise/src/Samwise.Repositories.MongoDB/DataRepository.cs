using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Repositories;

namespace Samwise.Repositories.MongoDB
{
    public class DataRepository: IDataRepository
    {
        private const string mongoAuthMech = "SCRAM-SHA-1";
        private const string MongoAdminDbN = "admin";
        private const string MongoUsername = "SAMWISE_MONGO_DB_USERNAME";
        private const string MongoPassword = "SAMWISE_MONGO_DB_PASSWORD";
        private const string MongoDatabase = "SAMWISE_MONGO_DB_DATABASE";
        private const string MongoConTcpIp = "SAMWISE_MONGO_DB_CONTCPIP";
        private const string MongoConnPort = "SAMWISE_MONGO_DB_CONNPORT";

        private readonly IMongoDatabase _mongoDatabase;

        public DataRepository(IConfiguration configuration)
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

        public Task<List<TData>> GetAllAsync<TData>(string collectionName)
        {
            throw new NotImplementedException();
        }

        public Task<TData> GetAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync<TData>(string collectionName, TData rawData)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<TData>(string collectionName, TData rawData)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression)
        {
            throw new NotImplementedException();
        }
        
        private IMongoCollection<TData> GetCollection<TData>(string collectionName)
        {
            return _mongoDatabase.GetCollection<TData>(collectionName);
        }
    }
}