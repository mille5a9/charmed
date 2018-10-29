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

        private readonly T _item;
        private readonly DoubleNode<T> _next;
        private readonly DoubleNode<T> _prev;
    }

    public class BinaryNode<T>
    {
        public BinaryNode(T item) { _item = item; }
        public T Get() { return _item; }
        public BinaryNode<T> GetLeft() { return _left; }
        public BinaryNode<T> GetRight() { return _right; }
        void SetItem(T item) { _item = item; }
        void SetLeft(BinaryNode<T> temp) { _left = temp; }
        void SetRight(BinaryNode<T> temp) { _right = temp; }

        private T _item;
        private BinaryNode<T> _left = null;
        private BinaryNode<T> _right = null;
    }


}
