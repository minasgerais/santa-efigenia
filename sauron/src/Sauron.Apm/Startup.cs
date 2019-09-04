using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sauron.Abstractions.Apm.Tracing;
using Sauron.Apm.Tracing;

namespace Sauron.Apm
{
    public static class Startup
    {
        public static IServiceCollection AddApm(this IServiceCollection collection)
        {
            collection.TryAddScoped<IMoment, Moment>();
            collection.TryAddScoped<IMonitor, Monitor>();

            return collection;
        }

        public static IApplicationBuilder UseApm(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseAllElasticApm(configuration);

            return app;
        }
    }
}