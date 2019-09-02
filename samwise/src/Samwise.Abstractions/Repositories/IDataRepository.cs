using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Samwise.Abstractions.Repositories
{
    // TODO Avaliar se é possível definir contrato e uma implementação base para acesso a dados via mongo no MorganFreeamam
    public interface IDataRepository<TRepository>
    {
        Task<List<TData>> GetAllAsync<TData>(string collectionName);
        Task<List<TData>> GetAllAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression);
        Task<TData> GetAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression);
        Task SaveOrUpdateAsync<TData>(string collectionName, TData rawData, Expression<Func<TData, bool>> expression);
        Task SaveAsync<TData>(string collectionName, TData rawData);
        Task UpdateAsync<TData>(string collectionName, TData rawData, Expression<Func<TData, bool>> expression);
        Task DeleteAsync<TData>(string collectionName, Expression<Func<TData, bool>> expression);
    }
}