using _03BarracksFactory.Contracts;

namespace _03BarracksFactory.Core.Command
{
    public abstract class Command : IExecutable
    {
        private string[] data;
        private IRepository repository;
        private IUnitFactory unitFactory;

        protected Command(string[] data, IRepository repository, IUnitFactory unitFactory)
        {
            Data = data;
            Repository = repository;
            UnitFactory = unitFactory;
        }

        public string[] Data
        {
            get => data;
            private set => data = value;
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

        public abstract string Execute();
    }
}