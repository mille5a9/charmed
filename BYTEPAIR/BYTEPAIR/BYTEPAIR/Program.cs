﻿using System;
using System.Collections.Generic;

namespace BYTEPAIR
{

    public class BytePair
    {
        public BytePair(int quantity = 5000)
        {
            Random x = new Random();
            byte[] bytes = new byte[quantity];
            for (int i = 0; i < quantity; i++)
            {
                bytes[i] = (byte)x.Next(16);
            }
            _bytes = bytes;
        }

        public void Compress()
        {
            List<Tuple<byte, byte>> bitsets = new List<Tuple<byte, byte>>();
            List<Tuple<byte, byte>> uniquebitsets = new List<Tuple<byte, byte>>();
            List<int> occurences = new List<int>();
            int maxindex = 0;
            do
            {
                bitsets.Clear();
                occurences.Clear();
                uniquebitsets.Clear();
                for (int i = 0; i < _bytes.Length - 1; i += 2)
                {
                    Tuple<byte, byte> here = new Tuple<byte, byte>(_bytes[i], _bytes[i + 1]);
                    bitsets.Add(here);
                    if (uniquebitsets.Contains(here) == false)
                    {
                        uniquebitsets.Add(here);
                        occurences.Add(1);
                    }
                    else
                    {
                        occurences[uniquebitsets.IndexOf(here)]++;
                    }
                }

                for (int x = 0; x < occurences.Count; x++) { if (occurences[x] > occurences[maxindex]) maxindex = x; }
                //max bitset is now uniquebitsets[maxindex] and it appears occurences[maxindex] number of times

                int freebyte = 0;
                bool stay = true;
                //for (; stay; freebyte++)
                //{
                //    for (int i = 0; i < _bytes.Length; i++)
                //    {
                //        if (freebyte == _bytes[i]) break;
                //        else if (i == _bytes.Length - 1) return; //if all bytes are used
                //        stay = false;
                //    }
                //}
                List<byte> bytelist = new List<byte>(_bytes);
                while (bytelist.Contains((byte)freebyte))
                {
                    freebyte++;
                    if (freebyte == 256) return;
                }
                byte convertbyte = (byte)freebyte;
                //now freebyte is the int value of a sequence of 8 bits that does not appear in _bits
                byte first = uniquebitsets[maxindex].Item1;
                byte second = uniquebitsets[maxindex].Item2;
                _key.Push(new Tuple<byte, byte, byte>(convertbyte, first, second));


                byte[] old_bytes = _bytes;
                List<byte> new_bytes = new List<byte>();
                for (int i = 0; i < old_bytes.Length - 1; i += 2)
                {
                    Tuple<byte, byte> here = new Tuple<byte, byte>(_bytes[i], _bytes[i + 1]);
                    if (here.Item1 == uniquebitsets[maxindex].Item1 && here.Item2 == uniquebitsets[maxindex].Item2)
                    {
                        new_bytes.Add(convertbyte);
                    }
                    else
                    {
                        new_bytes.Add(here.Item1);
                        new_bytes.Add(here.Item2);
                    }
                }
                //byte[] newbyteparser = new byte[new_bytes.Count];
                //for (int i = 0; i < new_bytes.Count; i++) { newbyteparser[i] = new_bytes[i]; }
                _bytes = new byte[new_bytes.Count];
                _bytes = new_bytes.ToArray();
                Console.Write(".");

                } while (occurences[maxindex] > 8);
        }

        public void Depress()
        {
            while (_key.Count > 0)
            {
                Tuple<byte, byte, byte> here = _key.Pop();
                List<byte> bytelist = new List<byte>();
                for (int i = 0; i < _bytes.Length; i++)
                {
                    if (_bytes[i] == here.Item1)
                    {
                        bytelist.Add(here.Item2);
                        bytelist.Add(here.Item3);
                    }
                    else
                    {
                        bytelist.Add(_bytes[i]);
                    }
                }
                _bytes = bytelist.ToArray();
            }
        }

        public byte[] GetBytes() { return _bytes; }

        private byte[] _bytes;
        private Stack<Tuple<byte, byte, byte>> _key = new Stack<Tuple<byte, byte, byte>>(); //holds the replacement byte and the two bytes it represents
    }

    class Program
    {
        static void Main(string[] args)
        {
            BytePair x = new BytePair();
            int initialsize = x.GetBytes().Length;
            x.Compress();
            float compercent = (1 - x.GetBytes().Length / (float)initialsize) * 100;
            Console.WriteLine("\nCompression Complete!\n");
            Console.WriteLine("The data is now of length " + x.GetBytes().Length + "!");
            Console.WriteLine("I mean... it WAS a whopping " + initialsize + "...");
            Console.WriteLine("It was compressed by " + compercent + "%!");
            Console.WriteLine("Now Depressing the data back to original size...");
            x.Depress();
            Console.WriteLine("The data has returned to " + x.GetBytes().Length + " bytes");
            Console.ReadLine();
        }
    }
}
