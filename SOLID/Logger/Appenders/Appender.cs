using LoggerApp.Appenders.Interfaces;
using LoggerApp.Layouts.Interfaces;
using LoggerApp.Loggers.Enums;

namespace LoggerApp.Appenders
{
    public abstract class Appender : IAppender
    {
        private readonly ILayout layout;

        protected Appender(ILayout layout)
        {
            this.layout = layout;
        }

        protected ILayout Layout
        {
            get { return this.layout; }
        }

        public ReportLevel ReportLevel { get; set; }
        public int MessagesCount { get; protected set; }

        public abstract void Append(string dateTime, ReportLevel reportLevel, string message);

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, " +
                   $"Layout type: {this.Layout.GetType().Name}, " +
                   $"Report level: {this.ReportLevel.ToString().ToUpper()}, " +
                   $"Messages appended: {this.MessagesCount}";
        }
    }
}