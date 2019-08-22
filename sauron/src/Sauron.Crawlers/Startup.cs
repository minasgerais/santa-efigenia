using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestSharp;
using Sauron.Abstractions.Crawlers;

namespace Sauron.Crawlers
{
    public static class Startup
    {
        public static IServiceCollection AddCrawlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IRestClient>(provider => new RestClient(configuration["SAURON_CRAWLER_BASE_URL"]));
            services.TryAddScoped(typeof(IWebCrawler<>), typeof(WebCrawler));

            return services;
        }
    }
}
