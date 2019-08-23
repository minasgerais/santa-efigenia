using FluentScheduler;
using Microsoft.Extensions.Configuration;
using Sauron.Abstractions.Extensions;
using System;
using System.Threading.Tasks;

namespace Sauron.Scheduling.Tasks
{
    public abstract class ScheduledTask : Registry
    {
        protected abstract string Scheduler { get; }

        protected int Interval { get; }

        public abstract Task ExecuteAsync();

        public ScheduledTask(IConfiguration configuration)
        {
            Interval = CalculateInterval(configuration);
            Schedule(async () => await ExecuteAsync()).ToRunNow().AndEvery(Interval).Days();
            NonReentrantAsDefault();
        }

        private int CalculateInterval(IConfiguration configuration)
        {
            var intervalValue = configuration.TryGet(Scheduler);

            if (string.IsNullOrWhiteSpace(intervalValue))
                throw new OperationCanceledException($"{nameof(Scheduler)}:{Scheduler} cannot be null or empty.");

            var interval = int.Parse(intervalValue);

            if (interval <= int.MinValue)
                throw new OperationCanceledException($"{nameof(Scheduler)}:{Scheduler} should be a positive value.");

            return interval;
        }
    }
}
