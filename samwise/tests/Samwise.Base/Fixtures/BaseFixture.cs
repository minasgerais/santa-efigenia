using System.IO;
using Microsoft.Extensions.Configuration;

namespace Samwise.Base.Fixtures
{
    public abstract class BaseFixture
    {
        public IConfiguration Configuration;
        
        public BaseFixture()
        {
            InitializeConfiguration();
        }

        protected virtual void InitializeConfiguration(string path = "appsettings.Development.json", bool optional = true, bool reloadOnChange = true)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional, reloadOnChange: reloadOnChange)
                .Build();
        }
    }
}