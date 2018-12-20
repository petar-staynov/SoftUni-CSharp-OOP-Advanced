using System;
using System.Linq;
using System.Reflection;
using Travel.Entities.Airplanes;

namespace Travel.Entities.Factories
{
	using Contracts;
	using Airplanes.Contracts;

	public class AirplaneFactory : IAirplaneFactory
	{
		public IAirplane CreateAirplane(string type)
		{
		    Type airplaneType = Assembly.GetCallingAssembly()
		        .GetTypes().FirstOrDefault(t => t.Name == type);

            


            var airplaneInstance = Activator.CreateInstance(airplaneType);
		    //For creating objects which receive arguments
		    //Activator.CreateInstance(airplaneType, new object[] {arguments});

		    return (IAirplane) airplaneInstance;
		}
	}
}