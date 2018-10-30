using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        public static void OnTimedEvent(Object source, ElapsedEventArgs e, DateTime now)
        {
            Console.Write("Elapsed time is: {0:HH:mm:ss.fff}\n", (e.SignalTime - now).ToString());
        }

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
                int dummy = Console.Read();
                dummy = Console.Read();
                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;

                    case 1:
                        var arrstackimp = mille5a9lib.Stack<int>.Create(100000000); //Array Stack init
                        Timer arrTimer = new Timer(10);
                        DateTime now = DateTime.Now;
                        arrTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        arrTimer.Start();

                        for (int i = 0; i < 100000000; i++) //100 million items
                        {
                            bool success = arrstackimp.Push(i);
                        }

                        int peeked = arrstackimp.Peek();
                        Console.Write("Top Item: " + peeked + "\n");
                        int sized = arrstackimp.Size();
                        Console.Write("Stack Size: " + sized + "\n");

                        for (int i = 0; i < 100000000; i++)
                        {
                            int itemout = arrstackimp.Pop();
                        }

                        arrTimer.Stop();
                        arrTimer.Dispose();

                        var linstackimp = mille5a9lib.Stack<string>.Create(); //Linked Stack init

                        Timer linTimer = new Timer(10);
                        now = DateTime.Now;
                        linTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        linTimer.Start();

                        for (int i = 0; i < 1000000; i++) //1 million items (for reasonable runtime)
                        {
                            bool success = linstackimp.Push("item " + i);
                            //System.Threading.Thread.Sleep(10);
                        }

                        string peekedagain = linstackimp.Peek();
                        Console.Write("Top Item: " + peekedagain + "\n");
                        sized = linstackimp.Size();
                        Console.Write("Stack Size: " + sized + "\n");

                        for (int i = 0; i < 1000000; i++)
                        {
                            string itemout = linstackimp.Pop();
                        }

                        linTimer.Stop();
                        linTimer.Dispose();
                        break;

                    case 2:
                        //Circular Array and Linked Queues
                        var arrqueueimp = mille5a9lib.Queue<int>.Create(100000000); //Array Stack init
                        arrTimer = new Timer(10);
                        now = DateTime.Now;
                        arrTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        arrTimer.Start();

                        for (int i = 0; i < 100000000; i++) //100 million items
                        {
                            bool success = arrqueueimp.Enqueue(i);
                        }

                        peeked = arrqueueimp.Peek();
                        Console.Write("Front Item: " + peeked + "\n");
                        sized = arrqueueimp.Size();
                        Console.Write("Queue Size: " + sized + "\n");

                        for (int i = 0; i < 100000000; i++)
                        {
                            int itemout = arrqueueimp.Dequeue();
                        }

                        arrTimer.Stop();
                        arrTimer.Dispose();

                        var linqueueimp = mille5a9lib.Queue<string>.Create(); //Linked Stack init

                        linTimer = new Timer(10);
                        now = DateTime.Now;
                        linTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        linTimer.Start();

                        for (int i = 0; i < 10000; i++) //1 million items (for reasonable runtime)
                        {
                            bool success = linqueueimp.Enqueue("item " + i);
                            //System.Threading.Thread.Sleep(10);
                        }

                        peekedagain = linqueueimp.Peek();
                        Console.Write("Front Item: " + peekedagain + "\n");
                        sized = linqueueimp.Size();
                        Console.Write("Queue Size: " + sized + "\n");

                        for (int i = 0; i < 10000; i++)
                        {
                            string itemout = linqueueimp.Dequeue();
                        }

                        linTimer.Stop();
                        linTimer.Dispose();
                        break;
                    case 3:
                        //Array and Linked Lists
                        break;
                    case 4:
                        //Binary Search Tree
                        break;
                    case 5:
                        //AVL Tree
                        break;
                    case 6:
                        //Hash table
                        break;
                    case 7:
                        //Graph
                        break;
                    case 8:
                        //Heap
                        break;
                }
            }
        }
    }
}
