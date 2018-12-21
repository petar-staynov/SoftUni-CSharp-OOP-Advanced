using CosmosX.Entities.Modules.Contracts;

namespace CosmosX.Entities.Modules
{
    public abstract class BaseModule : IModule
    {
        private int id;

        protected BaseModule(int id)
        {
            this.Id = id;
        }

        public int Id
        {
            get => this.id;
            set => this.id = value;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} Module - {this.Id}\n";
        }
    }
}