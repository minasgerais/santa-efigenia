using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sauron.Crawlers;
using Sauron.Repositories.MongoDB;
using System;

namespace Sauron.Orchestrator
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        protected IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            (Configuration, HostingEnvironment) = (configuration, hostingEnvironment);
        }

        public virtual void ConfigureServices(IServiceCollection services) { }

        public virtual void Configure(IApplicationBuilder app) { }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            services.AddCrawlers(Configuration);
            services.AddRepositories(Configuration);

            ConfigureServices(services);

            return services.BuildServiceProvider();
        }

        void IStartup.Configure(IApplicationBuilder app)
        {
            Configure(app);
        }
    }
}
