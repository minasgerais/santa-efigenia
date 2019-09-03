using Elastic.Apm;
using Elastic.Apm.Api;
using System;
using System.Threading.Tasks;

namespace Sauron.Apm.Tracing
{
    public static class Transaction
    {
        public static Task Capture(string name, string type, Func<ITransaction, Task> task)
        {
            return Agent.Tracer.CaptureTransaction(name, type, async () =>
            {
                await task(Agent.Tracer.CurrentTransaction);
            });
        }
    }
}
