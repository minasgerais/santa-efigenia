using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Samwise.Base.Fixtures
{
    public abstract class BaseFixture
    {
        public T Resolve<T>() => Services.GetService<T>();
        public ICollection<T> ResolveList<T>() => Services.GetServices<T>().ToList();
        public IConfiguration Configuration => Services.GetService<IConfiguration>();

        
        protected Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfiguration { get; set; }
        protected Action<IApplicationBuilder> Configure { get; set; }
        protected Action<WebHostBuilderContext, IServiceCollection> ConfigureServices { get; set; }
        
        protected IWebHost WebHost=> _webHost ?? (_webHost = CreateBuilder());
        
        protected virtual Type Startup { get; }
        protected virtual  string Environment => "Test";
        protected virtual string PathJsonFile => "appsettings.json";
        protected virtual bool JsonFileOption => false;
        protected virtual bool JsonFileReloadOnChange => true;
        
        private IServiceProvider Services => WebHost.Services;
        private IWebHost _webHost;


        public BaseFixture()
        {
            ConfigureAppConfiguration = (context, builder) => { };
            ConfigureServices = (context, collection) => { };
            Configure = builder => { };
        }


        protected IWebHost CreateBuilder()
        {
            WebHostBuilder hostBuilder = new WebHostBuilder();
            hostBuilder.UseEnvironment(Environment);
            hostBuilder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddJsonFile(PathJsonFile, JsonFileOption, reloadOnChange: JsonFileReloadOnChange);
                ConfigureAppConfiguration(context, configurationBuilder);
            });
            hostBuilder.ConfigureServices(ConfigureServices);
            hostBuilder.Configure(Configure);
            if (Startup != null)
                hostBuilder.UseStartup(Startup);
            return hostBuilder.Build();
        }
    }
}