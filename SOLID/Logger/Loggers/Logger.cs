using System.Diagnostics;
using LoggerApp.Appenders.Interfaces;
using LoggerApp.Loggers.Enums;
using LoggerApp.Loggers.Interfaces;

namespace LoggerApp.Loggers
{
    public class Logger : ILogger
    {
        private readonly IAppender consoleAppender;
        private readonly IAppender fileAppender;

        public Logger(IAppender consoleAppender)
        {
            this.consoleAppender = consoleAppender;
        }

        public Logger(IAppender consoleAppender, IAppender fileAppender) : this(consoleAppender)
        {
            this.fileAppender = fileAppender;
        }

        public void Info(string dateTime, string message)
        {
            this.AppendMessage(dateTime, ReportLevel.Info, message);
        }

        public void Warning(string dateTime, string message)
        {
            this.AppendMessage(dateTime, ReportLevel.Warning, message);
        }

        public void Error(string dateTime, string message)
        {
            this.AppendMessage(dateTime, ReportLevel.Error, message);
        }

        public void Critical(string dateTime, string message)
        {
            this.AppendMessage(dateTime, ReportLevel.Critical, message);
        }

        public void Fatal(string dateTime, string message)
        {
            this.AppendMessage(dateTime, ReportLevel.Fatal, message);
        }


        private void AppendMessage(string dateTime, ReportLevel reportLevel, string message)
        {
            this.consoleAppender?.Append(dateTime, reportLevel, message);
            this.fileAppender?.Append(dateTime, reportLevel, message);
        }
    }
}