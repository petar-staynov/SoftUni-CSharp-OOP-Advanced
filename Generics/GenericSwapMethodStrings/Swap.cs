using System;
using System.Collections.Generic;

namespace GenericSwapMethodStrings
{
    public class Swap<T>
    {
        public Swap(List<T> items)
        {
            this.Items = items;
        }

        public List<T> Items { get; set; }

        public void SwapItems(int index1, int index2)
        {
            T tempItem = Items[index1];
            Items[index1] = Items[index2];
            Items[index2] = tempItem;

            foreach (T item in Items)
            {
                Console.WriteLine($"{item.GetType().FullName}: {item.ToString()}");
            }
        }
    }
}