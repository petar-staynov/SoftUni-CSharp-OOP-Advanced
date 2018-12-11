using System;
using System.Linq;

namespace Tuple
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] args1 = Console.ReadLine().Split(' ');
            string fullName = $"{args1[0]} {args1[1]}";
            string kvartal = args1[2];
            var customTuple1 = new CustomTuple<string, string>(fullName, kvartal);
            Console.WriteLine(customTuple1.ToString());

            string[] args2 = Console.ReadLine().Split(' ');
            string name = args2[0];
            int liters = int.Parse(args2[1]);
            var customTuple2 = new CustomTuple<string, int>(name, liters);
            Console.WriteLine(customTuple2.ToString());

            string[] args3 = Console.ReadLine().Split(' ');
            int num1 = int.Parse(args3[0]);
            double num2 = double.Parse(args3[1]);
            var customTuple3 = new CustomTuple<int, double>(num1, num2);
            Console.WriteLine(customTuple3.ToString());
        }
    }

    public class CustomTuple<T1, T2>
    {
        private T1 item1;
        private T2 item2;

        public CustomTuple(T1 item1, T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public override string ToString()
        {
            return $"{item1.ToString()} -> {item2.ToString()}";
        }
    }
}