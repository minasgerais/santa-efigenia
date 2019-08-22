using Sauron.Abstractions.Crawlers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sauron.Crawlers
{
    public class Filter : IFilter
    {
        public static Filter Create() => new Filter();

        private readonly IDictionary<string, object> _parameters;

        private Filter()
        {
            _parameters = new Dictionary<string, object>();
        }

        public IFilter AddParameter(string key, object value)
        {
            _parameters.Add(key, value);

            return this;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var item in _parameters)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string AsQueryString()
        {
            var queryString = string.Join("&", _parameters.Select(item => item.Key + "=" + item.Value));
            return !string.IsNullOrWhiteSpace(queryString) ? $"?{queryString}" : default;
        }
    }
}
