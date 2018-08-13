using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Lis.Common.Logger
{
    public class SystemLogger : ISystemLogger
    {
        private static readonly ILog logger = LogManager.GetLogger("SystemLogger");
        private readonly object objLock = new object();

        public LogLevel LoggerLevel
        {
            get
            {
                if (logger.IsDebugEnabled)
                {
                    return LogLevel.Debug;
                }
                else if (logger.IsInfoEnabled)
                {
                    return LogLevel.Info;
                }
                else if (logger.IsWarnEnabled)
                {
                    return LogLevel.Warn;
                }
                else if (logger.IsErrorEnabled)
                {
                    return LogLevel.Error;
                }
                else if (logger.IsFatalEnabled)
                {
                    return LogLevel.Fatal;
                }
                return LogLevel.Fatal;
            }
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Warn(string message, Exception e = null)
        {
            Log(LogLevel.Warn, message, e);
        }

        public void Error(string message, Exception e = null)
        {
            Log(LogLevel.Error, message, e);
        }

        public void Fatal(string message)
        {
            Log(LogLevel.Fatal, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }        

        public void Log(LogLevel logLevel, string message, Exception e = null)
        {
            lock (this.objLock)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        logger.Debug(message);
                        break;

                    case LogLevel.Info:
                        logger.Info(message);
                        break;

                    case LogLevel.Warn:
                        if (e != null)
                        {
                            logger.Warn(message, e);
                        }
                        else
                        {
                            logger.Warn(message);
                        }
                        break;

                    case LogLevel.Error:
                        if (e != null)
                        {
                            logger.Error(message, e);
                        }
                        else
                        {
                            logger.Error(message);
                        }
                        break;

                    case LogLevel.Fatal:
                        logger.Fatal(message);
                        break;

                    default:
                        logger.Info(message);
                        break;
                }
            }
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        static SystemLogger()
        {
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "log4net.config");
            if (fileName.StartsWith(@"file:\"))
            {
                fileName = fileName.Substring(@"file:\".Length);
            }
            XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
        }
    }
}
