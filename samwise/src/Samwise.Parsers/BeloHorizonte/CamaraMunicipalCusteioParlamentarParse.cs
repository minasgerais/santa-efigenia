using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;

namespace Samwise.Parsers.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentarParse : IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>>
    {
        private const string DataTitleDespesa = "Despesa";
        private const string DataTitleDetalhamento = "Detalhamento";
        private const string DataTileValor = "Valor";
        private const string TabelaSelectorRoot = @"//table[@id='a']/tbody/tr/td[contains(@data-title, 'Despesa')]/..";
        private const string DataTileSelector = @".//td[@data-title='{0}']";

        public IEnumerable<CamaraMunicipalCusteioParlamentar> ParseData(HtmlDocument data)
        {
            return data.DocumentNode.SelectNodes(TabelaSelectorRoot)
                .Where(lnq => lnq.NodeType != HtmlNodeType.Text)
                .Select(lnq => new CamaraMunicipalCusteioParlamentar
                {
                    Nome = lnq.SelectSingleNode(string.Format(DataTileSelector, DataTitleDespesa)).InnerText.Trim(),
                    Despesa = lnq.SelectSingleNode(string.Format(DataTileSelector, DataTitleDetalhamento)).InnerText.Trim(),
                    Valor = lnq.SelectSingleNode(string.Format(DataTileSelector, DataTileValor)).InnerText.Trim()
                });
        }
    }
}