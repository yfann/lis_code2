using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Common.Logger
{
    public interface ISystemLogger
    {
        void Log(LogLevel logLevel, string message, Exception e = null);
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Error(string message, Exception e = null);
        void Fatal(string message);
        void Warn(string message, Exception e = null);
        LogLevel LoggerLevel { get; }
    }
}
