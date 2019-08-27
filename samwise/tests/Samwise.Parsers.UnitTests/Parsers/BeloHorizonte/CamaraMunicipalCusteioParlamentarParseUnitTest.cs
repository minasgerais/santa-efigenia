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
    public class CamaraMunicipalCusteioParlamentarParseUnitTest: IClassFixture<CamaraMunicipalCusteioParlamentarParseFixture>
    {
        private readonly IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>> _parseData;
        private readonly CamaraMunicipalCusteioParlamentarParseFixture _fixture;
        
        public CamaraMunicipalCusteioParlamentarParseUnitTest(CamaraMunicipalCusteioParlamentarParseFixture fixture)
        {
            _fixture = fixture;
            _parseData = new CamaraMunicipalCusteioParlamentarParse();
        }

        [Fact]
        public void Deve_Obter_Informacoes_HTML()
        {
            var (resultadoEsperado, htmlTabela) = _fixture.GerarDadosFake(1);
            var htmlDocumento = new HtmlDocument();
            htmlDocumento.LoadHtml(htmlTabela);
            var resultado = _parseData.ParseData(htmlDocumento);
            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
        
    }
}