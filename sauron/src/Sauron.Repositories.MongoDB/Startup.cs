using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sauron.Abstractions.Repositories;

namespace Sauron.Repositories.MongoDB
{
    public static class Startup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IRawDataRepository, RawDataRepository>();

            return services;
        }
    }
}
