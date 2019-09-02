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
        private const string SamwiseMongoAdminDbN = "admin";
        private const string SamwiseMongoUsername = "SAMWISE_MONGO_DB_USERNAME";
        private const string SamwiseMongoPassword = "SAMWISE_MONGO_DB_PASSWORD";
        private const string SamwiseMongoConTcpIp = "SAMWISE_MONGO_DB_CONTCPIP";
        private const string SamwiseMongoConnPort = "SAMWISE_MONGO_DB_CONNPORT";
        private const string SamwiseMongoDatabase = "SAMWISE_MONGO_DB_DATABASE";

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped(provider =>
                new MongoConfiguration<SamwiseDataRepository>(SamwiseMongoAdminDbN, configuration.TryGet(SamwiseMongoUsername), configuration.TryGet(SamwiseMongoPassword),
                    configuration.TryGet(SamwiseMongoConTcpIp), configuration.TryGet<int>(SamwiseMongoConnPort), configuration.TryGet(SamwiseMongoDatabase)));

            services.TryAddScoped<IDataRepository<SamwiseDataRepository>, SamwiseDataRepository>();

            return services;
        }
    }
}