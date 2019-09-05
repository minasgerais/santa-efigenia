using System.Collections.Generic;

namespace Sauron.Abstractions.Apm.Tracing
{
    public interface IMoment
    {
        void AddProperty(string name, string value);
        IEnumerable<(string, string)> GetInstances();
    }
}