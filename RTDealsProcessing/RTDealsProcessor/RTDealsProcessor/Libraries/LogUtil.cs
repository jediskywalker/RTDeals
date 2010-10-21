using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Libraries
{
    public enum LogLevel
    {
        DEBUG = 0,
        INFO = 1,
        WARN = 2,
        ERROR = 3,
        CRITICAL = 4
    }

    public class LogUtil
    {
        public static LogLevel MinLevel { get; set; }

        public static string LogFilePath { get; set; }

        private static List<string> _msgs = new List<string>();

        public static void Log(LogLevel level, string function, string message, Exception ex)
        {
            if (level < MinLevel) return;

            string msg = DateTime.Now.ToString("MM/dd HH:mm:ss.fff ") + string.Format("{0,-5} [{1}] {2} {3}", level, function, message, (ex == null ? "" : ex.Message));

            lock (_msgs)
            {
                _msgs.Add(msg);
            }

            if (_msgs.Count >= 10) WriteMsgsToFile();

        }

        public static void WriteMsgsToFile()
        {
            lock (_msgs)
            {
                if (_msgs.Count == 0) return;

                try
                {
                    if (string.IsNullOrEmpty(LogFilePath))
                        LogFilePath = "c:\\logs\\web.log";
                    StreamWriter sw = new StreamWriter(LogFilePath, true);
                    foreach (string msg in _msgs)
                    {
                        sw.WriteLine(msg);
                    }
                    sw.Close();
                    _msgs.Clear();
                }
                catch (Exception ex)
                {
                    // ignore for now
                }
            }
        }

    }


}