using System;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web.Scheduling;

namespace TakeOutTheTrash
{
    public class Component : IComponent
    {
        private IProfilingLogger _logger;
        private IRuntimeState _runtime;
        private IContentService _contentService;
        private BackgroundTaskRunner<IBackgroundTask> _cleanUpYourRoomRunner;

        public Component(IProfilingLogger logger, IRuntimeState runtime, IContentService contentService)
        {
            _logger = logger;
            _runtime = runtime;
            _contentService = contentService;
            _cleanUpYourRoomRunner = new BackgroundTaskRunner<IBackgroundTask>("TakeOutTheTrash", _logger);
        }

        public void Initialize()
        {
            int delayBeforeWeStart = 60000; // 60000ms = 1min
            int howOftenWeRepeat = 60000; //60000ms = 1min

            var task = new CleanRoom(_cleanUpYourRoomRunner, delayBeforeWeStart, howOftenWeRepeat, _runtime, _logger, _contentService);

            //As soon as we add our task to the runner it will start to run (after its delay period)
            _cleanUpYourRoomRunner.TryAdd(task);
        }

        public void Terminate()
        {
        }
    }
}
