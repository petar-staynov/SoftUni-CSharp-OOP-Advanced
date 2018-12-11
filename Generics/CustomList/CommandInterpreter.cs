using System;

namespace CustomList
{
    public class CommandInterpreter
    {
        private CustomList<string> customList;

        public CommandInterpreter()
        {
            this.customList = new CustomList<string>();
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                ReadCommand(commandLine);
            }
        }

        protected void ReadCommand(string commandLine)
        {
            string[] commandArgs = commandLine.Split(" ");
            switch (commandArgs[0].ToLower())
            {
                case "add":
                    this.customList.Add(commandArgs[1]);
                    break;
                case "remove":
                    this.customList.Remove(int.Parse(commandArgs[1]));
                    break;
                case "contains":
                    Console.WriteLine(this.customList.Contains(commandArgs[1]));
                    break;
                case "swap":
                    this.customList.Swap(int.Parse(commandArgs[1]), int.Parse(commandArgs[2]));
                    break;
                case "greater":
                    Console.WriteLine(this.customList.CountGreaterThan(commandArgs[1]));
                    break;
                case "max":
                    Console.WriteLine(this.customList.Max());
                    break;
                case "min":
                    Console.WriteLine(this.customList.Min());
                    break;
                case "print":
                    Console.WriteLine(this.customList.ToString());
                    break;
                case "sort":
                    this.customList.Sort();
                    break;
                case "end":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}