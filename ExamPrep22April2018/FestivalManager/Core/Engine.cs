using System;
using System.Linq;
using System.Reflection;

namespace FestivalManager.Core
{
    using Contracts;
    using Controllers.Contracts;
    using IO.Contracts;

    class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private IFestivalController festivalCоntroller;
        private ISetController setCоntroller;

        public Engine(IReader reader, IWriter writer, IFestivalController festivalController,
            ISetController setController)
        {
            this.reader = reader;
            this.writer = writer;
            this.festivalCоntroller = festivalController;
            this.setCоntroller = setController;
        }

        public void Run()
        {
            while (true)
            {
                string input = this.reader.ReadLine();
                if (input == "END")
                {
                    break;
                }

                try
                {
                    var result = this.ProcessCommand(input);
                    this.writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    this.writer.WriteLine("ERROR: " + e.InnerException.Message);
                }
            }

            string end = this.festivalCоntroller.ProduceReport();

            this.writer.WriteLine("Results:");
            this.writer.WriteLine(end);
        }

        public string ProcessCommand(string input)
        {
            string[] commandArgs = input.Split(" ");

            var command = commandArgs[0];
            var commandParams = commandArgs.Skip(1).ToArray();

            string result;
            if (command == "LetsRock")
            {
                result = this.setCоntroller.PerformSets();
            }
            else
            {
                var festivalControllerMethod = this.festivalCоntroller.GetType()
                    .GetMethods()
                    .FirstOrDefault(x => x.Name == command);

                result = (string) festivalControllerMethod.Invoke(this.festivalCоntroller,
                    new object[] {commandParams});
            }

            return result;
        }
    }
}