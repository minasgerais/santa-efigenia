using FluentAssertions;
using Sauron.Abstractions.Extensions;
using Sauron.Crawlers.UnitTests.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests.Scenarios
{
    public class WebCrawlerUnitTest : IClassFixture<RawDataFixture>
    {
        private readonly RawDataFixture _rawDataFixture;

        public WebCrawlerUnitTest(RawDataFixture rawDataFixture)
        {
            _rawDataFixture = rawDataFixture;
        }

        [Fact]
        public void Should_Generate_Crc()
        {
            "SAURON".GetCrc().Should().BeEquivalentTo("2B259010");
        }

        [Fact]
        public async Task Should_Extract_A_Global_Raw_Data()
        {
            var crawler = _rawDataFixture.CreateGlobalWebCrawler(10);
            var source = _rawDataFixture.Configuration.TryGet("SAURON_CRAWLER_GLOBAL_SOURCE");
            var rawData = await crawler.ExtractAsync(source, Filter.Create().AddParameter("data", "07/2019"));

            rawData.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Extract_A_Detail_Global_Raw_Data()
        {
            var crawler = _rawDataFixture.CreateDetailWebCrawler(10);
            var source = _rawDataFixture.Configuration.TryGet("SAURON_CRAWLER_DETAIL_SOURCE");
            var rawData = await crawler.ExtractAsync(source, Filter.Create().AddParameter("data", "07/2019").AddParameter("codVereador", "2c907f763ba9b074013bb8736b23014e"));

            rawData.Should().NotBeNull();
        }
    }
}
