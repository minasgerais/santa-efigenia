using Sauron.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sauron.Abstractions.Repositories
{
    public interface IRawDataRepository
    {
        Task<bool> AddAsync(string collectionName, RawData rawData);
        Task<bool> AddIfNotExistsAsync(string collectionName, RawData rawData);
        Task DeleteAsync(string collectionName, string id);
        Task DeleteAsync(string collectionName, RawData rawData);
        Task<List<RawData>> GetAllAsync(string collectionName);
        Task<RawData> GetAsync(string collectionName, string id);
        Task SaveAsync(string collectionName, RawData rawData);
        Task UpdateAsync(string collectionName, RawData rawData);
    }
}
