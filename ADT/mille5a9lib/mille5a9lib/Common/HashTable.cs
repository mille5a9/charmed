using System;

namespace mille5a9lib
{
    public class HashTable<X> where X : IComparable
    {
        public HashTable(int Z) //Z is the size of the table
        {
            _table = new Tuple<string, X>[Z];
        }
        public int Hash(string key)
        {
            int sum = 0;
            foreach (char x in key) sum += x;
            int hashnum = sum % _table.Length;
            while (_table[hashnum] != null && _table[hashnum].Item2 != null)
            {
                if (_table[hashnum].Item1.CompareTo(key) == 0) return hashnum;
                hashnum++;
                if (hashnum == _table.Length) hashnum = 0;
                else if (hashnum == sum % _table.Length) break;
            }
            return hashnum;
        }
        public bool Add(string key, X value)
        {
            if (_table.Length == _count) return false;
            int hashnum = Hash(key);
            Tuple<string, X> temp = new Tuple<string, X>(key, value);
            _table.SetValue(temp, hashnum);
            _count++;
            return true;
        }
        public X Remove(string key)
        {
            int hashnum = Hash(key);
            if (_table[hashnum].Item1 != key) return default(X);
            X output = _table[hashnum].Item2;
            _table[hashnum] = null;
            _count--;
            return output;
        }
        public X Get(string key)
        {
            int hashnum = Hash(key);
            if (_table[hashnum].Item1 != key) return default(X);
            return _table[hashnum].Item2;
        }
        public int GetLength() { return _count; }
        private int _count = 0;
        private Tuple<string, X>[] _table;
    }
}