using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Repositories.Configurations;

namespace Samwise.Repositories.MongoDB
{
    public abstract class DataRepository<TRepository> : IDataRepository<TRepository>
    {
        protected readonly IMongoDatabase MongoDatabase;

        protected const string MongoAuthMech = "SCRAM-SHA-1";

        public DataRepository(MongoConfiguration<TRepository> configuration)
        {
            var settings = new MongoClientSettings
            {
                Credential = new MongoCredential(
                    MongoAuthMech,
                    new MongoInternalIdentity(configuration.MongoAdminDbN, configuration.MongoUsername),
                    new PasswordEvidence(configuration.MongoPassword)
                ),
                Server = new MongoServerAddress(configuration.MongoConTcpIp, configuration.MongoConnPort)
            };

            MongoDatabase = new MongoClient(settings).GetDatabase(configuration.MongoDatabase);
        }

        public Task<List<TData>> GetAllAsync<TData>(string collectionName)
        {
            return GetCollection<TData>(collectionName).Find(lnq => true).ToListAsync();
        }

        public Task<List<TData>> GetAllAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression)
        {
             return GetCollection<TData>(collectionName).Find(expression).ToListAsync();
        }

        public Task<TData> GetAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression)
        {
            return GetCollection<TData>(collectionName).Find(expression).FirstOrDefaultAsync();
        }

        public Task SaveOrUpdateAsync<TData>(string collectionName, TData rawData, Expression<Func<TData, bool>> expression)
        {
            return GetAsync(collectionName, expression)
                .ContinueWith(
                    (getAsync) => getAsync.Result == null
                        ? SaveAsync(collectionName, rawData)
                        : UpdateAsync(collectionName, rawData, expression)
                );
        }

        public Task SaveAsync<TData>(string collectionName, TData rawData)
        {
            return GetCollection<TData>(collectionName).InsertOneAsync(rawData)
                .ContinueWith(
                    (insertOneAsync) => (insertOneAsync.Exception == default)
                );
        }

        public Task UpdateAsync<TData>(string collectionName, TData rawData, Expression<Func<TData, bool>> expression)
        {
            return GetCollection<TData>(collectionName).ReplaceOneAsync(expression, rawData);
        }

        public Task DeleteAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression)
        {
            return GetCollection<TData>(collectionName).DeleteOneAsync(expression);
        }

        private IMongoCollection<TData> GetCollection<TData>(string collectionName)
        {
            return MongoDatabase.GetCollection<TData>(collectionName);
        }
    }
}