using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Sync;
using Umbraco.Web.Scheduling;

namespace TakeOutTheTrash
{
    public class CleanRoom : RecurringTaskBase
    {
        private IRuntimeState _runtime;
        private IProfilingLogger _logger;
        private IContentService _contentService;

        public CleanRoom(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, IRuntimeState runtime, IProfilingLogger logger, IContentService contentService)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _runtime = runtime;
            _logger = logger;
            _contentService = contentService;
        }

        public override bool PerformRun()
        {
            // Do not run the code on replicas nor unknown role servers
            // ONLY run for Master server or Single
            switch (_runtime.ServerRole)
            {
                case ServerRole.Replica:
                    _logger.Debug<CleanRoom>("Does not run on replica servers.");
                    return true; // We return true to try again as the server role may change!
                case ServerRole.Unknown:
                    _logger.Debug<CleanRoom>("Does not run on servers with unknown role.");
                    return true; // We return true to try again as the server role may change!
            }

            if (_contentService.RecycleBinSmells())
            {
                // Take out the trash
                using (_logger.TraceDuration<CleanRoom>("Mum, I am emptying out the bin", "Its all clean now!"))
                {
                    _contentService.EmptyRecycleBin(userId: -1);
                }
            }

            // If we want to keep repeating - we need to return true
            // But if we run into a problem/error & want to stop repeating - return false
            return true;
        }

        public override bool IsAsync => false;
    }
}
