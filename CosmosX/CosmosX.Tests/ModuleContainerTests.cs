using CosmosX.Entities.Containers;
using CosmosX.Entities.Modules.Absorbing;
using CosmosX.Entities.Modules.Contracts;
using CosmosX.Entities.Modules.Energy;
using CosmosX.Entities.Modules.Energy.Contracts;
using CosmosX.Entities.Reactors;
using CosmosX.Entities.Reactors.Contracts;

namespace CosmosX.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ModuleContainerTests
    {
        [Test]
        public void FullTest()
        {
            //Test Add Energy Module
            //Test AddAbsorbingModule
            //Test RemoveOldestModule

            /*
             * Reactor Cryo 10 10
               Reactor Cryo 2 15
               Module 1 CryogenRod 100
               Module 1 CryogenRod 100
               Module 1 CryogenRod 100
               Module 1 CryogenRod 100
               Module 2 CryogenRod 100
               Module 1 HeatProcessor 10000
               Report 1
               Exit
             */
            //{reactorType} {additionalParameter} {moduleCapacity}
            //•	Module {reactorId} {type} {additionalParameter}

            var moduleContainer1 = new ModuleContainer(10);
            moduleContainer1.AddEnergyModule(new CryogenRod(1, 100));
            moduleContainer1.AddAbsorbingModule(new HeatProcessor(2, 50));

            var moduleContainer2 = new ModuleContainer(10);
            moduleContainer2.AddEnergyModule(new CryogenRod(1, 50));
            moduleContainer2.AddAbsorbingModule(new CooldownSystem(2, 100));

            var moduleContainer3 = new ModuleContainer(1);
            moduleContainer3.AddEnergyModule(new CryogenRod(1, 50));
            moduleContainer3.AddAbsorbingModule(new CooldownSystem(2, 100));
            moduleContainer3.AddAbsorbingModule(new HeatProcessor(3, 100));

            var modulesByInput1 = moduleContainer1.ModulesByInput.Count;
            var modulesByInput2 = moduleContainer2.ModulesByInput.Count;
            var modulesByInput3 = moduleContainer3.ModulesByInput.Count;

            Assert.That(modulesByInput1, Is.EqualTo(2));
            Assert.That(modulesByInput2, Is.EqualTo(2));
            Assert.That(modulesByInput3, Is.EqualTo(1));

            var totalEnergyOutput1 = moduleContainer1.TotalEnergyOutput;
            var totalEnergyOutput2 = moduleContainer2.TotalEnergyOutput;
            var totalEnergyOutput3 = moduleContainer3.TotalEnergyOutput;

            Assert.That(totalEnergyOutput1, Is.EqualTo(100));
            Assert.That(totalEnergyOutput2, Is.EqualTo(50));
            Assert.That(totalEnergyOutput3, Is.EqualTo(0));

            var totalHeatAbsorbing1 = moduleContainer1.TotalHeatAbsorbing;
            var totalHeatAbsorbing2 = moduleContainer2.TotalHeatAbsorbing;
            var totalHeatAbsorbing3 = moduleContainer3.TotalHeatAbsorbing;

            Assert.That(totalHeatAbsorbing1, Is.EqualTo(50));
            Assert.That(totalHeatAbsorbing2, Is.EqualTo(100));
            Assert.That(totalHeatAbsorbing3, Is.EqualTo(100));

            var moduleContainer1String = moduleContainer1.ToString();
            var moduleContainer2String = moduleContainer2.ToString();
            var moduleContainer3String = moduleContainer3.ToString();


        }
    }
}
