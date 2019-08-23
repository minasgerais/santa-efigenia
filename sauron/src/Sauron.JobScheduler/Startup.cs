using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sauron.JobScheduler
{
    public static class Startup
    {
        private static readonly IList<Type> _jobContainer = new List<Type>();

        public static IServiceCollection ScheduleJob<TJob>(this IServiceCollection services) where TJob : Job
        {
            _jobContainer.Add(typeof(TJob));
            services.TryAddScoped(typeof(TJob));
            return services;
        }

        public static IApplicationBuilder UseJobScheduler(this IApplicationBuilder app)
        {
            var jobs = _jobContainer
                .Select(type => BuildJobObject(app, type))
                .ToArray();

            JobManager.Initialize(jobs);

            return app;
        }

        private static Job BuildJobObject(IApplicationBuilder app, Type possibleJobType)
        {
            return app.ApplicationServices.GetService(possibleJobType) as Job;
        }
    }
}
