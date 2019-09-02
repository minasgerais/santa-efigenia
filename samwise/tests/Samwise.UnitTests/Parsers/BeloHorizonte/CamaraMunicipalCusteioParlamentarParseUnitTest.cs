using System.Collections.Generic;
using FluentAssertions;
using HtmlAgilityPack;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;
using Samwise.Base.Fixtures.Parsers;
using Samwise.Parsers.BeloHorizonte;
using Xunit;

namespace Samwise.UnitTests.Parsers.BeloHorizonte
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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(20)]
        public void Should_Get_Informations_From_Html(int amountElementsToGenerate)
        {
            var (expectedResult, tableHtml) = _fixture.GenerateFakeData(amountElementsToGenerate);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(tableHtml);
            var result = _parseData.ParseData(htmlDocument);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
        
    }
}