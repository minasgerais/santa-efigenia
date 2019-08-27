using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Base.Extensions;

namespace Samwise.Base.Fixtures
{
    public class CamaraMunicipalCusteioParlamentarParseFixture
    {
        private readonly Faker _faker = BogusExtensions.FakerPtBr;

        private const string DetalhamentoDespesa = "Detalhamento despesa FAKE";
        
        public (IEnumerable<CamaraMunicipalCusteioParlamentar> resultadoEsperado, string htmlTabelaFake) GerarDadosFake(int quantidadeElementos)
        {
            var nome = _faker.Name.FullName();
            var resultadoEsperado = new List<CamaraMunicipalCusteioParlamentar>();
            for (var i = 0; i < quantidadeElementos; i++)
            {
                resultadoEsperado.Add(new CamaraMunicipalCusteioParlamentar
                {
                    Nome = nome,
                    Despesa = DetalhamentoDespesa,
                    Valor = $"R${_faker.Finance.Amount()}"
                });
            }

            return (resultadoEsperado, GerarTabela(resultadoEsperado));
        }


        private string GerarTabela(IEnumerable<CamaraMunicipalCusteioParlamentar> registrosFake)
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
                {GerarTr(registrosFake)}
                </tbody>
                </table>
                <br>
                <div class=""col-sm-12"">
                    <a id=""voltarPesquisa_custeio"" class=""btn btn-default btn-fila"" href=""#inicioResultados""> Voltar </a>
                </div>
            ";
        }

        private string GerarTr(IEnumerable<CamaraMunicipalCusteioParlamentar> registrosFake)
        {
            var trs = new StringBuilder();
            var valorTotal = 0m;
            
            

            foreach (var item in registrosFake)
            {
                valorTotal += Convert.ToDecimal(item.Valor.Replace("R$", ""));
                    
                trs.AppendLine($@"
                    <tr class=""success"">
                        <td data-title=""Despesa"" class=""td_pad"">{item.Nome}</td>
                        <td data-title=""Detalhamento"" class=""td_pad"">{DetalhamentoDespesa}</td>
                        <td data-title=""Valor"" class=""td_pad"">{item.Valor}</td>
                    </tr>
                ");
            }

            trs.AppendLine($@"
                <tr class=""success"">
                    <td data-title=""Total"" class=""td_pad""><strong>Total</strong></td>
                    <td data-title="" class=""td_pad""></td>
                    <td data-title=""Valor"" class=""td_pad""><strong>R${valorTotal}</strong></td>
                </tr>
            ");

            return trs.ToString();
        }
    }
}