using System.Collections.Generic;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;

namespace Samwise.Parsers.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentarParse: IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>>
    {
        public IEnumerable<CamaraMunicipalCusteioParlamentar> ParseData(HtmlDocument data)
        {
            throw new System.NotImplementedException();
        }
    }
}