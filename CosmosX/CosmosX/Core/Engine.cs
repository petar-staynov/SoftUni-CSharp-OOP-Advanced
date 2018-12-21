using System;
using CosmosX.Core.Contracts;
using CosmosX.IO.Contracts;

namespace CosmosX.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = false;
        }

        public void Run()
        {
            this.isRunning = true;
            while (isRunning)
            {
                var input = reader.ReadLine().Split(' ');
                if (input[0] == "Exit")
                {
                    string result = this.commandParser.Parse(input);
                    writer.WriteLine(result);
                    isRunning = false;
                    break;
                }

                try
                {
                    string result = this.commandParser.Parse(input);
                    writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    writer.WriteLine(e.InnerException.Message);
                }
            }
        }
    }
}