using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Sauron.Apm
{
    public static class Startup
    {
        public static IApplicationBuilder UseApm(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseAllElasticApm(configuration);
            //Agent.Subscribe(new HttpDiagnosticsSubscriber());
            return app;
        }
    }
}
