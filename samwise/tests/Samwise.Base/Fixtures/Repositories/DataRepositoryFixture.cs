using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Models;
using Samwise.Abstractions.Repositories;
using Samwise.Repositories.MongoDB;

namespace Samwise.Base.Fixtures.Repositories
{
    public class DataRepositoryFixture : BaseFixture
    {
        public readonly string CollecionName;
        public readonly IDataRepository<SamwiseDataRepository> DataRepository;

        public readonly IEnumerable<RawData> ListRawDatas = new List<RawData>
        {
            new RawData {Id = "2F310C84", Url = "https://www.testes.com.br", Filter = "data=01/2001", Visited = new DateTimeOffset(DateTime.Now), Parsed = default, RawContent = "INFORMACAO"},
            new RawData {Id = "2F310C85", Url = "https://www.testes.com.br", Filter = "data=01/2003", Visited = new DateTimeOffset(DateTime.Now), Parsed = default, RawContent = "INFORMACAO"},
            new RawData {Id = "2F310C86", Url = "https://www.testes.com.br", Filter = "data=01/2002", Visited = new DateTimeOffset(DateTime.Now), Parsed = new DateTimeOffset(DateTime.Now), RawContent = "INFORMACAO"}
        };

        private const string CollectionSauronConfigKey = "SAMWISE_MONGO_DB_DATABASE_COLLECTION";

        public DataRepositoryFixture()
        {
            CollecionName = Configuration.TryGet(CollectionSauronConfigKey);
            DataRepository = InitializeMongoDatabase();
        }

        public IDataRepository<SamwiseDataRepository> InitializeMongoDatabase(IEnumerable<RawData> rawData = null)
        {
            rawData = rawData ?? ListRawDatas;

            var mock = new Mock<IDataRepository<SamwiseDataRepository>>();

            mock.Setup(lnq => lnq.GetAllAsync<RawData>(It.IsAny<string>(), It.IsAny<Expression<Func<RawData, bool>>>()))
                .ReturnsAsync((string collectionName, Expression<Func<RawData, bool>> expression) => rawData.Where(expression.Compile()).ToList());

            mock.Setup(lnq => lnq.GetAllAsync<RawData>(It.IsAny<string>()))
                .ReturnsAsync(rawData.ToList());

            mock.Setup(lnq => lnq.GetAsync(It.IsAny<string>(), It.IsAny<Expression<Func<RawData, bool>>>()))
                .ReturnsAsync((string nameCollection, Expression<Func<RawData, bool>> expression) => rawData.Where(expression.Compile()).FirstOrDefault());

            return mock.Object;
        }
    }
}