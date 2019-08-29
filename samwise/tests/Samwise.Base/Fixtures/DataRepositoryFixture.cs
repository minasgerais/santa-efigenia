using System;
using System.Collections.Generic;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Models;

namespace Samwise.Base.Fixtures
{
    public class DataRepositoryFixture: BaseFixture
    {
        public readonly string CollecionName;
        
        public readonly IEnumerable<RawData>  ListRawDatas = new List<RawData>
        {
            new RawData {Id = "2F310C84", Url = "https://www.testes.com.br", Filter = "data=01/2001", Visited = new DateTimeOffset(DateTime.Now), Parsed = default, RawContent = "INFORMACAO"},
            new RawData {Id = "2F310C85", Url = "https://www.testes.com.br", Filter = "data=01/2003", Visited = new DateTimeOffset(DateTime.Now), Parsed = default, RawContent = "INFORMACAO"},
            new RawData {Id = "2F310C86", Url = "https://www.testes.com.br", Filter = "data=01/2002", Visited = new DateTimeOffset(DateTime.Now), Parsed = new DateTimeOffset(DateTime.Now), RawContent = "INFORMACAO"}
        };
        
        private const string CollectionSauronConfigKey = "SAURON_MONGO_DB_DATABASE_DETAIL_COLLECTION";

        public DataRepositoryFixture()
        {
            CollecionName = this.Configuration.TryGet(CollectionSauronConfigKey);
        }
    }
}