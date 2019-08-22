using Sauron.Abstractions.Models;
using System.Threading.Tasks;

namespace Sauron.Abstractions.Repositories
{
    public interface IRawDataRepository
    {
        Task AddAsync(string collectionName, RawData rawData);
        Task<RawData> GetAsync(string collectionName, string id);
    }
}
