using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.HostedServices;
using Umbraco.Extensions;

namespace TakeOutTheTrash
{
    public class CleanRoom : RecurringHostedServiceBase
    {
        private readonly ILogger<CleanRoom> _logger;
        private readonly IProfilingLogger _profilingLogger;
        private readonly IContentService _contentService;
        private readonly IServerRoleAccessor _serverRole;
        private readonly IRuntimeState _runtimeState;

        public CleanRoom(
            ILogger<CleanRoom> logger, 
            IProfilingLogger profilingLogger, 
            IContentService contentService, 
            IServerRoleAccessor serverRole, 
            IRuntimeState runtimeState,
            IOptions<TakeOutTheTrashSettings> trashSettings) 
            : base(trashSettings.Value.HowOftenWeRepeat, trashSettings.Value.DelayBeforeWeStart)
        {
            _logger = logger;
            _profilingLogger = profilingLogger;
            _contentService = contentService;   
            _serverRole = serverRole;
            _runtimeState = runtimeState;
        }

        public override async Task PerformExecuteAsync(object state)
        {
            // Ensure this code is run when application is set to run
            // and not Installing, Upgrading etc...
            if(_runtimeState.Level != Umbraco.Cms.Core.RuntimeLevel.Run)
                return;

            // Do not run the code on replicas nor unknown role servers
            // ONLY run for Master server or Single
            switch (_serverRole.CurrentServerRole)
            {
                case ServerRole.Replica:
                    _logger.LogDebug("Does not run on replica servers.");
                    return;
                case ServerRole.Unknown:
                    _logger.LogDebug("Does not run on servers with unknown role.");
                    return;
            }

            if (_contentService.RecycleBinSmells())
            {
                // Take out the trash
                using (_profilingLogger.TraceDuration<CleanRoom>("Mum, I am emptying out the bin", "Its all clean now!"))
                {
                    // Pretty dumb example just empties it out whatever is in it
                    _contentService.EmptyRecycleBin(userId: -1);
                }
            }
        }
    }
}
