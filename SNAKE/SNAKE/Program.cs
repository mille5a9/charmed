using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace SNAKE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Wait to begin
            Console.WriteLine("Welcome to Snake! Press Enter to Start.");
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Enter)
                { break; }
            }

            int speed = 10;
            Queue<Tuple<int, int>> snakespots = new Queue<Tuple<int, int>>();
            snakespots.Enqueue(new Tuple<int, int>(5, 3));
            Tuple<int, int> snakespotsback = new Tuple<int, int>(5, 3);
            int direction = 1; //1 is east, 2 is north, 3 is west, 4 is south
            char[,] board = new char[90, 90];
            bool needsnewreward = true;


            //Start Game
            while (true)
            {
                direction = CheckControls(direction);

                foreach (Tuple<int, int>z in snakespots)
                {
                    board[z.Item1, z.Item2] = 'S';
                }

                if (needsnewreward)
                {
                    while (true)
                    {
                        int rewardx = RandomNumber(1, 89);
                        int rewardy = RandomNumber(1, 29);
                        if (snakespots.Contains(new Tuple<int, int>(rewardx, rewardy))) continue;
                        else
                        {
                            board[rewardx, rewardy] = '*';
                            break;
                        }
                    }
                }
                needsnewreward = true;

                Console.Clear();
                //construct graphics
                Console.WriteLine("------------------------------------------------------------------------------------------");
                for (int y = 1; y < 29; y++)
                {
                    string nextline = "|";
                    for (int x = 1; x < 89; x++)
                    {
                        nextline += board[x, y].ToString();
                    }
                    nextline += "|";
                    Console.WriteLine(nextline);
                }
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Thread.Sleep(speed);
                
                //if (Console.KeyAvailable)
                //{
                //    ConsoleKeyInfo arrow = Console.ReadKey(true);
                //    switch (arrow.Key)
                //    {
                //        case ConsoleKey.RightArrow:
                //            direction = 1;
                //            break;
                //        case ConsoleKey.UpArrow:
                //            direction = 2;
                //            break;
                //        case ConsoleKey.LeftArrow:
                //            direction = 3;
                //            break;
                //        case ConsoleKey.DownArrow:
                //            direction = 4;
                //            break;
                //    }
                //}

                switch (direction)
                {
                    case 1:
                        snakespotsback = new Tuple<int, int>(snakespotsback.Item1 + 1, snakespotsback.Item2);
                        if (snakespotsback.Item1 < 1 || snakespotsback.Item1 > 89 || snakespotsback.Item2 < 1 || snakespotsback.Item2 > 29) Environment.Exit(0);
                        if (snakespots.Contains(snakespotsback)) Environment.Exit(0);
                        snakespots.Enqueue(snakespotsback);
                        if (board[snakespotsback.Item1, snakespotsback.Item2] != '*')
                        {
                            Tuple<int, int> oldspot = snakespots.Dequeue();
                            board[oldspot.Item1, oldspot.Item2] = ' ';
                            needsnewreward = false;
                        }
                        else needsnewreward = true;
                        break;
                    case 2:
                        snakespotsback = new Tuple<int, int>(snakespotsback.Item1, snakespotsback.Item2 - 1);
                        if (snakespotsback.Item1 < 1 || snakespotsback.Item1 > 89 || snakespotsback.Item2 < 1 || snakespotsback.Item2 > 29) Environment.Exit(0);
                        if (snakespots.Contains(snakespotsback)) Environment.Exit(0);
                        snakespots.Enqueue(snakespotsback);
                        if (board[snakespotsback.Item1, snakespotsback.Item2] != '*')
                        {
                            Tuple<int, int> oldspot = snakespots.Dequeue();
                            board[oldspot.Item1, oldspot.Item2] = ' ';
                            needsnewreward = false;
                        }
                        else needsnewreward = true;
                        break;
                    case 3:
                        snakespotsback = new Tuple<int, int>(snakespotsback.Item1 - 1, snakespotsback.Item2);
                        if (snakespotsback.Item1 < 1 || snakespotsback.Item1 > 89 || snakespotsback.Item2 < 1 || snakespotsback.Item2 > 29) Environment.Exit(0);
                        if (snakespots.Contains(snakespotsback)) Environment.Exit(0);
                        snakespots.Enqueue(snakespotsback);
                        if (board[snakespotsback.Item1, snakespotsback.Item2] != '*')
                        {
                            Tuple<int, int> oldspot = snakespots.Dequeue();
                            board[oldspot.Item1, oldspot.Item2] = ' ';
                            needsnewreward = false;
                        }
                        else needsnewreward = true;
                        break;
                    case 4:
                        snakespotsback = new Tuple<int, int>(snakespotsback.Item1, snakespotsback.Item2 + 1);
                        if (snakespotsback.Item1 < 1 || snakespotsback.Item1 > 89 || snakespotsback.Item2 < 1 || snakespotsback.Item2 > 29) Environment.Exit(0);
                        if (snakespots.Contains(snakespotsback)) Environment.Exit(0);
                        snakespots.Enqueue(snakespotsback);
                        if (board[snakespotsback.Item1, snakespotsback.Item2] != '*')
                        {
                            Tuple<int, int> oldspot = snakespots.Dequeue();
                            board[oldspot.Item1, oldspot.Item2] = ' ';
                            needsnewreward = false;
                        }
                        else needsnewreward = true;
                        break;
                }

                if (snakespotsback.Item1 < 1 || snakespotsback.Item1 > 89 || snakespotsback.Item2 < 1 || snakespotsback.Item2 > 29) break;
            }
        }

        private static int RandomNumber(int min, int max) 
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private static int CheckControls(int normal)
        {
            if (Console.KeyAvailable)
            {
                //while (Console.KeyAvailable)
                //{
                //    Console.ReadKey(false);
                //}
                ConsoleKeyInfo arrow = Console.ReadKey(true);
                switch (arrow.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (normal != 3) return 1;
                        else break;
                    case ConsoleKey.UpArrow:
                        if (normal != 4) return 2;
                        else break;
                    case ConsoleKey.LeftArrow:
                        if (normal != 1) return 3;
                        else break;
                    case ConsoleKey.DownArrow:
                        if (normal != 2) return 4;
                        else break;
                }
            }
            return normal;
        }
    }
}
