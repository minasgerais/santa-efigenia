using System;
using FluentAssertions;
using Samwise.Abstractions.Models.BeloHorizonte;
using Xunit;

namespace Samwise.UnitTests.Models
{
    public class CamaraMunicipalCusteioParlamentarUnitTest
    {
        [Theory]
        [InlineData("A5971298")]
        [InlineData("7BB36A01")]
        [InlineData("AA628F44")]
        [InlineData("BB46EFC4")]
        public void Should_Set_Id_Document_Extracted(string idExtracted)
        {
            var result = new CamaraMunicipalCusteioParlamentar()
                .SetIdDocumentExtracted(idExtracted);
            result.Id.Should().Be(idExtracted);
        }

        [Fact]
        public void Should_Set_Date_Extracted()
        {
            var result = new CamaraMunicipalCusteioParlamentar()
                .SetDateExtractedWithDateNow();

            result.ExtractionDate.Should().NotBe(DateTimeOffset.MinValue);
        }

        [Theory]
        [InlineData("data=05/2017&codVereador=2c907f7658b6b07901591d957dc70563", "2c907f7658b6b07901591d957dc70563")]
        [InlineData("data=05/2017&codVereador=2c907f7801d41f000102446f48f200a8", "2c907f7801d41f000102446f48f200a8")]
        [InlineData("data=05/2017&codVereador=2c907f7658b6b07901591d8d00f0055e", "2c907f7658b6b07901591d8d00f0055e")]
        [InlineData("data=05/2017&codVereador=2c907f763ba9b074013bb8c870980167", "2c907f763ba9b074013bb8c870980167")]
        public void Shoul_Set_Id_Parliamentary_Extracted(string filter, string resultExpected)
        {
            var result = new CamaraMunicipalCusteioParlamentar()
                .SetIdParliamentaryExtracted(filter);

            result.IdParliamentary.Should().Be(resultExpected);
        }
    }
}