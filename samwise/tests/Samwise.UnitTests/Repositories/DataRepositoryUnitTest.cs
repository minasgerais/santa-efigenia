using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Samwise.Abstractions.Models;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Repositories;
using Samwise.Base.Fixtures.Repositories;
using Samwise.Repositories.MongoDB;
using Xunit;

namespace Samwise.UnitTests.Repositories
{
    public class DataRepositoryUnitTest : IClassFixture<DataRepositoryFixture>
    {
        private readonly IDataRepository<SamwiseDataRepository> _dataRepository;
        private readonly string _collectionName;
        private readonly IEnumerable<RawData> _listRawDatas;

        public DataRepositoryUnitTest(DataRepositoryFixture fixture)
        {
            _dataRepository = fixture.DataRepository;
            _collectionName = fixture.CollecionName;
            _listRawDatas = fixture.ListRawDatas;
        }

        [Fact]
        public async Task Should_Get_All_Elements()
        {
            var result = await _dataRepository.GetAllAsync<RawData>(_collectionName);
            result.Should().BeEquivalentTo(_listRawDatas);
        }

        [Fact]
        public async Task Should_Get_All_Elements_By_Expression_To_Filter()
        {
            var resultExpected = _listRawDatas.Where(lnq => lnq.Parsed == default);
            var result = await _dataRepository.GetAllAsync<RawData>(_collectionName, lnq => lnq.Parsed == default);
            result.Should().BeEquivalentTo(resultExpected);
        }
        
        [Fact]
        public async Task Should_Get_Element_By_Expression_To_Filter()
        {
            var resultExpected = _listRawDatas.FirstOrDefault(lnq => lnq.Id == "2F310C84");
            var result = await _dataRepository.GetAsync<RawData>(_collectionName, lnq => lnq.Id == "2F310C84");
            result.Should().BeEquivalentTo(resultExpected);
        }

        [Fact]
        public void Shoul_Save_Or_Update_Element_In_Collection()
        {
            var element = new CamaraMunicipalCusteioParlamentar
            {
                IdDocumentExtracted = "2F310C87",
                Name = "Teste",
                DetailExpanse = "Detail Expanse",
                Value = "R$12,21"
            };

            _dataRepository.Invoking(async lnq => await lnq.SaveOrUpdateAsync(_collectionName, element, t => t.IdDocumentExtracted == "2F310C87"))
                .Should().NotThrow();
        }
        
        [Fact]
        public void Shoul_Save_Element_In_Collection()
        {
            var element = new CamaraMunicipalCusteioParlamentar
            {
                IdDocumentExtracted = "2F310C87",
                Name = "Teste",
                DetailExpanse = "Detail Expanse",
                Value = "R$12,21"
            };

            _dataRepository.Invoking(async lnq => await lnq.SaveAsync(_collectionName, element))
                .Should().NotThrow();
        }
        
        [Fact]
        public void Shoul_Update_Element_In_Collection()
        {
            var element = new CamaraMunicipalCusteioParlamentar
            {
                IdDocumentExtracted = "2F310C84",
                Name = "Teste Update",
                DetailExpanse = "Detail Expanse",
                Value = "R$12,21"
            };

            _dataRepository.Invoking(async lnq => await lnq.UpdateAsync(_collectionName, element, t => t.IdDocumentExtracted == "2F310C84"))
                .Should().NotThrow();
        }
        
        [Fact]
        public void Shoul_Delete_Element_In_Collection()
        {
            _dataRepository.Invoking(async lnq => await lnq.DeleteAsync<CamaraMunicipalCusteioParlamentar>(_collectionName, t => t.IdDocumentExtracted == "2F310C84"))
                .Should().NotThrow();
        }
    }
}