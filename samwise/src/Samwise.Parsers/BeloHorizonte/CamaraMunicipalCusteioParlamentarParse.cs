using System.Collections.Generic;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;

namespace Samwise.Parsers.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentarParse: IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>>
    {
        private const string XPathTabelaDespesas = "//table[@id=\"a\"]//tbody";
        public IEnumerable<CamaraMunicipalCusteioParlamentar> ParseData(HtmlDocument data)
        {
            var tabela = data.DocumentNode.SelectSingleNode(XPathTabelaDespesas);

            throw new System.NotImplementedException();
        }
    }
}