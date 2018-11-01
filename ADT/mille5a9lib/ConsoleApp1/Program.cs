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
                        //Array and Linked Stacks
                        Console.Write("Would you like to make an Array Stack (0), or a Linked Stack (1)?: \n");
                        ConsoleKeyInfo c = Console.ReadKey();
                        if (c.KeyChar == '0')
                        {
                            // Array Stack Menu
                            Console.Write("\nCreating Array Stack...\n");
                            var arrStack = mille5a9lib.Stack<int>.Create(10); //Array Stack init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Push\n");
                                Console.Write("2. Pop\n");
                                Console.Write("3. Peek\n");
                                Console.Write("4. Size\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to push to the Stack:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = arrStack.Push(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    int output = arrStack.Pop();
                                    Console.Write("\nOut popped a: " + output + "\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\n");
                                    int item = arrStack.Peek();
                                    Console.Write("The item at the top is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\n");
                                    int num = arrStack.Size();
                                    Console.Write("The Stack is of size: " + num + "\n");
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
                            Console.Write("\nCreating linked Stack...\n");
                            var linStack = mille5a9lib.Stack<int>.Create(); //Linked Stack init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Push\n");
                                Console.Write("2. Pop\n");
                                Console.Write("3. Peek\n");
                                Console.Write("4. Size\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to push to the Stack:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = linStack.Push(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    int output = linStack.Pop();
                                    Console.Write("\nOut popped a: " + output + "\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\n");
                                    int item = linStack.Peek();
                                    Console.Write("The item at the top is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\n");
                                    int num = linStack.Size();
                                    Console.Write("The Stack is of size: " + num + "\n");
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
                        break;
                    case 2:
                        //Array and Linked Queues
                        Console.Write("Would you like to make an Array Queue (0), or a Linked Queue (1)?: \n");
                        c = Console.ReadKey();
                        if (c.KeyChar == '0')
                        {
                            // Array Queue Menu
                            Console.Write("\nCreating Array Queue...\n");
                            var arrqueue = mille5a9lib.Queue<int>.Create(10); //Array Queue init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Enqueue\n");
                                Console.Write("2. Dequeue\n");
                                Console.Write("3. Peek\n");
                                Console.Write("4. Size\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to put in the queue:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = arrqueue.Enqueue(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    int output = arrqueue.Dequeue();
                                    Console.Write("\nYou have dequeued a " + output + "\n");
                                }
                                else if (x.KeyChar == '3')
                                { 
                                    int item = arrqueue.Peek();
                                    Console.Write("\nThe item in question is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    int num = arrqueue.Size();
                                    Console.Write("\nThe queue is of size: " + num + "\n");
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
                            // Linked Queue Menu
                            Console.Write("\nCreating Linked Queue...(dequeue is broken)\n");
                            var linqueue = mille5a9lib.Queue<int>.Create(); //Linked Queue init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Enqueue\n");
                                Console.Write("2. Dequeue\n");
                                Console.Write("3. Peek\n");
                                Console.Write("4. Size\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to put in the queue:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = linqueue.Enqueue(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    int output = linqueue.Dequeue();
                                    Console.Write("\nYou have dequeued a " + output + "\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    int item = linqueue.Peek();
                                    Console.Write("\nThe item in question is: " + item + "\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    int num = linqueue.Size();
                                    Console.Write("\nThe queue is of size: " + num + "\n");
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
                        break;
                    case 3:
                        //Array and Linked Lists
                        Console.Write("Would you like to make an Array List (0), or a Linked List (1)?: \n");
                        c = Console.ReadKey();
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
                        //Heap
                        break;
                    case 7:
                        //Graph
                        break;
                    case 8:
                        //Hash table
                        break;
                }
            }
        }
    }
}
