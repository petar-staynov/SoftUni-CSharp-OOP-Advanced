﻿using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Modules.Absorbing.Contracts;
using CosmosX.Entities.Modules.Energy.Contracts;
using CosmosX.Entities.Reactors.Contracts;

namespace CosmosX.Entities.Reactors
{
    public abstract class BaseReactor : IReactor
    {
        private readonly IContainer moduleContainer;

        protected BaseReactor(int id, IContainer moduleContainer)
        {
            this.Id = id;
            this.moduleContainer = moduleContainer;
        }

        public int Id { get; private set; }


        public virtual long TotalEnergyOutput
            => this.moduleContainer.TotalEnergyOutput;

        public virtual long TotalHeatAbsorbing
            => this.moduleContainer.TotalHeatAbsorbing;

        //TODO maybe fix this
        public int ModuleCount
            => this.moduleContainer.ModulesByInput.Count;

        public void AddEnergyModule(IEnergyModule energyModule)
        {
            this.moduleContainer.AddEnergyModule(energyModule);
        }

        public void AddAbsorbingModule(IAbsorbingModule absorbingModule)
        {
            this.moduleContainer.AddAbsorbingModule(absorbingModule);
        }

        public override string ToString()
        {
            string result = $"{this.GetType().Name} - {this.Id}\n" +
                            $"Energy Output: {this.TotalEnergyOutput}\n" +
                            $"Heat Absorbing: {this.TotalHeatAbsorbing}\n" +
                            $"Modules: {this.ModuleCount}";

            return result;
        }
    }
}