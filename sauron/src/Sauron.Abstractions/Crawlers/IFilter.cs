using System.Collections.Generic;

namespace Sauron.Abstractions.Crawlers
{
    public interface IFilter : IEnumerable<KeyValuePair<string, object>>
    {
        IFilter AddParameter(string key, object value);
        string AsQueryString();
    }
}
