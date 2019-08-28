using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Samwise.Abstractions.Repositories
{
    // TODO avaliar abstração de repositório MorganFreeman para aplicações
    public interface IDataRepository
    {
        Task<List<TData>> GetAllAsync<TData>(string collectionName);
        Task<TData> GetAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression);
        Task SaveAsync<TData>(string collectionName, TData rawData);
        Task UpdateAsync<TData>(string collectionName, TData rawData);
        Task DeleteAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression);
    }
}