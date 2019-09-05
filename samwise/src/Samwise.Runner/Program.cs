﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Services;

namespace Samwise.Runner
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args).Build();
            var serviceProvider = builder.Services;

            var service = serviceProvider.GetRequiredService<IParseService<HtmlDocument, CamaraMunicipalCusteioParlamentar>>();
            await service.ExecuteParseAsync();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}