using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public interface IQueue<T>
    {
        bool Enqueue(T item);
        T Dequeue();
        T Peek();
        int Size();
    }

    public class Queue<T>
    {
        public static IQueue<T> Create()
        {
            return new LinkedQueue<T>();
        }

        public static IQueue<T> Create(int maxsize)
        {
            return new ArrayQueue<T>(maxsize);
        }
    }

    public class ArrayQueue<T> : IQueue<T>
    {
        public ArrayQueue(int maxsize)
        {
            _items = new T[maxsize];
            _maxsize = maxsize;
        }

        public bool Enqueue(T item)
        {
            if (_rear == _front - 1 || (_rear == _maxsize - 1 && _front == 0)) return false;
            else if (_front == -1)
            {
                _front = 0;
                _rear = 0;
                _items[_rear] = item;
            }
            else if (_rear == _maxsize - 1 && _front != 0)
            {
                _rear = 0;
                _items[_rear] = item;
            }
            else
            {
                _rear++;
                _items[_rear] = item;
            }
            _size++;
            return true;
        }

        public T Dequeue()
        {
            if (_front == -1 || _front == _rear + 1) throw new StackOverflowException("Cannot Dequeue from an empty Queue");
            T temp = _items[_front];
            _front++;
            if (_front == -1 || _front == _rear + 1)
            {
                _front = -1;
                _rear = -1;
            }
            _size--;
            return temp;
        }

        public T Peek()
        {
            if (_front == -1 || _front == _rear + 1) throw new StackOverflowException("Cannot Peek into an empty Queue");
            return _items[_front];
        }

        public int Size() { return _size; }

        private readonly int _maxsize;
        private T[] _items;
        private int _front = -1;
        private int _rear = -1;
        private int _size = 0;
    }

    public class LinkedQueue<T> : IQueue<T>
    {
        public bool Enqueue(T item)
        {
            _rear = new SingleNode<T>(item, _rear);
            if (_front == null) _front = _rear;
            _size++;
            return true;
        }

        public T Dequeue()
        {
            if (_front == null) throw new StackOverflowException("Cannot Dequeue from an empty Queue");
            SingleNode<T> temp = _front;
            T data = temp.Get();

            if (_front == _rear)
            {
                _front = null;
                _rear = null;
            }
            else
            {
                _front = _rear;
                while (_front.GetNext() != temp) _front = _front.GetNext();
                _size--;
            }
            temp = null;
            return data;
        }

        public T Peek()
        {
            if (_front == null) throw new StackOverflowException("Cannot Peek into an empty Queue");
            return _front.Get();
        }

        public int Size() { return _size; }

        private SingleNode<T> _front = null;
        private SingleNode<T> _rear = null;
        private int _size = 0;
    }
}
