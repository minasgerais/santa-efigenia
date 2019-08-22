using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Sauron.Abstractions.Repositories;

namespace Sauron.Repositories.MongoDB
{
    public static class Startup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IMongoClient>(provider => new MongoClient(configuration["SAURON_MONGO_DB_CONNECTION"]));
            services.TryAddScoped<IRawDataRepository, RawDataRepository>();

            return services;
        }
    }
}
