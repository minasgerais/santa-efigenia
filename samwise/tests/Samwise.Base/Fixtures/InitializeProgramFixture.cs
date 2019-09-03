using System;
using Microsoft.AspNetCore.Hosting;

namespace Samwise.Base.Fixtures
{
    public class InitializeProgramFixture: BaseFixture
    {
        public readonly IWebHost WebHost;
        
        protected override Type Startup => typeof(Runner.Startup);


        public InitializeProgramFixture()
        {
            WebHost = CreateBuilder();
        }
    }
}