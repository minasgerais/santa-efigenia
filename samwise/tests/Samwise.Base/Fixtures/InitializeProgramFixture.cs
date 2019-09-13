using System;
using Microsoft.AspNetCore.Hosting;

namespace Samwise.Base.Fixtures
{
    public class InitializeProgramFixture: BaseFixture
    {
        private readonly IWebHost _webHost;
        
        protected override Type Startup => typeof(Runner.Startup);


        public InitializeProgramFixture()
        {
            _webHost = CreateBuilder();
        }
    }
}