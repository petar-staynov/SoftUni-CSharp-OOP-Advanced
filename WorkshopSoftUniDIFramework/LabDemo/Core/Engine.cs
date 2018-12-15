using System;
using LabDemo.Core.Contracts;
using LabDemo.Models.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LabDemo.Core
{
    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader consoleReader;

        public Engine(IWriter writer, IReader consoleReader)
        {
            this.writer = writer;
            this.consoleReader = consoleReader;
        }

        public void Run()
        {
            string content = consoleReader.Read();
            this.writer.Write(content);
        }
    }
}