using System;
using System.Threading.Tasks;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Samwise.Abstractions.Extensions;

namespace Samwise.Scheduling.Tasks
{
    public abstract class ScheduledTask : Registry
    {
        protected abstract string Name { get; }
        
        protected abstract string Scheduler { get; }

        protected int Interval { get; }

        public abstract Task ExecuteAsync();

        public ScheduledTask(IConfiguration configuration, ILogger<ScheduledTask> logger)
        {
            Interval = CalculateInterval(configuration);
            Schedule(async () => await StartAsync(logger)).ToRunNow().AndEvery(Interval).Days();
            NonReentrantAsDefault();
        }

        private async Task StartAsync(ILogger logger)
        {
            logger.Stamp($"Scheduled Task [{Name}] started.");

            await ExecuteAsync();

            logger.Stamp($"Scheduled Task [{Name}] finished.");
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