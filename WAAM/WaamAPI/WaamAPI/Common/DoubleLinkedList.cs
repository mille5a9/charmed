using System;
using System.Collections;
using System.Collections.Generic;

namespace WaamAPI.Common
{
    class InvalidPositionException : Exception
    {
        public InvalidPositionException(uint pos, uint count)
        {
            _position = pos;
            _count = count;
        }
        public new string Message()
        {
            return "\nThis position is not valid in this list! pos was " + _position + " but _count was " + _count + ".\n";
        }
        private readonly uint _position;
        private readonly uint _count;
    }
    [Serializable]
    public class DoubleNode<T> where T : IComparable
    {
        public DoubleNode(T item, DoubleNode<T> next, DoubleNode<T> prev)
        {
            _item = item;
            _next = next;
            _prev = prev;
        }

        public T Get() { return _item; }
        public DoubleNode<T> GetNext() { return _next; }
        public DoubleNode<T> GetPrev() { return _prev; }
        public void SetItem(T item) { _item = item; }
        public void SetNext(DoubleNode<T> next) { _next = next; }
        public void SetPrev(DoubleNode<T> prev) { _prev = prev; }

        private T _item;
        private DoubleNode<T> _next;
        private DoubleNode<T> _prev;
    }

    public class LinkedListEnum<T> : IEnumerator<T> where T : IComparable
    {
        public DoubleLinkedList<T> list;
        private int _pos = -1;

        public LinkedListEnum(DoubleLinkedList<T> input) { list = input; }
        public bool MoveNext() { return (++_pos < list.Size()); }
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
                return list.GetItem((uint)_pos);
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
    [Serializable]
    public class DoubleLinkedList<T> : IEnumerable<T> where T : IComparable
    {
        public LinkedListEnum<T> GetEnumerator()
        {
            return new LinkedListEnum<T>(this);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        //special Blockchain method for inserting on the end of the list (at the head)
        //equivalent to Insert(Size(), item)
        public void Append(T item)
        {
            _head = new DoubleNode<T>(item, _head, null);
            if (_head.GetNext() != null) _head.GetNext().SetPrev(_head);
            _count++;
        }

        public bool Contains(T item)
        {
            DoubleNode<T> temp = _head;
            while (temp != null)
            {
                if (temp.Get().CompareTo(item) == 0) return true;
                temp = temp.GetNext();
            }
            return false;
        }

        public T GetItem(uint pos)
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

        public uint Size() { return _count; }

        public void Clear()
        {
            while (_head != null)
            {
                DoubleNode<T> temp = _head.GetNext();
                _head = temp;
            }
            _count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private DoubleNode<T> _head = null;
        private uint _count = 0;
    }
}
