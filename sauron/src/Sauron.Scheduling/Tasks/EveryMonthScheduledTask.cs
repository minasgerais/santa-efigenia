using FluentScheduler;
using System.Threading.Tasks;

namespace Sauron.Scheduling.Tasks
{
    public abstract class EveryMonthScheduledTask : Registry
    {
        protected int Interval { get; }

        public abstract Task ExecuteAsync();

        public EveryMonthScheduledTask()
        {
            Schedule(async () => await ExecuteAsync()).ToRunNow().AndEvery(1).Months().OnTheLastDay();
            NonReentrantAsDefault();
        }
    }
}
