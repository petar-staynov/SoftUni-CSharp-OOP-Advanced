using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomList
{
    public class CustomList<T> : ICustomList<T> where T : IComparable
    {
        private List<T> items;

        public CustomList()
        {
            this.items = new List<T>();
        }

        public CustomList(List<T> inputList)
        {
            this.items = inputList;
        }

        public void Add(T element)
        {
            this.items.Add(element);
        }

        public bool Contains(T element)
        {
            return this.items.Contains(element);
        }

        public int CountGreaterThan(T element)
        {
            int count = 0;
            foreach (var item in this.items)
            {
                if (item.CompareTo(element) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public T Max()
        {
            return items.Max();
        }

        public T Min()
        {
            return items.Min();
        }

        public T Remove(int index)
        {
            T removedItem = items.ElementAt(index);
            items.RemoveAt(index);
            return removedItem;
        }

        public void Swap(int index1, int index2)
        {
            T tempItem = items[index1];
            this.items[index1] = this.items[index2];
            this.items[index2] = tempItem;
        }

        public void Sort()
        {
            this.items.Sort();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}