using System;
using LabDemo.Core;
using LabDemo.Core.Contracts;
using LabDemo.Models;
using LabDemo.Models.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LabDemo
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();
            IEngine engine = serviceProvider.GetService<IEngine>();
            engine.Run();
            
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IEngine, Engine>();
            serviceCollection.AddTransient<IWriter, ConsoleWriter>();
            serviceCollection.AddTransient<IReader, ConsoleReader>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}