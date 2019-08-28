using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samwise.Orchestrator;

namespace Samwise.Runner
{
    public class Startup: SamwiseStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }
        
        public override void ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public override void Configure(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}