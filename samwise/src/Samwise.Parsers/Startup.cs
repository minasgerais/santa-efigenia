using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Parsers;
using Samwise.Parsers.BeloHorizonte;

namespace Samwise.Parsers
{
    public static class Startup
    {
        public static IServiceCollection AddParsers(this IServiceCollection services)
        {
            services.TryAddScoped<IParseData<HtmlDocument, IEnumerable<CamaraMunicipalCusteioParlamentar>>, CamaraMunicipalCusteioParlamentarParse>();
            
            return services;
        }
    }
}