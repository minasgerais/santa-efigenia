using FluentAssertions;
using Sauron.Abstractions.Extensions;
using Sauron.Crawlers.UnitTests.Fixtures;
using Sauron.Runner.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests.Scenarios
{
    public class ScheduledTaskUnitTest : IClassFixture<RawDataFixture>
    {
        private const string GlobalSource = "SAURON_CRAWLER_GLOBAL_SOURCE";
        private const string DetailSource = "SAURON_CRAWLER_DETAIL_SOURCE";

        private GlobalRawDataScheduledTask GlobalRawDataScheduledTask { get; }

        private DetailRawDataScheduledTask DetailRawDataScheduledTask { get; }

        public ScheduledTaskUnitTest(RawDataFixture rawDataFixture)
        {
            var configuration = rawDataFixture.Configuration;

            GlobalRawDataScheduledTask = new GlobalRawDataScheduledTask(
                    rawDataFixture.Configuration,
                    rawDataFixture.CreateGlobalWebCrawler(10),
                    rawDataFixture.MockGlobalRawDataRepository(10, 10, configuration.TryGet(GlobalSource), Filter.Create().AddParameter("data", "07/19")),
                    rawDataFixture.GetLogger<RawDataScheduledTask>()
                );

            DetailRawDataScheduledTask = new DetailRawDataScheduledTask(
                    rawDataFixture.Configuration,
                    rawDataFixture.CreateDetailWebCrawler(10),
                    rawDataFixture.MockGlobalRawDataRepository(10, 10, configuration.TryGet(GlobalSource), Filter.Create().AddParameter("data", "07/19")),
                    rawDataFixture.GetLogger<RawDataScheduledTask>()
                );
        }

        [Fact]
        public void Should_Extract_Global_Data()
        {
            GlobalRawDataScheduledTask.Invoking(act => act.ExecuteAsync()).Should().NotThrow();
        }

        [Fact]
        public void Should_Extract_Detail_Data()
        {
            DetailRawDataScheduledTask.Invoking(act => act.ExecuteAsync()).Should().NotThrow();
        }
    }
}
