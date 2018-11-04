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
                Console.Write("4. Tree\n");
                Console.Write("5. Heap\n");
                Console.Write("6. Graph\n");
                Console.Write("7. Hash Table\n");
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
                        Console.Write("Would you like to make a Binary Search Tree (0), or an AVL Tree (1)?: \n");
                        c = Console.ReadKey();
                        if (c.KeyChar == '0')
                        {
                            // BST Menu
                            Console.Write("\nCreating Binary Search Tree...\n");
                            var bst = mille5a9lib.Tree<int>.Create(false); //BST init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("Tree contents are: ");
                                var contents = mille5a9lib.List<int>.Create();
                                contents = bst.GetPreorder();
                                Console.Write("\nPreorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = bst.GetInorder();
                                Console.Write("\nInorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = bst.GetPostorder();
                                Console.Write("\nPostorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Remove\n");
                                Console.Write("3. Contains\n");
                                Console.Write("4. Clear Tree\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to add to the tree:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = bst.Insert(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nPlease type a number to remove from the tree:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int num = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = bst.Remove(num);
                                    if (success == false) Console.Write("Removal failed: Number is not in the tree.\n");
                                    else Console.Write("Removal success!\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a position to observe in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = bst.Contains(pos);
                                    if (success) Console.Write("The item in question is definitely there.\n");
                                    else Console.Write("The item in question does not exist in the tree.\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nClearing list...\n");
                                    bst.Clear(bst.GetRoot());
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
                            // AVLT Menu
                            Console.Write("\nCreating Binary Search Tree...\n");
                            var avl = mille5a9lib.Tree<int>.Create(true); //AVLT init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("Tree contents are: ");
                                var contents = mille5a9lib.List<int>.Create();
                                contents = avl.GetPreorder();
                                Console.Write("\nPreorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = avl.GetInorder();
                                Console.Write("\nInorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = avl.GetPostorder();
                                Console.Write("\nPostorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Remove\n");
                                Console.Write("3. Contains\n");
                                Console.Write("4. Clear Tree\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to add to the tree:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = avl.Insert(num);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nPlease type a number to remove from the tree:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int num = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = avl.Remove(num);
                                    if (success == false) Console.Write("Removal failed: Number is not in the tree.\n");
                                    else Console.Write("Removal success!\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a position to observe in the list:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = avl.Contains(pos);
                                    if (success) Console.Write("The item in question is definitely there.\n");
                                    else Console.Write("The item in question does not exist in the tree.\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nClearing list...\n");
                                    avl.Clear(avl.GetRoot());
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
                        break;
                    case 5:
                        //Heap
                        Console.Write("Would you like to make a Min Heap (0), or a Max Heap (1)?: \n");
                        c = Console.ReadKey();
                        if (c.KeyChar == '0')
                        {
                            // Minheap Menu
                            Console.Write("\nCreating Min Heap...\n");
                            mille5a9lib.MinHeap<int> min = new mille5a9lib.MinHeap<int>(); //Minheap init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("Tree contents are: ");
                                var contents = mille5a9lib.List<int>.Create();
                                contents = min.GetPreorder();
                                Console.Write("\nPreorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = min.GetInorder();
                                Console.Write("\nInorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = min.GetPostorder();
                                Console.Write("\nPostorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Extract\n");
                                Console.Write("3. Contains\n");
                                Console.Write("4. Clear Minheap\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to add to the heap:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = min.Insert(num, min.GetRoot(), null);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nRemoving the minimum value from the heap:\n");
                                    int success = min.Extract(min.GetRoot(), null);
                                    Console.Write("You found a " + success + "\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a number to check for:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = min.Contains(pos, min.GetRoot());
                                    if (success) Console.Write("The item in question is definitely there.\n");
                                    else Console.Write("The item in question does not exist in the tree.\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nClearing heap...\n");
                                    min.Clear(min.GetRoot());
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
                            // Maxheap Menu
                            Console.Write("\nCreating Max Heap...\n");
                            mille5a9lib.MaxHeap<int> max = new mille5a9lib.MaxHeap<int>(); //Maxheap init
                            bool innerexit = false;
                            while (!innerexit)
                            {
                                Console.Write("Tree contents are: ");
                                var contents = mille5a9lib.List<int>.Create();
                                contents = max.GetPreorder();
                                Console.Write("\nPreorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = max.GetInorder();
                                Console.Write("\nInorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                contents = max.GetPostorder();
                                Console.Write("\nPostorder: ");
                                for (int i = 0; i < contents.Size(); i++) Console.Write(contents.GetItem(i) + " ");
                                Console.Write("\nOptions:\n");
                                Console.Write("1. Insert\n");
                                Console.Write("2. Extract\n");
                                Console.Write("3. Contains\n");
                                Console.Write("4. Clear Minheap\n");
                                Console.Write("0. Exit to Main Menu\n");
                                ConsoleKeyInfo x = Console.ReadKey();
                                if (x.KeyChar == '1')
                                {
                                    Console.Write("\nPlease type a number to add to the heap:\n");
                                    ConsoleKeyInfo input = Console.ReadKey();
                                    Console.Write("\n");
                                    int num = input.KeyChar - 48;
                                    bool success = max.Insert(num, max.GetRoot(), null);
                                }
                                else if (x.KeyChar == '2')
                                {
                                    Console.Write("\nRemoving the maximum value from the heap:\n");
                                    int success = max.Extract(max.GetRoot(), null);
                                    Console.Write("You found a " + success + "\n");
                                }
                                else if (x.KeyChar == '3')
                                {
                                    Console.Write("\nPlease type a number to check for:\n");
                                    ConsoleKeyInfo poskey = Console.ReadKey();
                                    int pos = poskey.KeyChar - 48;
                                    Console.Write("\n");
                                    bool success = max.Contains(pos, max.GetRoot());
                                    if (success) Console.Write("The item in question is definitely there.\n");
                                    else Console.Write("The item in question does not exist in the tree.\n");
                                }
                                else if (x.KeyChar == '4')
                                {
                                    Console.Write("\nClearing heap...\n");
                                    max.Clear(max.GetRoot());
                                }
                                else if (x.KeyChar == '0')
                                {
                                    innerexit = true;
                                    Console.Write("\n");
                                }
                            }
                        }
                        break;
                    case 6:
                        //Graph
                        break;
                    case 7:
                        //Hash table
                        break;
                }
            }
        }
    }
}
