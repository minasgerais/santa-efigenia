using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestSharp;
using Sauron.Abstractions.Crawlers;

namespace Sauron.Crawlers
{
    public static class Startup
    {
        public static IServiceCollection AddCrawlers(this IServiceCollection services)
        {
            services.TryAddScoped<IRestClient>(provider =>
                new RestClient("https://www.cmbh.mg.gov.br/sites/all/modules/execucao_orcamentaria_custeio")
            );
            services.TryAddScoped(typeof(IWebCrawler<>), typeof(WebCrawler));
            return services;
        }
    }
}
