using System.Linq;
using System.Reflection;

namespace P02_BlackBoxInteger
{
    using System;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            Type type = typeof(BlackBoxInteger);

            MethodInfo[] methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).ToArray();

            BlackBoxInteger blackBoxInteger = (BlackBoxInteger) Activator.CreateInstance(type, true);
            while (true)
            {
                string[] commandArgs = Console.ReadLine().Split('_');
                string methodName = commandArgs[0];

                if (methodName == "END")
                {
                    break;
                }

                int value = int.Parse(commandArgs[1]);

                MethodInfo classMethod = methods.First(m => m.Name == methodName);

                classMethod.Invoke(blackBoxInteger, new object[] {value});

                FieldInfo methodResult = type
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .First(f => f.Name == "innerValue");
                Console.WriteLine(methodResult.GetValue(blackBoxInteger));
            }
        }
    }
}