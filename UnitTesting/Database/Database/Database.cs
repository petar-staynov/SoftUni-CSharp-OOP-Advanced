using System;
using System.Linq;

namespace Database
{
    public class Database
    {
        private const int Capacity = 16;
        private int[] data;
        private int lastIndex;

        public Database()
        {
            this.data = new int[Capacity];
            LastIndex = 0;
        }

        public Database(int[] values) : this()
        {
            Data = values;
        }

        public int[] Data
        {
            get => this.data;
            set
            {
                if (value.Length > Capacity)
                {
                    throw new InvalidOperationException("Invalid number of elements");
                }

                for (int i = 0; i < value.Length; i++)
                {
                    Add(value[i]);
                }
            }
        }

        public int LastIndex
        {
            get => lastIndex;
            private set => lastIndex = value;
        }

        public void Add(int value)
        {
            if (LastIndex >= Capacity)
            {
                throw new InvalidOperationException("Database full");
            }

            Data[LastIndex] = value;
            LastIndex++;
        }

        public void Remove()
        {
            if (Data.Length <= 0 || LastIndex <= 0)
            {
                throw new InvalidOperationException("Database is empty");
            }

            Data[LastIndex] = 0;
            LastIndex--;
        }

        public int[] Fetch()
        {
            return Data.Take(LastIndex).ToArray();
        }
    }
}