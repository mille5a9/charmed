using System;

namespace MATH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MATH, type your math:");
            while (true)
            {
                string mafs = Console.ReadLine();
                Expression x = new Expression(mafs);
                Variable y = x.Solve();
                Console.WriteLine("Solved! The equation comes out as: " + y.Name + ": " + y.Value);
            }
        }
    }
}
