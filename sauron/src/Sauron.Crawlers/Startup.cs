using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestSharp;
using Sauron.Abstractions.Crawlers;
using Sauron.Abstractions.Extensions;
using Sauron.Abstractions.Models;

namespace Sauron.Crawlers
{
    public static class Startup
    {
        public static IServiceCollection AddCrawlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IRestClient>(provider => new RestClient(configuration.TryGet("SAURON_CRAWLER_BASE_URL")));
            services.TryAddScoped(typeof(IWebCrawler<RawData>), typeof(WebCrawler));

            return services;
        }
    }
}
