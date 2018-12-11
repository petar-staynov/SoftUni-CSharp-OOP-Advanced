using System;

namespace GenericBox
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var message = int.Parse(Console.ReadLine());
                Box<int> box = new Box<int>(message);
                Console.WriteLine(box);
            }
        }
    }
}