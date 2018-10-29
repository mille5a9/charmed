using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome to the mille5a9lib tester!\n");
            bool exit = false;
            while (!exit)
            {
                Console.Write("Please choose a class to test:\n");
                Console.Write("1. Stack\n");
                Console.Write("2. Queue\n");
                Console.Write("3. List\n");
                Console.Write("4. Binary Search Tree\n");
                Console.Write("5. AVL Tree\n");
                Console.Write("6. Hash Table\n");
                Console.Write("7. Graph\n");
                Console.Write("8. Heap\n");
                Console.Write("0. Exit\n");
                int choice = Console.Read() - 48; //ascii 0 is 48

                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;

                    case 1:
                        var arrstackimp = mille5a9lib.Stack<int>.Create(100); //Array Stack init

                        //Now do stuff

                        var linstackimp = mille5a9lib.Stack<string>.Create(); //Linked Stack init

                        //Now do stuff

                        break;
                }
            }
        }
    }
}
