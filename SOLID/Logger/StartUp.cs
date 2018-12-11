using System;
using LoggerApp.Appenders;
using LoggerApp.Core;
using LoggerApp.Core.Interfaces;
using LoggerApp.Layouts;
using LoggerApp.Loggers;
using LoggerApp.Loggers.Enums;


namespace LoggerApp
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IEngine engine = new Engine(commandInterpreter);
            engine.Run();
        }
    }
}