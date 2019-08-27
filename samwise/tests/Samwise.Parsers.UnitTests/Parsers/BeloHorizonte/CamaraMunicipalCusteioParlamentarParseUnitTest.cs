using System.Collections.Generic;
using FluentAssertions;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;
using Samwise.Base.Fixtures;
using Samwise.Parsers.BeloHorizonte;
using Xunit;

namespace Samwise.Parsers.UnitTests.Parsers.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentarParseUnitTest
    {
        private readonly IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>> _parseData;

        public CamaraMunicipalCusteioParlamentarParseUnitTest()
        {
            _parseData = new CamaraMunicipalCusteioParlamentarParse();
        }

        [Fact]
        public void Deve_Obter_Informacoes_HTML()
        {
            var htmlDocumento = new HtmlDocument();
            htmlDocumento.LoadHtml(CamaraMunicipalCusteioParlamentarParseFixture.Html);
            var resultado = _parseData.ParseData(htmlDocumento);
            resultado.Should().NotBeNull();
        }
        
    }
}