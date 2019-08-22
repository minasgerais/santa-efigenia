using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests
{
    public class SearchRawDataRepositoryUnitTest : IClassFixture<DataFixture>
    {
        private readonly DataFixture _dataFixture;

        public SearchRawDataRepositoryUnitTest(DataFixture dataFixture)
        {
            _dataFixture = dataFixture;
        }

        [Fact]
        public async Task Should_Add_Raw_Data_To_Search_Collection_In_MongoDB()
        {
            var crawler = _dataFixture.CreateWebCrawler();
            var source = _dataFixture.Configuration["SAURON_CRAWLER_SEARCH_RESOURCE"];
            var rawData = await crawler.ExtractAsync(source, _dataFixture.CreateDefaultFilter());
            var collectionName = _dataFixture.Configuration["SAURON_MONGO_DB_DATABASE_SEARCH_COLLECTION"];
            var repository = _dataFixture.GetRawDataRepository();

            repository.Invoking(act => act.AddAsync(collectionName, rawData)).Should().NotThrow();
        }
    }
}
