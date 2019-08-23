using FluentScheduler;
using System.Threading.Tasks;

namespace Sauron.JobScheduler
{
    public abstract class Job : Registry
    {
        protected int Interval { get; }

        public abstract Task ExecuteAsync();

        public Job()
        {
            Schedule(async () => await ExecuteAsync()).ToRunNow().AndEvery(1).Months().OnTheLastDay();
            NonReentrantAsDefault();
        }
    }
}
