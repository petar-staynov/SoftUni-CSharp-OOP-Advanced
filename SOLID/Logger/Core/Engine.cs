using System;
using System.Collections.Generic;
using LoggerApp.Appenders.Interfaces;
using LoggerApp.Core.Interfaces;

namespace LoggerApp.Core
{
    public class Engine : IEngine
    {
        private ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split();

                this.commandInterpreter.AddAppender(inputArgs);
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "end")
                {
                    break;
                }

                string[] inputArgs = input.Split('|');
                this.commandInterpreter.AddMessages(inputArgs);
            }

            this.commandInterpreter.PrintInfo();
        }
    }
}