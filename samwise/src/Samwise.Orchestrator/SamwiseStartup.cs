using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samwise.Parsers;
using Samwise.Repositories.MongoDB;
using Samwise.Services;

namespace Samwise.Orchestrator
{
    public abstract class SamwiseStartup: IStartup
    {
        public IConfiguration Configuration { get; }
        protected IHostingEnvironment HostingEnvironment { get; }
        
        public SamwiseStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) =>
            (Configuration, HostingEnvironment) = (configuration, hostingEnvironment);
        
        
        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            services.AddParsers()
                .AddRepositories(Configuration)
                .AddServices();
            
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        void IStartup.Configure(IApplicationBuilder app)
        {
            Configure(app);
        }

        public abstract void ConfigureServices(IServiceCollection services);
        public abstract void Configure(IApplicationBuilder app);
    }
}