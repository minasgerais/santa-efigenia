using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Abstractions.Extensions;
using Samwise.Abstractions.Repositories;
using Samwise.Abstractions.Repositories.Configurations;

namespace Samwise.Repositories.MongoDB
{
    public static class Bootstrap
    {
        private const string SamwiseMongoAdminDb = "SAMWISE_MONGO_ADMIN_DB";
        private const string SamwiseMongoUsername = "SAMWISE_MONGO_DB_USERNAME";
        private const string SamwiseMongoPassword = "SAMWISE_MONGO_DB_PASSWORD";
        private const string SamwiseMongoConTcpIp = "SAMWISE_MONGO_DB_CONTCPIP";
        private const string SamwiseMongoConnPort = "SAMWISE_MONGO_DB_CONNPORT";
        private const string SamwiseMongoDatabase = "SAMWISE_MONGO_DB_DATABASE";
        
        private const string SauronMongoAdminDb = "SAURON_MONGO_ADMIN_DB";
        private const string SauronMongoUsername = "SAURON_MONGO_DB_USERNAME";
        private const string SauronMongoPassword = "SAURON_MONGO_DB_PASSWORD";
        private const string SauronMongoConTcpIp = "SAURON_MONGO_DB_CONTCPIP";
        private const string SauronMongoConnPort = "SAURON_MONGO_DB_CONNPORT";
        private const string SauronMongoDatabase = "SAURON_MONGO_DB_DATABASE";

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped(provider => new MongoConfiguration<SamwiseDataRepository>(
                configuration.TryGet(SamwiseMongoAdminDb),
                configuration.TryGet(SamwiseMongoUsername),
                configuration.TryGet(SamwiseMongoPassword),
                configuration.TryGet(SamwiseMongoConTcpIp),
                configuration.TryGet<int>(SamwiseMongoConnPort),
                configuration.TryGet(SamwiseMongoDatabase)));
            
            services.TryAddScoped(provider => new MongoConfiguration<SauronDataRepository>(
                configuration.TryGet(SauronMongoAdminDb),
                configuration.TryGet(SauronMongoUsername),
                configuration.TryGet(SauronMongoPassword),
                configuration.TryGet(SauronMongoConTcpIp),
                configuration.TryGet<int>(SauronMongoConnPort),
                configuration.TryGet(SauronMongoDatabase)));

            services.TryAddScoped<IDataRepository<SamwiseDataRepository>, SamwiseDataRepository>();
            services.TryAddScoped<IDataRepository<SauronDataRepository>, SauronDataRepository>();

            return services;
        }
    }
}