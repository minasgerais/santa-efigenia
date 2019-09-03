using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Samwise.Abstractions.Repositories.Configurations;
using Samwise.Base.Fixtures;
using Samwise.Repositories.MongoDB;
using Xunit;

namespace Samwise.UnitTests
{
    public class InitializeProgramUnitTest: IClassFixture<InitializeProgramFixture>
    {
        private readonly InitializeProgramFixture _fixture;
        public InitializeProgramUnitTest(InitializeProgramFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_Initialize_MongoConfiguration_SamwiseDataRepository()
        {
            _fixture.Invoking(lnq => lnq.Resolve<MongoConfiguration<SamwiseDataRepository>>())
                .Should().NotThrow();
        }
        
        [Fact]
        public void Should_Initialize_MongoConfiguration_SauronDataRepository()
        {
            _fixture.Invoking(lnq => lnq.Resolve<MongoConfiguration<SauronDataRepository>>())
                .Should().NotThrow();
        }
    }
}