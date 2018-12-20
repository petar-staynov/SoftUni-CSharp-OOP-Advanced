using System;
using System.Linq;
using System.Reflection;

namespace Travel.Entities.Factories
{
    using Contracts;
    using Items;
    using Items.Contracts;

    public class ItemFactory : IItemFactory
    {
        public IItem CreateItem(string type)
        {
            Type itemType = Assembly.GetCallingAssembly()
                .GetTypes().FirstOrDefault(t => t.Name == type);

            var itemObject = Activator.CreateInstance(itemType);

            return (IItem) itemObject;
        }
    }
}