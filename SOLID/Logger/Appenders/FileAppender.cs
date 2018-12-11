using System.IO;
using LoggerApp.Appenders.Interfaces;
using LoggerApp.Layouts.Interfaces;
using LoggerApp.Loggers.Enums;
using LoggerApp.Loggers.Interfaces;

namespace LoggerApp.Appenders
{
    public class FileAppender : Appender
    {
        private const string filePath = "log.txt";
        private readonly ILogFile logFile;

        public FileAppender(ILayout layout, ILogFile logFile) : base(layout)
        {
            this.logFile = logFile;
        }

        public override void Append(string dateTime, ReportLevel reportLevel, string message)
        {
            this.MessagesCount++;
            string content = string.Format(this.Layout.Format, dateTime, reportLevel, message) + "\n";
            this.logFile.Write(content);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"File size: {this.logFile.Size}";
        }
    }
}