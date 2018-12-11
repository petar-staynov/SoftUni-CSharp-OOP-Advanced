using System;
using System.Collections.Generic;

namespace GenericCountMethodStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var items = new List<double>();

            for (int i = 0; i < n; i++)
            {
                var item = double.Parse(Console.ReadLine());
                items.Add(item);
            }

            var element = double.Parse(Console.ReadLine());
            Count<double> thing = new Count<double>(items, element);

            Console.WriteLine(thing.ToString());
        }
    }

    public class Count<T> where T : IComparable<T>
    {
        private T item;
        private List<T> items;

        public Count(List<T> items, T item)
        {
            this.items = items;
            this.item = item;
        }

        public int GreaterItems()
        {
            int count = 0;
            foreach (T inputItem in items)
            {
                if (inputItem.CompareTo(item) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public override string ToString()
        {
            return $"{this.GreaterItems()}";
        }
    }
}