﻿using LoggerApp.Loggers.Enums;

namespace LoggerApp.Appenders.Interfaces
{
    public interface IAppender
    {
        void Append(string dateTime, ReportLevel reportLevel, string message);

        ReportLevel ReportLevel { get; set; }
        int MessagesCount { get; }

    }
}