using System;

namespace Sauron.Abstractions.Models
{
    public class RawData
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Filter { get; set; }
        public DateTimeOffset Visited { get; set; }
        public DateTimeOffset? Parsed { get; set; }
        public string RawContent { get; set; }
    }
}
