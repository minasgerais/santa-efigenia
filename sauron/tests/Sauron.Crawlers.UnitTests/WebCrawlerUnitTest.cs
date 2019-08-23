using FluentAssertions;
using Sauron.Abstractions.Extensions;
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
            var source = _dataFixture.Configuration.TryGet("SAURON_CRAWLER_GLOBAL_SOURCE");
            var rawData = await crawler.ExtractAsync(source, _dataFixture.CreateDefaultFilter());

            rawData.Should().NotBeNull();
        }
    }
}
