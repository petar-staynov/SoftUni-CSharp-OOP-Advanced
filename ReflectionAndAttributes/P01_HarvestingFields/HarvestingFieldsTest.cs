using System.Linq;
using System.Reflection;

namespace P01_HarvestingFields
{
    using System;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            Type type = typeof(HarvestingFields);

            while (true)
            {
                string command = Console.ReadLine();

                FieldInfo[] fields = type
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .ToArray();

                switch (command)
                {
                    case "public":
                        var publicFields = fields.Where(f => f.IsPublic).ToArray();
                        foreach (FieldInfo field in publicFields)
                        {
                            Console.WriteLine(
                                $"{field.Attributes.ToString().ToLower()} {field.FieldType.Name} {field.Name}");
                        }

                        break;

                    case "private":
                        var privateFields = fields.Where(f => f.IsPrivate).ToArray();
                        foreach (FieldInfo field in privateFields)
                        {
                            Console.WriteLine(
                                $"{field.Attributes.ToString().ToLower()} {field.FieldType.Name} {field.Name}");
                        }

                        break;
                    case "protected":
                        var protectedFields = fields.Where(f => f.IsFamily).ToArray();
                        foreach (FieldInfo field in protectedFields)
                        {
                            Console.WriteLine($"protected {field.FieldType.Name} {field.Name}");
                        }

                        break;
                    case "all":
                        foreach (FieldInfo field in fields)
                        {
                            string fieldAttribute = field.Attributes.ToString().ToLower();
                            if (field.Attributes.ToString().ToLower() == "family")
                            {
                                fieldAttribute = "protected";
                            }

                            Console.WriteLine(
                                $"{fieldAttribute} {field.FieldType.Name} {field.Name}");
                        }

                        break;

                    case "HARVEST":
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}