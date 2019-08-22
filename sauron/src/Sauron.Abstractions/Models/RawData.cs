using System;

namespace Sauron.Abstractions.Models
{
    public class RawData
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public DateTime Visited { get; set; }
        public string RawContent { get; set; }
    }
}
