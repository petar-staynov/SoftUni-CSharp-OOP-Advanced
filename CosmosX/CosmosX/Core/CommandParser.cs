using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CosmosX.Core.Contracts;

//using CosmosX.Core.Factories;

namespace CosmosX.Core
{
    public class CommandParser : ICommandParser
    {
        private const string CommandNameSuffix = "Command";

        private readonly IManager reactorManager;


        public CommandParser(IManager reactorManager)
        {
            this.reactorManager = reactorManager;
        }

        public string Parse(IList<string> arguments)
        {
            string command = arguments[0];

            string[] commandArguments = arguments.Skip(1).ToArray();

            string managerType = this.reactorManager.GetType().Name;

            Type reactorManagerType =
                Assembly
                    .GetCallingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.Name == managerType);

            MethodInfo[] methods = reactorManagerType.GetMethods();

            var methodToCall = methods.FirstOrDefault(m => m.Name.Contains(command));

            var result = methodToCall.Invoke(this.reactorManager, new object[] {commandArguments});

            return result.ToString();
        }
    }
}