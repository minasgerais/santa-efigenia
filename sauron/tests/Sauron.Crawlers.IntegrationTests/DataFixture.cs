using Microsoft.Extensions.Configuration;
using RestSharp;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using Sauron.Repositories.MongoDB;

namespace Sauron.Crawlers.IntegrationTests
{
    public class DataFixture
    {
        public IConfiguration Configuration { get; }

        public DataFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IWebCrawler<RawData> CreateWebCrawler()
        {
            var baseUrl = @"https://www.cmbh.mg.gov.br/sites/all/modules/execucao_orcamentaria_custeio";
            var restClient = new RestClient(baseUrl);

            return new WebCrawler(restClient);
        }

        public IFilter CreateDefaultFilter()
        {
            return Filter.Create()
                    .AddParameter("paginaRequerida", 1)
                    .AddParameter("data", "06/2019");
        }

        public IRawDataRepository GetRawDataRepository()
        {
            return new RawDataRepository(Configuration);
        }
    }
}
