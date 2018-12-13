﻿using System.Linq;
using System.Reflection;

namespace _03BarracksFactory.Core.Factories
{
    using System;
    using Contracts;

    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitType)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly.GetTypes().First(t => t.Name == unitType);

            IUnit typeInstance = (IUnit)Activator.CreateInstance(type, true);

            return typeInstance;
        }
    }
}
