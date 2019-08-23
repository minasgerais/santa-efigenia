using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samwise.Abstractions.Repositories
{
    // TODO avaliar abstração de repositório MorganFreeman para aplicações
    public interface IDataRepository
    {
        Task AddAsync<TData>(string collectionName, TData rawData);
        Task DeleteAsync(string collectionName, string id);
        Task DeleteAsync<TData>(string collectionName, TData rawData);
        Task<List<TData>> GetAllAsync<TData>(string collectionName);
        Task<TData> GetAsync<TData>(string collectionName, string id);
        Task SaveAsync<TData>(string collectionName, TData rawData);
        Task UpdateAsync<TData>(string collectionName, TData rawData);
    }
}