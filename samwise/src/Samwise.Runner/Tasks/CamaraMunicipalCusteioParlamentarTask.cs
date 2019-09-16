using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Samwise.Abstractions.Models.BeloHorizonte;
using Samwise.Abstractions.Services;
using Samwise.Scheduling.Tasks;

namespace Samwise.Runner.Tasks
{
    public class CamaraMunicipalCusteioParlamentarTask : ScheduledTask
    {
        protected override string Name => nameof(CamaraMunicipalCusteioParlamentarTask);
        protected override string Scheduler => "SAMWISE_TASK_SCHEDULER_DAYS_INTERVAL";

        private readonly IParseService<HtmlDocument, CamaraMunicipalCusteioParlamentar> _parseService;

        public CamaraMunicipalCusteioParlamentarTask(
            IConfiguration configuration,
            ILogger<ScheduledTask> logger,
            IParseService<HtmlDocument, CamaraMunicipalCusteioParlamentar> parseService
        )
            : base(configuration, logger)
        {
            _parseService = parseService;
        }

        public override Task ExecuteAsync()
        {
            return _parseService.ExecuteParseAsync();
        }
    }
}