using System;
using System.Collections;
using System.Collections.Generic;

namespace BYTEPAIR
{

    public class BytePair
    {
        public BytePair(int quantity = 2000)
        {
            Random x = new Random();
            byte[] bytes = new byte[quantity];
            for (int i = 0; i < quantity; i++)
            {
                bytes[i] = (byte)x.Next(256);
            }
            _bits = new BitArray(bytes);
        }

        public void Compress()
        {
            List<BitArray> bitsets = new List<BitArray>();
            List<BitArray> uniquebitsets = new List<BitArray>();
            List<int> occurences = new List<int>();
            bool currentbit = false;
            int initialsize = _bits.Count;
            int maxindex = 0;
            do
            {
                bitsets.Clear();
                occurences.Clear();
                uniquebitsets.Clear();
                for (int i = 0; i < _bits.Count - 8; i += 16)
                {
                    bool[] arr = { _bits.Get(i), _bits.Get(i + 1), _bits.Get(i + 2), _bits.Get(i + 3),
                        _bits.Get(i + 4), _bits.Get(i + 5), _bits.Get(i + 6), _bits.Get(i + 7),
                        _bits.Get(i + 8), _bits.Get(i + 9), _bits.Get(i + 10), _bits.Get(i + 11),
                        _bits.Get(i + 12), _bits.Get(i + 13), _bits.Get(i + 14), _bits.Get(i + 15) };
                    BitArray add = new BitArray(arr);
                    bitsets.Add(add);
                    if (uniquebitsets.Contains(add) == false)
                    {
                        uniquebitsets.Add(add);
                        occurences.Add(1);
                    }
                    else
                    {
                        occurences[uniquebitsets.IndexOf(add)]++;
                    }
                }

                for (int x = 0; x < occurences.Count; x++) { if (occurences[x] > occurences[maxindex]) maxindex = x; }
                //max bitset is now uniquebitsets[maxindex] and it appears occurences[maxindex] number of times

                int freebyte = 0;
                bool stay = true;
                for (; stay; freebyte++)
                {
                    for (int i = 0; i < _bits.Count - 8; i += 16)
                    {
                        bool[] arr = { _bits.Get(i), _bits.Get(i + 1), _bits.Get(i + 2), _bits.Get(i + 3),
                        _bits.Get(i + 4), _bits.Get(i + 5), _bits.Get(i + 6), _bits.Get(i + 7) };
                        if (new BitArray(freebyte) == new BitArray(arr)) break;
                        else if (i == _bits.Count - 1) return; //if all bytes are used
                        stay = false;
                    }
                }
                freebyte--; //now freebyte is the int value of a sequence of 8 bits that does not appear in _bits
                byte[] replaced = new byte[2];
                byte convertbyte = (byte)freebyte;
                BitArray convertbit = new BitArray(convertbyte);
                uniquebitsets[maxindex].CopyTo(replaced, 0);
                _key.Push(new Tuple<byte, byte, byte>(convertbyte, replaced[0], replaced[1]));


                List<byte> new_bytes = new List<byte>();
                for (int i = 0; i < _bits.Count - 8; i += 16)
                {
                    bool[] arr1values = new bool[8];
                    for (int j = 0; j < 7; j++) arr1values[j] = _bits.Get(i + j);
                    bool[] arr2values = new bool[8];
                    for (int j = 0; j < 7; j++) arr2values[j] = _bits.Get(i + j + 8);
                    BitArray arr1 = new BitArray(arr1values);
                    BitArray arr2 = new BitArray(arr2values);
                    byte[] totalarr = new byte[2];
                    arr1.CopyTo(totalarr, 0);
                    arr2.CopyTo(totalarr, 1);
                    uniquebitsets[maxindex].CopyTo(replaced, 0);
                    BitArray mayberemove = new BitArray(totalarr);
                    if (mayberemove == uniquebitsets[maxindex])
                    {
                        new_bytes.Add(convertbyte);
                    }
                    else
                    {
                        new_bytes.Add(totalarr[0]);
                        new_bytes.Add(totalarr[1]);
                    }
                }
                _bits = new BitArray(new_bytes.ToArray());
                Console.Write(".");

                } while (occurences[maxindex] > 3);
            float compercent = (1 - (float)initialsize / (_bits.Count) * 100);
            Console.WriteLine("\nCompression Complete!\n");
            Console.WriteLine("The data is now of length " + _bits.Count + "!\n");
            Console.WriteLine("I mean... it WAS a whopping " + initialsize + "...\n");
            Console.WriteLine("It was compressed by " + compercent + "%!\n");
        }

        public void Depress()
        {

        }

        public BitArray GetBits() { return _bits; }

        private BitArray _bits;
        private Stack<Tuple<byte, byte, byte>> _key = new Stack<Tuple<byte, byte, byte>>(); //holds the replacement byte and the two bytes it represents
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BytePair x = new BytePair();
            x.Compress();
            Console.ReadLine();
        }
    }
}
