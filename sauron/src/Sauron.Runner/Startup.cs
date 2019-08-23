using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sauron.JobScheduler;
using Sauron.Runner.Jobs;

namespace Sauron.Runner
{
    public class Startup : Orchestrator.Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        { }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.ScheduleJob<SearchRawDataJob>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
