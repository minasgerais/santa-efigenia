using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;

namespace Samwise.Parsers.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentarParse : IParseData<HtmlDocument, CamaraMunicipalCusteioParlamentar>
    {
        private const string DataTitleDespesa = "Despesa";
        private const string DataTitleDetalhamento = "Detalhamento";
        private const string DataTileValor = "Valor";
        private const string TabelaSelectorRoot = @"//table[@id='a']/tbody/tr/td[contains(@data-title, 'Despesa')]/..";
        private const string DataTileSelector = @".//td[@data-title='{0}']";

        public CamaraMunicipalCusteioParlamentar ParseData(HtmlDocument data)
        {
            var nodes = data.DocumentNode.SelectNodes(TabelaSelectorRoot)
                ?.Where(lnq => lnq.NodeType != HtmlNodeType.Text);
            return nodes != default
                ? new CamaraMunicipalCusteioParlamentar
                {
                    Name = nodes.First().SelectSingleNode(string.Format(DataTileSelector, DataTitleDespesa)).InnerText
                        .Trim(),
                    CamaraMunicipalCusteioParlamentarExpenseses = nodes.Select(lnq =>
                        new CamaraMunicipalCusteioParlamentarExpenses
                        {
                            DetailExpanse = lnq.SelectSingleNode(string.Format(DataTileSelector, DataTitleDetalhamento))
                                .InnerText.Trim(),
                            Value = lnq.SelectSingleNode(string.Format(DataTileSelector, DataTileValor)).InnerText
                                .Trim()
                        })
                }
                : new CamaraMunicipalCusteioParlamentar();
        }
    }
}