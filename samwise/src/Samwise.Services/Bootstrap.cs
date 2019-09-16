using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Services;

namespace Samwise.Services
{
    public static class Bootstrap
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IParseService<HtmlDocument, CamaraMunicipalCusteioParlamentar>, CamaraMunicipalCusteioParlamentarParseService>();
            
            return services;
        }
    }
}