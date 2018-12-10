using System;
using System.Collections.Generic;
using System.Linq;

namespace BYTEPAIR
{

    public class BytePair
    {
        public BytePair(int quantity = 1000, int range = 8, int aggression = 8)
        {
            Random x = new Random();
            byte?[] bytes = new byte?[quantity];
            for (int i = 0; i < quantity; i++)
            {
                bytes[i] = (byte)x.Next(range); // range determines the diversity of the bytes: 255 will take infinite time while smaller numbers are more effective
            }
            _bytes = bytes;
            if (aggression < 4) aggression = 4;
            _aggro = aggression; // aggression determines at how small of a gain the compression will cease
                                 // setting aggression below 4 is useless, because of the size of the data that goes in _key
        }

        public void Compress()
        {
            byte?[] bitsets1 = new byte?[_bytes.Length / 2];
            byte?[] bitsets2 = new byte?[_bytes.Length / 2];
            byte?[] uniquebitsets1 = new byte?[_bytes.Length / 2];
            byte?[] uniquebitsets2 = new byte?[_bytes.Length / 2];
            int[] occurences = new int[_bytes.Length / 2];
            int maxindex = 0;
            do
            {
                bitsets1 = new byte?[_bytes.Length / 2];
                bitsets2 = new byte?[_bytes.Length / 2];
                uniquebitsets1 = new byte?[_bytes.Length / 2];
                uniquebitsets2 = new byte?[_bytes.Length / 2];
                occurences = new int[_bytes.Length / 2];
                byte? lastbite = null;
                for (int i = 0; i < _bytes.Length; i += 2)
                {
                    if (i == _bytes.Length - 1)
                    {
                        lastbite = _bytes[i];
                        break;
                    }
                    bitsets1[i / 2] = _bytes[i];
                    bitsets2[i / 2] = _bytes[i + 1];
                    for (int j = 0; j <= (i / 2); j++)
                    {
                        if (uniquebitsets1[j] == null)
                        {
                            uniquebitsets1[j] = _bytes[i];
                            uniquebitsets2[j] = _bytes[i + 1];
                            break;
                        }
                        if (uniquebitsets1[j] == _bytes[i] && uniquebitsets2[j] == _bytes[i + 1])
                        {
                            occurences[j]++;
                            break;
                        }
                    }
                }

                for (int x = 0; x < occurences.Length; x++) { if (occurences[x] > occurences[maxindex]) maxindex = x; }
                //max bitset is now uniquebitsets[maxindex] and it appears occurences[maxindex] number of times

                int freebyte = -1;
                while (freebyte != 256)
                {
                    freebyte++;
                    bool cont = false;
                    for (int i = 0; i < _bytes.Length; i++) if (_bytes[i] == freebyte)
                        {
                            cont = true;
                            break;
                        };
                    if (cont) continue;
                    break;
                }
                if (freebyte == 256) return;
                byte convertbyte = (byte)freebyte;
                //now freebyte is the int value of a sequence of 8 bits that does not appear in _bits
                byte? first = uniquebitsets1[maxindex];
                byte? second = uniquebitsets2[maxindex];
                _key.Push(new Tuple<byte, byte?, byte?>(convertbyte, second, first));


                byte?[] old_bytes = _bytes;
                byte?[] new_bytes = new byte?[old_bytes.Length];
                int lostbytes = 0;
                for (int i = 0; i < old_bytes.Length; i += 2)
                {
                    if (i == old_bytes.Length - 1)
                    {
                        new_bytes[i - lostbytes] = lastbite;
                        break;
                    }
                    if (old_bytes[i] == uniquebitsets1[maxindex] && old_bytes[i + 1] == uniquebitsets2[maxindex])
                    {
                        new_bytes[i - lostbytes] = convertbyte;
                        lostbytes++;
                    }
                    else
                    {
                        new_bytes[i - lostbytes] = old_bytes[i];
                        new_bytes[i - lostbytes + 1] = old_bytes[i + 1];
                    }
                }
                //byte[] newbyteparser = new byte[new_bytes.Count];
                //for (int i = 0; i < new_bytes.Count; i++) { newbyteparser[i] = new_bytes[i]; }
                _bytes = new byte?[new_bytes.Length - lostbytes];
                for (int i = 0; i < _bytes.Length; i++) _bytes[i] = new_bytes[i];
                Console.Write(".");

            } while (occurences[maxindex] > _aggro);
        }

        public void Depress()
        {
            while (_key.Count > 0)
            {
                Tuple<byte, byte?, byte?> here = _key.Pop();
                byte?[] bytelist = new byte?[_bytes.Length * 2];
                int x = 0;

                for (int i = 0; i < _bytes.Length + x; i++)
                {
                    if (_bytes[i - x] == here.Item1)
                    {
                        bytelist[i] = here.Item3;
                        bytelist[++i] = here.Item2;
                        x++;
                    }
                    else
                    {
                        bytelist[i] = _bytes[i - x];
                    }
                }
                byte? copyfinalbyte = _bytes[_bytes.Length - 1];
                _bytes = new byte?[_bytes.Length + x];
                if (bytelist[_bytes.Length - 1] == null) bytelist[_bytes.Length - 1] = copyfinalbyte;
                for (int i = 0; i < _bytes.Length; i++) _bytes[i] = bytelist[i];
                Console.Write(".");
            }
        }

        public byte?[] GetBytes() { return _bytes; }
        public Stack<Tuple<byte, byte?, byte?>> GetStack() { return _key; }

        private byte?[] _bytes;
        private readonly int _aggro;
        private Stack<Tuple<byte, byte?, byte?>> _key = new Stack<Tuple<byte, byte?, byte?>>(); //holds the replacement byte and the two bytes it represents
    }

    class Program
    {
        static void Main(string[] args)
        {
            BytePair x = new BytePair();
            int initialsize = x.GetBytes().Length;
            byte?[] initialbytes = x.GetBytes();
            x.Compress();
            float compercent = (1 - x.GetBytes().Length / (float)initialsize) * 100;
            Console.WriteLine("\nCompression Complete!\n");
            Console.WriteLine("The data is now of length " + x.GetBytes().Length + "!");
            byte?[] compressed = x.GetBytes();
            Stack<Tuple<byte, byte?, byte?>> stack = x.GetStack();
            int stacksize = stack.Count;
            Console.WriteLine("I mean... it WAS a whopping " + initialsize + "...");
            Console.WriteLine("It was compressed by " + compercent + "%!");
            Console.WriteLine("Now Depressing the data back to original size...");
            x.Depress();
            Console.WriteLine("\nThe data has returned to " + x.GetBytes().Length + " bytes");
            byte?[] depressed = x.GetBytes();
            bool match = true;
            for (int i = 0; i < initialsize; i++) if (initialbytes[i] != depressed[i]) match = false;
            if (match)
            {
                Console.WriteLine("The datasets match, too! What an accomplishment.");
            }
            else
            {
                Console.WriteLine("Err... I guess they don't match?");
            }
            Console.WriteLine("And for the record, the stack of keys was " + (stacksize * 3) + " bytes large... so add that in");
            Console.ReadLine();
        }
    }
}
