using System;
using System.Threading.Tasks;
using Samwise.Abstractions.Parsers;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Services;

namespace Samwise.Services
{
    public class ParseService<TDataInput, TResult>: IParseService
    {
        private readonly IParseData<TDataInput, TResult> _parseData;
        private readonly IDataRepository _dataRepository;

        public ParseService(IParseData<TDataInput, TResult> parseData, IDataRepository dataRepository) =>
            (_parseData, _dataRepository) = (parseData, dataRepository);

        public Task ExecuteParseAsync()
        {
            throw new NotImplementedException();
        }
    }
}