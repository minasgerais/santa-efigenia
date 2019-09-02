using Samwise.Abstractions.Repositories.Configurations;

namespace Samwise.Repositories.MongoDB
{
    public class SauronDataRepository: DataRepository<SauronDataRepository>
    {
        public SauronDataRepository(MongoConfiguration<SauronDataRepository> configuration) : base(configuration)
        {
        }
    }
}