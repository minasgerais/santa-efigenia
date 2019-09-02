using Samwise.Abstractions.Repositories.Configurations;

namespace Samwise.Repositories.MongoDB
{
    public class SamwiseDataRepository: DataRepository<SamwiseDataRepository>
    {
        public SamwiseDataRepository(MongoConfiguration<SamwiseDataRepository> configuration) : base(configuration)
        {
        }
    }
}