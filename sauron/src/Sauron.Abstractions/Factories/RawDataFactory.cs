using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;
using System;

namespace Sauron.Abstractions.Factories
{
    public static class RawDataFactory
    {
        private const string UtcHoursOffsetConfigKey = "SAURON_UTC_HOURS_OFFSET";

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
                Visited = DateTime.Now.ToDateTimeOffset(Globals.Configuration.TryGet<double>(UtcHoursOffsetConfigKey)),
                RawContent = rawContent
            };
        }
    }
}
