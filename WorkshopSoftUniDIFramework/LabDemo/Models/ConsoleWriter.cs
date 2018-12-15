using System;
using LabDemo.Models.Contracts;

namespace LabDemo.Models
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}