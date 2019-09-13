using System;
using System.Collections.Generic;
using System.Linq;
using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Scheduling.Tasks;

namespace Samwise.Scheduling
{
    public static class Bootstrap
    {
        private static readonly IList<Type> _schedule = new List<Type>();

        public static IServiceCollection AddScheduledTask<TScheduledTask>(this IServiceCollection services) where TScheduledTask : ScheduledTask
        {
            _schedule.Add(typeof(TScheduledTask));

            services.TryAddScoped(typeof(TScheduledTask));

            return services;
        }

        public static IApplicationBuilder UseScheduledTask(this IApplicationBuilder app)
        {
            var jobs = _schedule
                .Select(type => (app.ApplicationServices.GetService(type) as ScheduledTask))
                .ToArray();

            JobManager.Initialize(jobs);

            return app;
        }
    }
}