using System;

namespace TakeOutTheTrash
{
    public class TakeOutTheTrashSettings
    {
        /// <summary>
        /// A timespan period for how often we repeat and check for clearing out the content recylce bin 
        /// </summary>
        /// <remarks>This defaults to 1 minute</remarks>
        public TimeSpan HowOftenWeRepeat { get; set; } = new TimeSpan(0, 1, 0);

        /// <summary>
        /// A timespan period in how long we should wait until the first iteration runs after the website has booted
        /// </summary>
        /// <remarks>This defaults to 1 minute</remarks>
        public TimeSpan DelayBeforeWeStart { get; set; } = new TimeSpan(0, 1, 0);
    }
}
