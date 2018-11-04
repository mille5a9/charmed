using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    class InvalidPositionException : Exception
    {
        public InvalidPositionException(int pos, int count)
        {
            _position = pos;
            _count = count;
        }
        public new string Message()
        {
            return "\nThis position is not valid in this list! pos was " + _position + " but _count was " + _count + ".\n";
        }
        private readonly int _position, _count;
    }

    public interface IList<T>
    {
        bool Insert(int pos, T item);
        bool Remove(int pos);
        bool SetItem(int pos, T item);
        T GetItem(int pos);
        int Size();
        void Clear();
    }

    public class List<T>
    {
        public static IList<T> Create()
        {
            return new LinkedList<T>();
        }

        public static IList<T> Create(int maxsize)
        {
            return new ArrayList<T>(maxsize);
        }
    }

    public class ArrayListEnum<T> : IEnumerator<T>
    {
        public ArrayList<T> list;
        private int _pos = -1;

        public ArrayListEnum(ArrayList<T> input) { list = input; }
        public bool MoveNext() { return (_pos++ < list.Size()); }
        public void Reset() { _pos = -1; }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                return list.GetItem(_pos);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ArrayListEnum() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ArrayList<T> : IList<T>, IEnumerable<T>
    {
        public ArrayListEnum<T> GetEnumerator()
        {
            return new ArrayListEnum<T>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        public ArrayList(int maxsize)
        {
            _items = new T[maxsize];
            _maxsize = maxsize;
        }

        public bool Insert(int pos, T item)
        {
            try
            {
                if (pos > _count || pos < 0) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            if (_count == 0)
            {
                _items[0] = item;
                _count++;
                return true;
            }
            else
            {
                for (int i = _count; i >= pos; i--)
                {
                    _items[i] = _items[i - 1];
                }
                _items[pos] = item;
                _count++;
                return true;
            }
        }

        public bool Remove(int pos)
        {
            try
            {
                if (pos >= _count || pos < 0) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            for (int i = pos; i < _count; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
            return true;
        }

        public bool SetItem(int pos, T item)
        {
            try
            {
                if (pos < 0 || pos >= _count) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            _items[pos] = item;
            return true;
        }

        public T GetItem(int pos)
        {
            try
            {
                if (pos < 0 || pos >= _count) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return default(T);
            }
            return _items[pos];
        }

        public int Size() { return _count; }

        public void Clear() { _count = 0; }

        private readonly int _maxsize;
        private T[] _items;
        private int _count = 0;
    }

    public class LinkedListEnum<T> : IEnumerator<T>
    {
        public LinkedList<T> list;
        private int _pos = -1;

        public LinkedListEnum(LinkedList<T> input) { list = input; }
        public bool MoveNext() { return (_pos++ < list.Size()); }
        public void Reset() { _pos = -1; }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                return list.GetItem(_pos);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LinkedListEnum() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class LinkedList<T> : IList<T>, IEnumerable<T>
    {
        public LinkedListEnum<T> GetEnumerator()
        {
            return new LinkedListEnum<T>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Insert(int pos, T item)
        {
            try
            {
                if (pos > _count || pos < 0) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            if (_head == null || pos == _count)
            {
                _head = new DoubleNode<T>(item, _head, null);
                if (_head.GetNext() != null) _head.GetNext().SetPrev(_head);
                _count++;
                return true;
            }
            DoubleNode<T> temp = _head;
            for (int i = 1; i < _count - pos; i++)
            {
                temp = temp.GetNext();
            }
            _count++;
            temp.SetNext(new DoubleNode<T>(item, temp.GetNext(), temp));
            return true;
        }

        public bool Remove(int pos)
        {
            Console.Write("\n" + _count + "\n");
            try
            {
                if (pos > _count || pos < 0) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            if (pos == _count - 1)
            {
                _head = _head.GetNext();
                _count--;
                return true;
            }
            DoubleNode<T> temp = _head;
            for (int i = 1; i < _count - pos; i++) temp = temp.GetNext();
            if (temp.GetNext() != null) temp.GetNext().SetPrev(temp.GetPrev());
            if (temp.GetPrev() != null) temp.GetPrev().SetNext(temp.GetNext());
            _count--;
            return true;
        }

        public bool SetItem(int pos, T item)
        {
            try
            {
                if (pos < 0 || pos >= _count) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return false;
            }
            if (pos == _count - 1)
            {
                _head.SetItem(item);
                return true;
            }
            DoubleNode<T> temp = _head;
            for (int i = 1; i < _count - pos; i++) temp = temp.GetNext();
            temp.SetItem(item);
            temp = null;
            return true;
        }

        public T GetItem(int pos)
        {
            try
            {
                if (pos < 0 || pos >= _count) throw new InvalidPositionException(pos, _count);
            }
            catch (InvalidPositionException e)
            {
                Console.Write(e.Message());
                return default(T);
            }
            if (pos == _count - 1) return _head.Get();
            DoubleNode<T> temp = _head;
            for (int i = 1; i < _count - pos; i++) temp = temp.GetNext();
            T output = temp.Get();
            temp = null;
            return output;
        }

        public int Size() { return _count; }

        public void Clear()
        {
            while (_head != null)
            {
                DoubleNode<T> temp = _head.GetNext();
                _head = temp;
            }
            _count = 0;
        }

        private DoubleNode<T> _head = null;
        private int _count = 0;
    }
}
