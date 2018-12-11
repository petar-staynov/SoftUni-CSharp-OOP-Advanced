using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwapMethodStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> items = new List<int>();
            for (int i = 0; i < n; i++)
            {
                var item = int.Parse(Console.ReadLine());
                items.Add(item);
            }

            var swapItems = new Swap<int>(items);

            int[] indexes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            swapItems.SwapItems(indexes[0], indexes[1]);
        }
    }
}