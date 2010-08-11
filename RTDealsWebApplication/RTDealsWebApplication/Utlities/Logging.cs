using System;
using System.Collections.Generic;

namespace Utilities
{
    public enum LoggingLevel
    {
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4,
        CRITICAL = 5
    }

    public class Logging
    {
        // min level for generating logs
        private static LoggingLevel minLevel = LoggingLevel.DEBUG;

        /// <summary>
        /// Dynamically change minimum logging level
        /// </summary>
        /// <param name="level"></param>
        public static void SetMinLevel(LoggingLevel level)
        {
            minLevel = level;
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="function"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Log(LoggingLevel level, string function, string message, Exception ex)
        {
            if (level >= minLevel)
            {
                // log message to file, email, pager
            }
        }
    }


}