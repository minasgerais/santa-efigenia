using Elastic.Apm;
using Elastic.Apm.Api;
using System;
using System.Threading.Tasks;
using Sauron.Abstractions.Apm.Tracing;

namespace Sauron.Apm.Tracing
{
    public class Monitor : IMonitor
    {
        public Task Start(string name, string type, Func<Task> task)
        {
            return Agent.Tracer.CaptureTransaction(name, type, task);
        }

        public Task InspectMoment(string description, string type, Func<IMoment, Task<IMoment>> task)
        {
            return Agent.Tracer.CurrentTransaction?.CaptureSpan(description, type, async (span) =>
            {
                var moment = await task(new Moment());

                foreach (var (key, value) in moment.GetInstances())
                    span.Context.Labels[key] = value;
            });
        }
    }
}