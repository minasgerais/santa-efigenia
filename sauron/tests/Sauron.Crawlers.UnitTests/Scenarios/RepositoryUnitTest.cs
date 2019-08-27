using FluentAssertions;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using Sauron.Crawlers.UnitTests.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests.Scenarios
{
    public class RepositoryUnitTest : IClassFixture<RawDataFixture>
    {
        private readonly RawDataFixture _rawDataFixture;
        private readonly IWebCrawler<RawData> _globalWebCrawler;
        private readonly IWebCrawler<RawData> _detailWebCrawler;

        public RepositoryUnitTest(RawDataFixture rawDataFixture)
        {
            _rawDataFixture = rawDataFixture;
            _globalWebCrawler = rawDataFixture.CreateGlobalWebCrawler(10);
            _detailWebCrawler = rawDataFixture.CreateDetailWebCrawler(10);
        }

        [Fact]
        public async Task Should_Add_A_Global_Raw_Data_To_Database()
        {
            var collectionName = _rawDataFixture.Configuration.TryGet("SAURON_MONGO_DB_DATABASE_GLOBAL_COLLECTION");
            var source = _rawDataFixture.Configuration.TryGet("SAURON_CRAWLER_GLOBAL_SOURCE");
            var filter = Filter.Create().AddParameter("data", "07/2019");
            var repository = _rawDataFixture.MockGlobalRawDataRepository(10, 10, source, filter);
            var rawData = await _globalWebCrawler.ExtractAsync(source, filter);

            repository.Invoking(act => act.AddAsync(collectionName, rawData)).Should().NotThrow();
        }

        [Fact]
        public async Task Should_Add_A_Detail_Raw_Data_To_Database()
        {
            var collectionName = _rawDataFixture.Configuration.TryGet("SAURON_MONGO_DB_DATABASE_DETAIL_COLLECTION");
            var source = _rawDataFixture.Configuration.TryGet("SAURON_CRAWLER_DETAIL_SOURCE");
            var filter = Filter.Create().AddParameter("data", "07/2019").AddParameter("codVereador", "2c907f763ba9b074013bb8736b23014e");
            var repository = _rawDataFixture.MockGlobalRawDataRepository(10, 10, source, filter);
            var rawData = await _detailWebCrawler.ExtractAsync(source, filter);

            repository.Invoking(act => act.AddAsync(collectionName, rawData)).Should().NotThrow();
        }
    }
}
