using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests
{
    public class WebCrawlerUnitTest : IClassFixture<DataFixture>
    {
        private readonly DataFixture _dataFixture;

        public WebCrawlerUnitTest(DataFixture dataFixture)
        {
            _dataFixture = dataFixture;
        }

        [Fact]
        public async Task Should_Get_Raw_Data()
        {
            var crawler = _dataFixture.CreateWebCrawler();
            var source = _dataFixture.Configuration["SAURON_CRAWLER_SEARCH_RESOURCE"];
            var rawData = await crawler.ExtractAsync(source, _dataFixture.CreateDefaultFilter());

            rawData.Should().NotBeNull();
        }
    }
}
