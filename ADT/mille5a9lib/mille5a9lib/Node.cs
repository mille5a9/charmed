using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public class SingleNode<T>
    {
        public SingleNode(T item, SingleNode<T> next)
        {
            _item = item;
            _next = next;
        }

        public T Get() { return _item; }
        public SingleNode<T> GetNext() { return _next; }

        private readonly T _item;
        private readonly SingleNode<T> _next;
    }

    public class DoubleNode<T>
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

    public class BinaryNode<T> where T : IComparable<T>
    {
        public BinaryNode(T item) { _item = item; }
        public T Get() { return _item; }
        public BinaryNode<T> GetLeft() { return _left; }
        public BinaryNode<T> GetRight() { return _right; }
        public void SetLeft(BinaryNode<T> temp) { _left = temp; }
        public void SetRight(BinaryNode<T> temp) { _right = temp; }

        public int CompareTo(BinaryNode<T> other)
        {
            if (other == null) return 1;
            return Get().CompareTo(other.Get());
        }

        private T _item;
        private BinaryNode<T> _left = null;
        private BinaryNode<T> _right = null;
    }


}
