using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Abstractions.Repositories;

namespace Samwise.Repositories.MongoDB
{
    public static class Bootstrap
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IDataRepository,DataRepository>();
            
            return services;
        }
    }
}