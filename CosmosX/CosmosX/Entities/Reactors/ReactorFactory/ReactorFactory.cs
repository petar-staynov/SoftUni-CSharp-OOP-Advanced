using System;
using System.Linq;
using System.Reflection;
using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Reactors.Contracts;
using CosmosX.Entities.Reactors.ReactorFactory.Contracts;

namespace CosmosX.Entities.Reactors.ReactorFactory
{
    public class ReactorFactory : IReactorFactory
    {
        public IReactor CreateReactor(string reactorTypeName, int id, IContainer moduleContainer,
            int additionalParameter)
        {
            string reactorFullName = reactorTypeName + "Reactor";

            Type reactorType =
                Assembly
                    .GetCallingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.Name.Contains(reactorTypeName));

            object[] parameters = new object[] {id, moduleContainer, additionalParameter};

            var reactorObject = Activator.CreateInstance(reactorType, parameters);

            return (IReactor) reactorObject;
        }
    }
}