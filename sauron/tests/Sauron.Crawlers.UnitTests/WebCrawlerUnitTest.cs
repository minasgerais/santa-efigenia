using FluentAssertions;
using RestSharp;
using System.Threading.Tasks;
using Xunit;

namespace Sauron.Crawlers.UnitTests
{
    public class WebCrawlerUnitTest
    {
        [Fact]
        public async Task Should_Get_Raw_Data()
        {
            var baseUrl = @"https://www.cmbh.mg.gov.br/sites/all/modules/execucao_orcamentaria_custeio";
            var restClient = new RestClient(baseUrl);
            var crawler = new WebCrawler(restClient);

            var result = await crawler.ExtractAsync("pesquisar.php", Filter.Create()
                    .AddParameter("paginaRequerida", 1)
                    .AddParameter("data", "07/2019")
                );

            result.Should().NotBeNull();
        }
    }
}
