using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;
using Samwise.Parsers.BeloHorizonte;

namespace Samwise.Parsers
{
    public static class Bootstrap
    {
        public static IServiceCollection AddParsers(this IServiceCollection services)
        {
            services.TryAddScoped<IParseData<HtmlDocument, CamaraMunicipalCusteioParlamentar>, CamaraMunicipalCusteioParlamentarParse>();
            
            return services;
        }
    }
}