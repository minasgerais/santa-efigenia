using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using System;

namespace Sauron.Abstractions.Factories
{
    public static class RawDataFactory
    {
        public static RawData CreateRawData(string url, IFilter filter, string rawContent)
        {
            string buildRawDataId()
            {
                return $"{url}{filter.AsQueryString()}{rawContent}".GetCrc();
            }

            return new RawData
            {
                Id = buildRawDataId(),
                Url = url,
                Filter = filter.AsQueryString(),
                Visited = DateTimeOffset.Now,
                RawContent = rawContent
            };
        }
    }
}
