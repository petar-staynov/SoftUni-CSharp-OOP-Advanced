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
            string darjava = args1[3];

            var customTuple1 = new CustomTuple<string, string, string>(fullName, kvartal, darjava);
            Console.WriteLine(customTuple1.ToString());

            string[] args2 = Console.ReadLine().Split(' ');
            string name = args2[0];
            int liters = int.Parse(args2[1]);
            bool status = args2[2] == "drunk" ? true : false;
            var customTuple2 = new CustomTuple<string, int, bool>(name, liters, status);
            Console.WriteLine(customTuple2.ToString());

            string[] args3 = Console.ReadLine().Split(' ');
            string num1 = args3[0];
            double num2 = double.Parse(args3[1]);
            string num3 = args3[2];
            var customTuple3 = new CustomTuple<string, double, string>(num1, num2, num3);
            Console.WriteLine(customTuple3.ToString());
        }
    }

    public class CustomTuple<T1, T2, T3>
    {
        private T1 item1;
        private T2 item2;
        private T3 item3;

        public CustomTuple(T1 item1, T2 item2, T3 item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }

        public override string ToString()
        {
            return $"{item1.ToString()} -> {item2.ToString()} -> {item3.ToString()}";
        }
    }
}