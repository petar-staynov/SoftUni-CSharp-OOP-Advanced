using System.Collections.Generic;
using System.Linq;
using CosmosX.Core.Contracts;
using CosmosX.Entities.CommonContracts;
using CosmosX.Entities.Containers;
using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Modules.Absorbing;
using CosmosX.Entities.Modules.Absorbing.Contracts;
using CosmosX.Entities.Modules.Contracts;
using CosmosX.Entities.Modules.Energy;
using CosmosX.Entities.Modules.Energy.Contracts;
using CosmosX.Entities.Reactors;
using CosmosX.Entities.Reactors.Contracts;
using CosmosX.Entities.Reactors.ReactorFactory;

using CosmosX.Utils;

namespace CosmosX.Core
{
    public class ReactorManager : IManager
    {
        private int currentId;
        private readonly IDictionary<int, IIdentifiable> allObjects;
        private readonly IDictionary<int, IReactor> reactors;
        private readonly IDictionary<int, IModule> modules;

        private ReactorFactory reactorFactory;

        public ReactorManager()
        {
            this.currentId = Constants.StartingId;
            this.allObjects = new Dictionary<int, IIdentifiable>();
            this.reactors = new Dictionary<int, IReactor>();
            this.modules = new Dictionary<int, IModule>();

            this.reactorFactory = new ReactorFactory();
        }

        public string ReactorCommand(IList<string> arguments)
        {
            string reactorType = arguments[0];
            int additionalParameter = int.Parse(arguments[1]);
            int moduleCapacity = int.Parse(arguments[2]);

            IContainer container = new ModuleContainer(moduleCapacity);

            IReactor reactor =
                this.reactorFactory.CreateReactor(reactorType, this.currentId, container, additionalParameter);


            this.reactors.Add(reactor.Id, reactor);
            this.allObjects.Add(reactor.Id, reactor);

            this.currentId++;


            string result = string.Format(Constants.ReactorCreateMessage, reactorType, reactor.Id); 
            return result;
        }

        public string ModuleCommand(IList<string> arguments)
        {
            int reactorId = int.Parse(arguments[0]);
            string moduleType = arguments[1];
            int additionalParameter = int.Parse(arguments[2]);

            int moduleId = 0;


            switch (moduleType)
            {
                case "CryogenRod":
                    IEnergyModule cryogenRod = new CryogenRod(this.currentId, additionalParameter);
                    this.modules.Add(cryogenRod.Id, cryogenRod);

                    this.reactors[reactorId].AddEnergyModule(cryogenRod);
                    this.allObjects.Add(cryogenRod.Id, cryogenRod);

                    moduleId = cryogenRod.Id;
                    break;
                case "HeatProcessor":
                    IAbsorbingModule heatProcessor = new HeatProcessor(this.currentId, additionalParameter);
                    this.reactors[reactorId].AddAbsorbingModule(heatProcessor);
                    this.modules.Add(heatProcessor.Id, heatProcessor);
                    this.allObjects.Add(heatProcessor.Id, heatProcessor);

                    moduleId = heatProcessor.Id;
                    break;
                case "CooldownSystem":
                    IAbsorbingModule cooldownSystem = new CooldownSystem(this.currentId, additionalParameter);
                    this.reactors[reactorId].AddAbsorbingModule(cooldownSystem);
                    this.allObjects.Add(cooldownSystem.Id, cooldownSystem);
                    this.modules.Add(cooldownSystem.Id, cooldownSystem);

                    moduleId = cooldownSystem.Id;
                    break;
            }
            this.currentId++;

            string result = string.Format(Constants.ModuleCreateMessage, moduleType, moduleId, reactorId);
            return result;
        }

        public string ReportCommand(IList<string> arguments)
        {
            int id = int.Parse(arguments[0]);
            var objectToReport = this.allObjects[id];


            if (objectToReport is IReactor)
            {
                return objectToReport.ToString();
            }
            else if (objectToReport is IModule)
            {
                return objectToReport.ToString();
            }

            return null;
        }

        public string ExitCommand(IList<string> arguments)
        {
            long cryoReactorCount = this.reactors
                .Values
                .Count(r => r is CryoReactor);

            long heatReactorCount = this.reactors
                .Values
                .Count(r => r is HeatReactor);

            long energyModulesCount = this.modules
                .Values
                .Count(m => m is IEnergyModule);

            long absorbingModulesCount = this.modules
                .Values
                .Count(m => m is IAbsorbingModule);

            long totalEnergyOutput = this.reactors
                .Values
                .Sum(r => r.TotalEnergyOutput);

            long totalHeatAbsorbing = this.reactors
                .Values
                .Sum(r => r.TotalHeatAbsorbing);

            string result = $"Cryo Reactors: {cryoReactorCount}\n" +
                            $"Heat Reactors: {heatReactorCount}\n" +
                            $"Energy Modules: {energyModulesCount}\n" +
                            $"Absorbing Modules: {absorbingModulesCount}\n" +
                            $"Total Energy Output: {totalEnergyOutput}\n" +
                            $"Total Heat Absorbing: {totalHeatAbsorbing}";

            return result;
        }
    }
}