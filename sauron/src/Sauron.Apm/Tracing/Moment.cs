using System.Collections.Generic;
using System.Linq;
using Sauron.Abstractions.Apm.Tracing;

namespace Sauron.Apm.Tracing
{
    public class Moment : IMoment
    {
        private readonly IDictionary<string, string> _instances;

        public Moment()
        {
            _instances = new Dictionary<string, string>();
        }

        public void AddProperty(string key, string description) => _instances.Add(key, description);

        public IEnumerable<(string, string)> GetInstances() => _instances.Select(item => (item.Key, item.Value));
    }
}