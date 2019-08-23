using System.Collections.Generic;
using System.Threading.Tasks;
using Samwise.Abstractions.Repositories;

namespace Samwise.Repositories.MongoDB
{
    public class DataRepository: IDataRepository
    {
        public Task AddAsync<TData>(string collectionName, TData rawData)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string collectionName, string id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync<TData>(string collectionName, TData rawData)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TData>> GetAllAsync<TData>(string collectionName)
        {
            throw new System.NotImplementedException();
        }

        public Task<TData> GetAsync<TData>(string collectionName, string id)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync<TData>(string collectionName, TData rawData)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync<TData>(string collectionName, TData rawData)
        {
            throw new System.NotImplementedException();
        }
    }
}