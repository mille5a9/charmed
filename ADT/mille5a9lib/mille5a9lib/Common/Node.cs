﻿using System;

namespace mille5a9lib
{
    //Generic Node with one data value
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
    
    //Generic Node with one data point and an available link in two directions
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

    //Generic Node with one data point and a link to two different children
    public class BinaryNode<T> where T : IComparable
    {
        public BinaryNode(T item) { _item = item; }
        public T Get() { return _item; }
        public BinaryNode<T> GetLeft() { return _left; }
        public BinaryNode<T> GetRight() { return _right; }
        public void Set(T data) { _item = data; }
        public void SetLeft(BinaryNode<T> temp) { _left = temp; }
        public void SetRight(BinaryNode<T> temp) { _right = temp; }

        private T _item;
        private BinaryNode<T> _left = null;
        private BinaryNode<T> _right = null;
    }


}
