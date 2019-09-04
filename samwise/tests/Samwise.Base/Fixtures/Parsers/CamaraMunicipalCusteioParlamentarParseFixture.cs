using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Base.Extensions;

namespace Samwise.Base.Fixtures.Parsers
{
    public class CamaraMunicipalCusteioParlamentarParseFixture : BaseFixture
    {
        private string _nameFake;

        private readonly Faker _faker = BogusExtensions.FakerPtBr;

        private const string ExpenseDetailing = "Detalhamento despesa FAKE";

        public (CamaraMunicipalCusteioParlamentar expectedResult, string htmlTabelaFake) GenerateFakeData(int amountElements)
        {
            _nameFake = _faker.Name.FullName();
            var expectedResult = new CamaraMunicipalCusteioParlamentar
            {
                Name = amountElements == default ? default : _nameFake,
                Expenses = GenerateCamaraMunicipalCusteioParlamentarExpenses(amountElements)
            };

            return (expectedResult, GenerateTable(expectedResult.Expenses));
        }

        private IEnumerable<CamaraMunicipalCusteioParlamentarExpenses> GenerateCamaraMunicipalCusteioParlamentarExpenses(int amountElements)
        {
            var result = new List<CamaraMunicipalCusteioParlamentarExpenses>();
            for (var i = 0; i < amountElements; i++)
            {
                result.Add(new CamaraMunicipalCusteioParlamentarExpenses
                {
                    DetailExpanse = ExpenseDetailing,
                    Value = $"R${_faker.Finance.Amount()}"
                });
            }

            return result;
        }


        private string GenerateTable(IEnumerable<CamaraMunicipalCusteioParlamentarExpenses> fakeElements)
        {
            return $@"
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
                {GenerateTrs(fakeElements)}
                </tbody>
                </table>
                <br>
                <div class=""col-sm-12"">
                    <a id=""voltarPesquisa_custeio"" class=""btn btn-default btn-fila"" href=""#inicioResultados""> Voltar </a>
                </div>
            ";
        }

        private string GenerateTrs(IEnumerable<CamaraMunicipalCusteioParlamentarExpenses> fakeElements)
        {
            var trs = new StringBuilder();
            var totalValue = 0m;


            foreach (var item in fakeElements)
            {
                totalValue += Convert.ToDecimal(item.Value.Replace("R$", ""));

                trs.AppendLine($@"
                    <tr class=""success"">
                        <td data-title=""Despesa"" class=""td_pad"">{_nameFake}</td>
                        <td data-title=""Detalhamento"" class=""td_pad"">{ExpenseDetailing}</td>
                        <td data-title=""Valor"" class=""td_pad"">{item.Value}</td>
                    </tr>
                ");
            }

            trs.AppendLine($@"
                <tr class=""success"">
                    <td data-title=""Total"" class=""td_pad""><strong>Total</strong></td>
                    <td data-title="" class=""td_pad""></td>
                    <td data-title=""Valor"" class=""td_pad""><strong>R${totalValue}</strong></td>
                </tr>
            ");

            return trs.ToString();
        }
    }
}