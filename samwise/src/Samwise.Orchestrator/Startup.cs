using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Samwise.Orchestrator
{
    // TODO avaliar um injetor MorganFreeman abstraido para as aplicações
    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public void Configure(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}