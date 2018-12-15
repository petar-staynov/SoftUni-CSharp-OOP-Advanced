using System;
using LabDemo.Models.Contracts;

namespace LabDemo.Models
{
    public class ConsoleReader : IReader
    {
        public string Read()
        {
            string input = Console.ReadLine();
            return input;
        }
    }
}