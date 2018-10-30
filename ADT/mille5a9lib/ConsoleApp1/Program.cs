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
                ConsoleKeyInfo choicekey = Console.ReadKey();
                int choice = choicekey.KeyChar - 48; //ascii 0 is 48
                Console.Write("\n");
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
                        //var arrqueueimp = mille5a9lib.Queue<int>.Create(100000000); //Array Stack init
                        //arrTimer = new Timer(10);
                        //now = DateTime.Now;
                        //arrTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        //arrTimer.Start();

                        //for (int i = 0; i < 100000000; i++) //100 million items
                        //{
                        //    bool success = arrqueueimp.Enqueue(i);
                        //}

                        //peeked = arrqueueimp.Peek();
                        //Console.Write("Front Item: " + peeked + "\n");
                        //sized = arrqueueimp.Size();
                        //Console.Write("Queue Size: " + sized + "\n");

                        //for (int i = 0; i < 100000000; i++)
                        //{
                        //    int itemout = arrqueueimp.Dequeue();
                        //}

                        //arrTimer.Stop();
                        //arrTimer.Dispose();

                        //var linqueueimp = mille5a9lib.Queue<string>.Create(); //Linked Stack init

                        //linTimer = new Timer(10);
                        //now = DateTime.Now;
                        //linTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, now);
                        //linTimer.Start();

                        //for (int i = 0; i < 10000; i++) //1 million items (for reasonable runtime)
                        //{
                        //    bool success = linqueueimp.Enqueue("item " + i);
                        //    //System.Threading.Thread.Sleep(10);
                        //}

                        //peekedagain = linqueueimp.Peek();
                        //Console.Write("Front Item: " + peekedagain + "\n");
                        //sized = linqueueimp.Size();
                        //Console.Write("Queue Size: " + sized + "\n");

                        //for (int i = 0; i < 10000; i++)
                        //{
                        //    string itemout = linqueueimp.Dequeue();
                        //}

                        //linTimer.Stop();
                        //linTimer.Dispose();
                        break;
                    case 3:
                        //Array and Linked Lists
                        Console.Write("Would you like to make an Array List (0), or a Linked List (1)?: \n");
                        ConsoleKeyInfo c = Console.ReadKey();
                        if (c.KeyChar == '0')
                        {
                            // Array List Menu
                            Console.Write("\nCreating Array List...\n");
                            var arrlist = mille5a9lib.List<int>.Create(10); //Array Stack init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("List contents are: ");
                                for (int i = 0; i < arrlist.Size(); i++) Console.Write(arrlist.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Remove\n");
                                Console.Write("3. Get Item\n");
                                Console.Write("4. Set Item\n");
                                Console.Write("5. Clear List\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a position to insert into the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\nPlease type a number to add to the list:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = arrlist.Insert(pos, num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nPlease type a position to remove from the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = arrlist.Remove(pos);
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a position to observe in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    int item = arrlist.GetItem(pos);
                                    Console.Write("The item in question is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nPlease type a position to change in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\nPlease type a number to place in that spot:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = arrlist.SetItem(pos, num);
                                }
                                else if (x.KeyChar == '5')
                                {
                                    Console.Write("\nClearing list...\n");
                                    arrlist.Clear();
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
                        else if (c.KeyChar == '1')
                        {
                            // Linked List Menu
                            Console.Write("\nCreating Linked List...\n");
                            var linlist = mille5a9lib.List<int>.Create(); //Array Stack init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("List contents are: ");
                                for (int i = 0; i < linlist.Size(); i++) Console.Write(linlist.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Remove\n");
                                Console.Write("3. Get Item\n");
                                Console.Write("4. Set Item\n");
                                Console.Write("5. Clear List\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a position to insert into the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\nPlease type a number to add to the list:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = linlist.Insert(pos, num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nPlease type a position to remove from the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = linlist.Remove(pos);
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a position to observe in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    int item = linlist.GetItem(pos);
                                    Console.Write("The item in question is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nPlease type a position to change in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\nPlease type a number to place in that spot:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = linlist.SetItem(pos, num);
                                }
                                else if (x.KeyChar == '5')
                                {
                                    Console.Write("\nClearing list...\n");
                                    linlist.Clear();
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
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
