using System;
using System.Collections.Generic;
using LoggerApp.Appenders.Factory;
using LoggerApp.Appenders.Factory.Interfaces;
using LoggerApp.Appenders.Interfaces;
using LoggerApp.Core.Interfaces;
using LoggerApp.Layouts.Factory;
using LoggerApp.Layouts.Factory.Interfaces;
using LoggerApp.Layouts.Interfaces;
using LoggerApp.Loggers.Enums;

namespace LoggerApp.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private ICollection<IAppender> appenders;
        private IAppenderFactory appenderFactory;
        private ILayoutFactory layoutFactory;

        public CommandInterpreter()
        {
            this.appenders = new List<IAppender>();
            this.appenderFactory = new AppenderFactory();
            this.layoutFactory = new LayoutFactory();
        }

        public void AddAppender(string[] args)
        {
            string appenderType = args[0];
            string layoutType = args[1];
            ReportLevel reportLevel = ReportLevel.Info;
            if (args.Length >= 3)
            {
                reportLevel = Enum.Parse<ReportLevel>(args[2], true);
            }

            ILayout layout = this.layoutFactory.CreateLayout(layoutType);

            IAppender appender = appenderFactory.CreateAppender(appenderType, layout);
            this.appenders.Add(appender);
        }

        public void AddMessages(string[] args)
        {
            ReportLevel reportLevel = Enum.Parse<ReportLevel>(args[0], true);
            string dateTime = args[1];
            string message = args[2];

            foreach (IAppender appender in appenders)
            {
                appender.Append(dateTime, reportLevel, message);

            }
        }

        public void PrintInfo()
        {
            Console.WriteLine("Logger info");

            foreach (IAppender appender in appenders)
            {
                Console.WriteLine(appender.ToString());
            }
        }
    }
}