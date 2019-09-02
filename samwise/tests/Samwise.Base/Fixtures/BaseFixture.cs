using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Samwise.Base.Fixtures
{
    public abstract class BaseFixture
    {
        public T Resolve<T>() => _services.GetService<T>();
        public ICollection<T> ResolveList<T>() => _services.GetServices<T>().ToList();
        public HttpClient Client => _testServer.CreateClient();
        public IConfiguration Configuration => _services.GetService<IConfiguration>();

        protected TestServer TestServer => _testServer ?? (_testServer = new TestServer(CreateBuilder()));
        protected Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfiguration { get; set; }
        protected Action<IApplicationBuilder> Configure { get; set; }
        protected Action<WebHostBuilderContext, IServiceCollection> ConfigureServices { get; set; }

        protected virtual Type Startup { get; set; }
        protected virtual string Environment => "Test";
        protected virtual string PathJsonFile => "appsettings.json";
        protected virtual bool JsonFileOption => false;
        protected virtual bool JsonFileReloadOnChange => true;

        private IServiceProvider _services => TestServer.Host.Services;
        private TestServer _testServer;


        public BaseFixture()
        {
            ConfigureAppConfiguration = (context, builder) => { };
            ConfigureServices = (context, collection) => { };
            Configure = builder => { };
        }


        protected WebHostBuilder CreateBuilder()
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
            return hostBuilder;
        }
    }
}