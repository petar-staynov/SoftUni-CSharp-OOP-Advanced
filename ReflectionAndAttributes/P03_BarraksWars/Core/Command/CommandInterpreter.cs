using System;
using System.Linq;
using System.Reflection;
using _03BarracksFactory.Contracts;

namespace _03BarracksFactory.Core.Command
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private IRepository repository;
        private IUnitFactory unitFactory;

        public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
        {
            Repository = repository;
            UnitFactory = unitFactory;
        }

        public IRepository Repository
        {
            get => repository;
            private set => repository = value;
        }

        public IUnitFactory UnitFactory
        {
            get => unitFactory;
            private set => unitFactory = value;
        }

        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name.ToLower() == commandName + "command");

            IExecutable executable = (IExecutable)
                Activator.CreateInstance(type, new object[] {data, this.repository, this.unitFactory});

            return executable;
        }
    }
}