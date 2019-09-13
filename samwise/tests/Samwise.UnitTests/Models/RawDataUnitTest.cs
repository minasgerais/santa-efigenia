using System;
using FluentAssertions;
using Samwise.Abstractions.Models;
using Xunit;

namespace Samwise.UnitTests.Models
{
    public class RawDataUnitTest
    {
        [Fact]
        public void Shoul_Be_Null_When_Data_Parsed_Not_Set()
        {
            var rawData = new RawData();

            rawData.Parsed.Should().BeNull();
        }
        
        [Fact]
        public void Shoul_Not_Be_Null_When_Data_Parsed_Set()
        {
            var rawData = new RawData();
            rawData.SetDateParsed(DateTimeOffset.Now);

            rawData.Parsed.Should().NotBeNull();
        }
    }
}