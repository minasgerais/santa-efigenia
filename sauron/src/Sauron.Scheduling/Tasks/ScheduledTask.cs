using FluentScheduler;
using System.Threading.Tasks;

namespace Sauron.Scheduling.Tasks
{
    public abstract class ScheduledTask : Registry
    {
        protected int Interval { get; }

        public abstract Task ExecuteAsync();

        public ScheduledTask()
        {
            Schedule(async () => await ExecuteAsync()).ToRunNow().AndEvery(1).Months().OnTheLastDay();
            NonReentrantAsDefault();
        }
    }
}
