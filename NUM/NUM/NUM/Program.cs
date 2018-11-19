using System;
using System.Threading.Tasks;

namespace NUM
{
    public class NLP //NLP for Natural Language Processor
    {
        private static readonly string[] _baseNumbers =
        {
            "one", "two", "three", "four",
            "five", "six", "seven", "eight",
            "nine", "ten", "eleven", "twelve"
        };
        private static readonly string[] _prefixNumbers =
        {
            "twen", "thir", "for", "fif",
            "six", "seven", "eigh", "nine"
        };
        private static readonly string[] _powerNumbers =
        {
            "hundred", "thousand", "million", "billion"
        };
        public static string NumberToWord(int input)
        {
            if (input == 0) return "zero";
            string inputstring = input.ToString();
            string output = "";
            bool teenflag = false;
            for (int i = 0; i < inputstring.Length; i++)
            {
                int comparison = inputstring.Length - i;
                if (i != 0)
                {
                    if (comparison == 3 && inputstring.Substring(i - 3, 3) != "000") output += " thousand ";
                    else if (comparison == 6) output += " million ";
                    else if (comparison == 9) output += " billion ";
                }
                int number = inputstring[i] - 48;
                switch ((inputstring.Length - i) % 3)
                {
                    case 0:
                        if (number > 0) output += _baseNumbers[number - 1] + " hundred ";
                        break;

                    case 2:
                        if (number > 1)
                        {
                            output += _prefixNumbers[number - 2] + "ty-";
                        }
                        else if (number == 1) teenflag = true;
                        break;

                    case 1:
                        //ten or higher
                        if (teenflag)
                        {
                            if (number > 2) output += _prefixNumbers[number - 2] + "teen";
                            else output += _baseNumbers[number + 9];
                            teenflag = false;
                        }
                        else if (number > 0)//nine or lower
                        {
                            output += _baseNumbers[number - 1];
                        }
                        break;
                }
            }
            if (output[output.Length - 1] == '-') output = output.Substring(0, output.Length - 1);
            return output;
        }
        public static async Task<int> StringToNumber(string input)
        {
            int output = 0;
            string[] splitted;
            string currentsplit = input;
            if (input.Contains("billion"))
            {
                splitted = input.Split(" billion ");
                output += await SplittedSTN(splitted[0], (splitted.Length == 2)) * 1000000000;
                if (splitted.Length == 2) currentsplit = splitted[1];
                else currentsplit = "";
            }
            if (input.Contains("million"))
            {
                splitted = currentsplit.Split(" million ");
                output += await SplittedSTN(splitted[0], (splitted.Length == 2)) * 1000000;
                if (splitted.Length == 2) currentsplit = splitted[1];
                else currentsplit = "";
            }
            if (input.Contains("thousand"))
            {
                splitted = currentsplit.Split(" thousand ");
                output += await SplittedSTN(splitted[0], (splitted.Length == 2)) * 1000;
                if (splitted.Length == 2) currentsplit = splitted[1];
                else currentsplit = "";
            }
            output += await SplittedSTN(currentsplit, currentsplit != "");
            return output;
        }

        private static async Task<int> SplittedSTN(string input, bool valid)
        {
            string edgecase = input + " ";
            if (valid)
            {
                int[] output = { 0 };
                output[0] = await new Task<int>(() => ForLoopTask(0, input, edgecase));
                output[1] = await new Task<int>(() => ForLoopTask(1, input, edgecase));
                output[2] = await new Task<int>(() => ForLoopTask(2, input, edgecase));
                output[3] = await new Task<int>(() => ForLoopTask(3, input, edgecase));
                output[4] = await new Task<int>(() => ForLoopTask(4, input, edgecase));
                foreach (int o in output) if (o != -1) return o;
            }
            return 0;
        }

        private static int ForLoopTask(int index, string input, string edgecase)
        {
            for (int i = index * 200; i < (index + 1) * 200; i++)
            {
                string check = NumberToWord(i);
                if ((check == input) || (check == edgecase)) return i;
            }
            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the NUM natural language processor!\n");
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Please enter 0 to convert a number to a string, or 1 for the opposite: ");
                ConsoleKeyInfo x = Console.ReadKey();
                switch (x.KeyChar)
                {
                    case '0':
                        Console.WriteLine("\nPlease enter your number as an integer: ");
                        string input = Console.ReadLine();
                        bool zoop = Int32.TryParse(input, out int num);
                        string output = NLP.NumberToWord(num);
                        Console.WriteLine("You typed:\n" + output + "\n");
                        break;
                    case '1':
                        Console.WriteLine("\nPlease enter your number as words: ");
                        input = Console.ReadLine();
                        num = NLP.StringToNumber(input).Result;
                        Console.WriteLine("You typed:\n" + num + "\n");
                        break;
                    case '2':
                        loop = false;
                        break;
                }
            }
            for (int i = 0; i < 1000001; i++)
            {
                Console.WriteLine(NLP.NumberToWord(i) + "\n");
            }
            Console.ReadLine();
        }
    }
}
