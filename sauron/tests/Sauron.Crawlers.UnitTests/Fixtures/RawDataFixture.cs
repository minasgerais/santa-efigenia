using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Crawlers.UnitTests.Fakers;
using System.Linq;

namespace Sauron.Crawlers.UnitTests.Fixtures
{
    public class RawDataFixture
    {
        public IConfiguration Configuration { get; }

        public RawDataFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public ILogger<T> GetLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }

        public IWebCrawler<RawData> CreateGlobalWebCrawler(int amount)
        {
            var mock = new Mock<IWebCrawler<RawData>>();

            mock
                .Setup(m => m.ExtractAsync(It.IsAny<string>(), It.IsAny<IFilter>()))
                .ReturnsAsync((string source, IFilter filter) =>
                {
                    return WebCrawlerResultFaker.GetGlobalRawDataFakeResult(amount, source, filter);
                });

            return mock.Object;
        }

        public IWebCrawler<RawData> CreateDetailWebCrawler(int amount)
        {
            var mock = new Mock<IWebCrawler<RawData>>();

            mock
                .Setup(m => m.ExtractAsync(It.IsAny<string>(), It.IsAny<IFilter>()))
                .ReturnsAsync((string source, IFilter filter) =>
                {
                    return WebCrawlerResultFaker.GetDetailRawDataFakeResult(amount, source, filter);
                });

            return mock.Object;
        }

        public IRawDataRepository MockGlobalRawDataRepository(int amount, int innerAmount, string source, IFilter filter)
        {
            var mock = new Mock<IRawDataRepository>();

            mock
                .Setup(m => m.AddAsync(It.IsAny<string>(), It.IsAny<RawData>()));

            mock
                .Setup(m => m.AddIfNotExistsAsync(It.IsAny<string>(), It.IsAny<RawData>()))
                .ReturnsAsync(true);

            mock
                .Setup(m => m.GetAllAsync(It.IsAny<string>()))
                .ReturnsAsync(WebCrawlerResultFaker.ListGlobalRawDataFakeResult(amount, innerAmount, source, filter).ToList());


            return mock.Object;
        }

        public IRawDataRepository MockDetailRawDataRepository(int amount, int innerAmount, string source, IFilter filter)
        {
            var mock = new Mock<IRawDataRepository>();

            mock
                .Setup(m => m.AddAsync(It.IsAny<string>(), It.IsAny<RawData>()));

            mock
                .Setup(m => m.AddIfNotExistsAsync(It.IsAny<string>(), It.IsAny<RawData>()))
                .ReturnsAsync(true);

            mock
                .Setup(m => m.GetAllAsync(It.IsAny<string>()))
                .ReturnsAsync(WebCrawlerResultFaker.ListDetailRawDataFakeResult(amount, innerAmount, source, filter).ToList());


            return mock.Object;
        }
    }
}
