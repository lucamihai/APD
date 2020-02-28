using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace APD.L1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();

            Ex2();
            Console.WriteLine();

            Ex3();
            Console.WriteLine();

            Console.Read();
        }

        private static void Ex1()
        {
            Console.Write("1. Enter an integer: ");
            var integer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"You've entered {integer}");
            
        }

        private static void Ex2()
        {
            Console.Write("2. Enter a float/double: ");
            var number = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Math.Ceiling({number}) = {Math.Ceiling(number)}");
            Console.WriteLine($"Math.Floor({number}) = {Math.Floor(number)}");
            Console.WriteLine($"Math.Round({number}) = {Math.Round(number)}");
        }

        private static void Ex3()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Numbers.txt";

            Console.WriteLine($"3. Reading integers from '{filePath}'");

            var fileContents = File.ReadAllLines(filePath);
            var integers = new List<int>();

            foreach (var line in fileContents)
            {
                foreach (var stringifiedInteger in line.Split(' '))
                {
                    integers.Add(Convert.ToInt32(stringifiedInteger));
                }
            }

            Console.WriteLine($"Average: {integers.Average()}");
        }
    }
}
