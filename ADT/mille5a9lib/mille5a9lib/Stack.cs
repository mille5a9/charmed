using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public interface IStack<T>
    {
        bool Push(T item);
        T Pop();
        T Peek();
        int Size();
    }

    public class Stack<T>
    {
        public static IStack<T> Create()
        {
            return new LinkedStack<T>();
        }

        public static IStack<T> Create(int maxsize)
        {
            return new ArrayStack<T>(maxsize);
        }
    }

    public class ArrayStack<T> : IStack<T>
    {
        public ArrayStack(int maxsize)
        {
            T[] _items = new T[maxsize];
            _maxsize = maxsize;
        }

        public bool Push(T item)
        {
            try
            {
                if (_top == _maxsize - 1)
                {
                    throw new StackOverflowException("Error: Stack Overflow, Cannot Push");
                }
            }
            catch (StackOverflowException e)
            {
                Console.Write(e.Message);
                return false;
            }

            _top = _top + 1;
            _items[_top] = item;
            _size++;
            return true;
        }

        public T Pop()
        {
            try
            {
                if (_top == -1)
                {
                    throw new StackOverflowException("Error: Stack Empty, Cannot Pop");
                }
            }
            catch (StackOverflowException e)
            {
                Console.Write(e.Message);
                return default(T);
            }

            _top--;
            _size--;
            return _items[_top + 1];
        }

        public T Peek() { return _items[_top]; }

        public int Size() { return _size; }

        private T[] _items;
        private readonly int _maxsize;
        private int _top = -1;
        private int _size = 0;
    }
    public class LinkedStack<T> : IStack<T>
    {
        public bool Push(T item)
        {
            _top = new SingleNode<T>(item, _top);
            _size++;
            return true;
        }

        public T Pop()
        {
            try
            {
                if (_top == null)
                {
                    throw new StackOverflowException("Error: Stack Empty, Cannot Pop");
                }
            }
            catch (StackOverflowException e)
            {
                Console.Write(e.Message);
                return default(T);
            }

            T temp = _top.Get();
            if (_top.GetNext() == null)
            {
                _top = null;
            }
            else
            {
                SingleNode<T> outgoing = _top;
                _top = _top.GetNext();
                outgoing = null;
            }
            _size--;
            return temp;
        }

        public T Peek()
        {
            try
            {
                if (_top == null)
                {
                    throw new StackOverflowException("Error: Stack Empty, Cannot Peek");
                }
            }
            catch (StackOverflowException e)
            {
                Console.Write(e.Message);
                return default(T);
            }

            return _top.Get();
        }

        public int Size() { return _size; }

        private SingleNode<T> _top = null;
        private int _size = 0;
    }
}
