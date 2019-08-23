using Microsoft.Extensions.Configuration;
using Moq;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Models;
using Sauron.Abstractions.Repositories;
using System;

namespace Sauron.Crawlers.UnitTests
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

        public RawData CreateRawData()
        {
            return new RawData
            {
                Id = "744fa1e57ba5b2de247351b3b542dfe7",
                Url = "https://mocked.url",
                Filter = "?paginaRequerida=1&data=06/2019",
                Visited = DateTime.Now,
                Parsed = default,
                RawContent = "<h2>Resultados da pesquisa</h2><table id=\"a\" class=\"table-striped table-hover responsive-table\" width=\"100%\">\r\n\t\t\t\t\t\t<thead>\r\n\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t<th class=\"cab_total\" scope=\"col\"><strong>Vereador</strong></th>\r\n                                <th class=\"cab_th\" scope=\"col\"><div align=\"center\"><strong><span class=\"glyphicon glyphicon-eye-open\"></span></strong></div></th>\r\n                                <th class=\"cab_total\" scope=\"col\"><strong>Valor</strong></th>                                \t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t</thead>\r\n                        <tbody>\r\n                        <tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Álvaro Damião</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591d957dc70563\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$310,60</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Arnaldo Godoy</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7801d41f000102446f48f200a8\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$700,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Autair Gomes</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7801d41f00010244a287b800bc\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$553,74</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Bim da Ambulância</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8c870980167\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.431,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Carlos Henrique</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591db307860576\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$11,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Catatau do Povo</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591dff80580588\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.430,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Cesar Gordin</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7668a3f8d90168a41c1b6c0010\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.400,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Coronel Piccinini</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb864ee4a0140\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.400,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Dimas da Ambulância</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f765a5186a0015aa9bc21b4057a\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$10,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Edmar Branco</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591dad0512056f\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$284,31</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Fernando Borja</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591e0bc7080595\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.438,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Fernando Luiz</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8693de70145\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$619,44</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Gilson Reis</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb86aca120148\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.400,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Hélio da Farmácia</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591dfc7d850583\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.514,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Irlan Melo</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591e1128480598\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.690,60</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Jair Di Gregório</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591e03014a058c\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.740,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Juninho Los Hermanos</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8d902cb0171\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.574,36</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Maninho Félix</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7668a3f8d90168a418c060000d\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.400,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Marilda Portela</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591da7486e056a\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$378,22</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Nely Aquino</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591dfe1e9b0585\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$280,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Orlei</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8b35f910160\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$20,00</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Pedrão do Depósito</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591daf13440572\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$651,40</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Pedro Bueno</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591e014bf8058a\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$290,60</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Pedro Patrus</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8b604d80162\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$327,54</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Preto</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f780244c44a010249886a19003f\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$87,18</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Professor Juliano Lopes</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f763ba9b074013bb8736b23014e\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$217,95</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Reinaldo Gomes</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f761de31dc1011e896714b00889\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$290,60</td></tr><tr class=\"success\"><td data-title=\"Despesa\" class=\"td_pad\">Wesley Autoescola</td><td data-title=\"Detalhamento\" class=\"td_pad\"><a href=\"#resultadoPesquisa_custeio\" class=\"detalhar\" data-codvereador=\"2c907f7658b6b07901591e1625c1059c\">Detalhamento das despesas</a></td><td data-title=\"Valor\" class=\"td_pad\">R$1.400,00</td></tr></tbody></table>"
            };
        }

        public IWebCrawler<RawData> CreateWebCrawler()
        {
            var mock = new Mock<IWebCrawler<RawData>>();

            mock
                .Setup(m => m.ExtractAsync(It.IsAny<string>(), It.IsAny<IFilter>()))
                .ReturnsAsync(CreateRawData());

            return mock.Object;
        }

        public IFilter CreateDefaultFilter()
        {
            return Filter.Create()
                    .AddParameter("data", "06/2019");
        }

        public IRawDataRepository GetRawDataRepository()
        {
            var mock = new Mock<IRawDataRepository>();

            mock
                .Setup(m => m.AddAsync(It.IsAny<string>(), It.IsAny<RawData>()));

            mock
                .Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(CreateRawData());

            return mock.Object;
        }
    }
}
