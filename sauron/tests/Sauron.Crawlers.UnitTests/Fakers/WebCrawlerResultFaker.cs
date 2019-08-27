using Bogus;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sauron.Crawlers.UnitTests.Fakers
{
    public static class WebCrawlerResultFaker
    {
        private static readonly Faker _faker = new Faker(locale: "pt_BR");

        public static RawData GetGlobalRawDataFakeResult(int amount, string source, IFilter filter)
        {
            return GetRawDataFakeResult(amount, source, filter, ProduceGlobalHtmlFakeResult);
        }

        public static RawData GetDetailRawDataFakeResult(int amount, string source, IFilter filter)
        {
            return GetRawDataFakeResult(amount, source, filter, GetDetailHtmlFakeResult);
        }

        public static IEnumerable<RawData> ListGlobalRawDataFakeResult(int amout, int innerAmount, string source, IFilter filter)
        {
            for (int i = 0; i < amout; ++i)
            {
                yield return GetGlobalRawDataFakeResult(innerAmount, source, filter);
            }
        }

        public static IEnumerable<RawData> ListDetailRawDataFakeResult(int amout, int innerAmount, string source, IFilter filter)
        {
            for (int i = 0; i < amout; ++i)
            {
                yield return GetDetailRawDataFakeResult(innerAmount, source, filter);
            }
        }

        private static string GetMd5Hash(string input)
        {
            using (var provider = MD5.Create())
            {
                return string.Join("",
                        provider
                            .ComputeHash(Encoding.UTF8.GetBytes(input))
                            .Select(s => s.ToString("x2"))
                    );
            }
        }

        private static string ProduceGlobalItemTemplate(int amount)
        {
            var buffer = new StringBuilder();

            for (int i = 0; i < amount; ++i)
            {
                var name = _faker.Name.FullName();

                buffer.AppendLine(
                    $@"
                        <tr class=""success"">
	                        <td data-title=""Despesa"" class=""td_pad"">{name}</td>
	                        <td data-title=""Detalhamento"" class=""td_pad""><a href=""#resultadoPesquisa_custeio"" class=""detalhar""
			                        data-codvereador=""{GetMd5Hash(name)}"">{_faker.Commerce.ProductName()}</a></td>
	                        <td data-title=""Valor"" class=""td_pad"">R${_faker.Finance.Amount()}</td>
                        </tr>
                    ");
            }

            return buffer.ToString();
        }

        private static string ProduceGlobalHtmlFakeResult(int amount)
        {
            return
                $@"
                    <h2>Resultados da pesquisa</h2>
                    <table id=""a"" class=""table-striped table-hover responsive-table"" width=""100%"">
	                    <thead>
		                    <tr>
			                    <th class=""cab_total"" scope=""col""><strong>Vereador</strong></th>
			                    <th class=""cab_th"" scope=""col"">
				                    <div align=""center""><strong><span class=""glyphicon glyphicon-eye-open""></span></strong></div>
			                    </th>
			                    <th class=""cab_total"" scope=""col""><strong>Valor</strong></th>
		                    </tr>
	                    </thead>
	                    <tbody>
		                   {ProduceGlobalItemTemplate(amount)}
	                    </tbody>
                    </table>
                ";
        }

        private static string ProduceDetailItemTemplate(int amount)
        {
            var total = 0m;
            var buffer = new StringBuilder();
            var name = _faker.Name.FullName();

            for (int i = 0; i < amount; ++i)
            {
                var value = _faker.Finance.Amount();

                total += value;

                buffer.AppendLine(
                    $@"
                        <tr class=""success"">
			                <td data-title=""Despesa"" class=""td_pad"">{name}</td>
			                <td data-title=""Detalhamento"" class=""td_pad"">{_faker.Commerce.ProductName()}</td>
			                <td data-title=""Valor"" class=""td_pad"">R${value}</td>
		                </tr>
                    ");
            }

            buffer.AppendLine(
                    $@"
                        <tr class=""success"">
	                        <td data-title=""Total"" class=""td_pad""><strong>Total</strong></td>
	                        <td data-title="""" class=""td_pad""></td>
	                        <td data-title=""Valor"" class=""td_pad""><strong>R${total}</strong></td>
                        </tr>        
                    ");

            return buffer.ToString();
        }

        private static string GetDetailHtmlFakeResult(int amount)
        {
            return
                $@"
                    <h2>Resultados da pesquisa</h2>
                    <table summary=""Despesas de Vereadores"" id=""a"" class=""table-striped table-hover responsive-table"" style=""width:100%"">
	                    <thead>
		                    <tr>
			                    <th class=""cab_total"" scope=""col""><strong>Vereador</strong></th>
			                    <th class=""cab_th"" scope=""col""><strong>Despesa</strong></div>
			                    </th>
			                    <th class=""cab_total"" scope=""col""><strong>Valor</strong></th>
		                    </tr>
	                    </thead>
	                    <tbody>
                            {ProduceDetailItemTemplate(amount)}
	                    </tbody>
                    </table>
                    <br>
                    <div class=""col-sm-12"">
	                    <a id=""voltarPesquisa_custeio"" class=""btn btn-default btn-fila"" href=""#inicioResultados""> Voltar </a>
                    </div>
                ";
        }

        private static RawData GetRawDataFakeResult(int amount, string source, IFilter filter, Func<int, string> rawContentProducer)
        {
            var rawContent = rawContentProducer(amount);

            var baseUrl = $"http://mock.it/{source}";

            return new RawData
            {
                Id = baseUrl.GetCrc(),
                Url = baseUrl,
                Filter = filter.AsQueryString(),
                Visited = DateTime.Now,
                Parsed = default,
                RawContent = rawContent
            };
        }
    }
}
