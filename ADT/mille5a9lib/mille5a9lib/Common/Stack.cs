using System;

namespace mille5a9lib
{
    //Interface for Array/Linked Stacks
    public interface IStack<T>
    {
        bool Push(T item);
        T Pop();
        T Peek();
        int Size();
    }

    //Base Stack class used to create Stacks
    public class Stack<T>
    {
        //Create is overloaded to make a Linked stack when there are no args
        //and an Array stack when a size arg is supplied
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
            _items = new T[maxsize];
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
            if (_top == -1)
            {
                throw new StackOverflowException("Error: Stack Empty, Cannot Pop");
                //Technically an Underflow, but StackOverflow at least describes that the contents of the stack present a problem for popping
            }

            _top--;
            _size--;
            return _items[_top + 1];
        }

        public T Peek()
        {
            if (_top < 0) throw new StackOverflowException("Error: Cannot Peek an Empty Stack");
            //See Pop() comment
            return _items[_top];
        }

        public int Size() { return _size; }

        private readonly int _maxsize;
        private T[] _items;
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
            if (_top == null)
            {
                throw new StackOverflowException("Error: Stack Empty, Cannot Pop");
                //Technically an Underflow, but StackOverflow at least describes that the contents of the stack present a problem for popping
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
            if (_top == null)
            {
                throw new StackOverflowException("Error: Stack Empty, Cannot Peek");
                //See Pop() comment
            }

            return _top.Get();
        }

        public int Size() { return _size; }

        private SingleNode<T> _top = null;
        private int _size = 0;
    }
}
