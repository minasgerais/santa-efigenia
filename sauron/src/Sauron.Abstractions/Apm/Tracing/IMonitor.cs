using System;
using System.Threading.Tasks;

namespace Sauron.Abstractions.Apm.Tracing
{
    public interface IMonitor
    {
         Task Start(string name, string type, Func<Task> task);
         Task InspectMoment(string description, string type, Func<IMoment, Task<IMoment>> task);
    }
}