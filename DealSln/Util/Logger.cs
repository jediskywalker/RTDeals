using System;
using System.Collections.Generic;
using System.IO;

namespace Util
{
    public enum LogLevel
    {
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4
    }

    public class Logger
    {
        // min level for generating logs
        private static LogLevel minLevel;

        // when messages reach this size, flush to file
        private static int msgQueueSize;

        // remember last time flushed. If longer than certain duration, flush again
        private static DateTime lastFlushTime = DateTime.Now;

        // how many seconds to wait until flush; 
        // the check is only trigger by a logging event, so not guarranteed to flush by that time
        private static int flushWaitTime;

        // log file path
        private static string logPath;

        // log file name
        private static string logFile;

        // log file size
        private static int logFileSize;

        // message queues
        private static List<string> logMsgs = new List<string>();   

        private Logger() { } // hide constructor

        // initialize parameters
        static Logger()
        {
            minLevel = LogLevel.DEBUG;      // debug level
            msgQueueSize = 20;              // 20 messages
            flushWaitTime = 5;              // 5 secs
            logFileSize = 10 * 100000;      // 10 MB
            logPath = "c:\\logs";           // log file directory
            logFile = System.AppDomain.CurrentDomain.FriendlyName + ".log";
        }

        private static void checkLogSize()
        {
            //get File Attributes
            try
            {
                FileInfo fi = new FileInfo(logFile);
                if (fi.Length > logFileSize)
                {
                    DateTime fileTime = File.GetLastWriteTime(logFile);
                    string fileTimeStr = fileTime.Year + "-" + fileTime.Month + "-" + fileTime.Day + "_" + fileTime.Hour + "-" + fileTime.Minute + "-" + fileTime.Second;
                    
                    //rename file
                    File.Move(logFile, logPath + "\\" + fi.Name + "-" + fileTimeStr + ".log.archive");

                    //create new log file
                    using (StreamWriter sw = File.CreateText(logFile))
                    {
                        sw.WriteLine("Starting New Log");
                        sw.WriteLine("*********" + System.DateTime.Now + "***********");
                    }
                }
            }
            catch (Exception)
            {
               
            }
        }

        private static string ExceptionMessage(Exception ex)
        {
            if (ex == null) return "";
            string msg = "";

            msg += " {EX:" + ex.Message;

            if (ex.InnerException != null)
            {
                string innermsg = ex.InnerException.Message; // ExceptionMessage(ex.InnerException);
                msg += "<Inner:" + innermsg + ">";
            }
            msg += "}";

            return msg;
        }



        /// Dynamically change minimum logging level
        /// </summary>
        /// <param name="level"></param>
        public static void SetMinLevel(LogLevel level)
        {
            minLevel = level;
        }

        /// <summary>
        /// Dyanamically change flush count
        /// </summary>
        /// <param name="count"></param>
        public static void SetMsgQueueSize(int size)
        {
            msgQueueSize = size;
        }

        /// <summary>
        /// Dyanamically change flush count
        /// </summary>
        /// <param name="count"></param>
        public static void SetLogFileName(string filename)
        {
            logFile = filename;
        }

        public static void LogDebug(string message)
        {
            LogDebug("", message);
        }

        public static void LogDebug(string function, string message)
        {
            Log(LogLevel.DEBUG, function, message, null);
        }

        public static void LogInfo(string message)
        {
            LogInfo("", message);
        }

        public static void LogInfo(string function, string message)
        {
            Log(LogLevel.INFO, function, message, null);
        }

        public static void LogWarn(string message)
        {
            LogWarn("", message);
        }

        public static void LogWarn(string function, string message)
        {
            Log(LogLevel.WARN, function, message, null);
        }

        public static void LogWarn(string function, string message, Exception ex)
        {
            Log(LogLevel.WARN, function, message, ex);
        }

        public static void LogError(string message)
        {
            LogError("", message);
        }

        public static void LogError(string function, string message)
        {
            Log(LogLevel.ERROR, function, message, null);
        }

        public static void LogError(string function, string message, Exception ex)
        {
            Log(LogLevel.ERROR, function, message, ex);
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="level"></param>
        /// <param name="function"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Log(LogLevel level, string function, string message, Exception ex)
        {
            lock (logMsgs)
            {
                if (level < minLevel) return;

                // log message to file
                string msg = DateTime.Now.ToString("MM/dd HH:mm:ss.fff ") + string.Format("{0,-5} [{1}] {2}", level, function, message);
                if (ex != null)
                    msg += ExceptionMessage(ex);

                lock (logMsgs)
                {
                    logMsgs.Add(msg);
                }

                if (level >= LogLevel.WARN)
                    FlushToFile();
                else if (logMsgs.Count >= msgQueueSize)
                    FlushToFile();
                else if (lastFlushTime.AddSeconds(flushWaitTime) < DateTime.Now)
                    FlushToFile();
            }
        }

        public static void FlushToFile()
        {
            lock (logMsgs)
            {
                lastFlushTime = DateTime.Now;

                if (logMsgs.Count == 0) return;

                checkLogSize();

                try
                {
                    string logFilePath = "c:\\logs\\" + logFile;
                    StreamWriter sw = new StreamWriter(logFilePath, true);
                    foreach (string msg in logMsgs)
                    {
                        sw.WriteLine(msg);
                    }
                    sw.Close();
                    logMsgs.Clear();
                }
                catch (Exception)
                {
                    // ignore for now
                }
            }

        }

    }

}