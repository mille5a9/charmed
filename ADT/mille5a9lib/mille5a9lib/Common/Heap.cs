using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    //Interface to define MinHeap / MaxHeap behavior
    public interface IHeap<T> where T : IComparable
    {
        bool Insert(T item, BinaryNode<T> temp, BinaryNode<T> parent = null);
        bool Contains(T item, BinaryNode<T> temp);
        T Extract(BinaryNode<T> temp, BinaryNode<T> parent = null);
        int GetMinHeight(BinaryNode<T> temp);
        int GetMaxHeight(BinaryNode<T> temp);
        int GetDepth(BinaryNode<T> temp, BinaryNode<T> subject = null);
        LinkedList<T> GetPreorder();
        LinkedList<T> GetInorder();
        LinkedList<T> GetPostorder();
        void Clear(BinaryNode<T> temp);
        BinaryNode<T> GetRoot();
    }


    public class MinHeap<T> : IHeap<T> where T : IComparable
    {
        public bool Insert(T item, BinaryNode<T> temp, BinaryNode<T> parent = null)
        {
            if (Contains(item, _root)) return false;
            if (_root == null)
            {
                _root = new BinaryNode<T>(item);
                return true;
            }
            else if (temp == null)
            {
                temp = new BinaryNode<T>(item);
                if (parent.GetLeft() != null) parent.SetRight(temp);
                else parent.SetLeft(temp);
                return true;
            }
            bool zoop;
            if (GetMinHeight(temp.GetRight()) < GetMinHeight(temp.GetLeft()))
            {
                zoop = Insert(item, temp.GetRight(), temp);
                if (temp.GetRight() != null && temp.Get().CompareTo(temp.GetRight().Get()) > 0)
                {
                    T data = temp.Get();
                    temp.Set(temp.GetRight().Get());
                    temp.GetRight().Set(data);
                }
            }
            else
            {
                zoop = Insert(item, temp.GetLeft(), temp);
                if (temp.GetLeft() != null && temp.Get().CompareTo(temp.GetLeft().Get()) > 0)
                {
                    T data = temp.Get();
                    temp.Set(temp.GetLeft().Get());
                    temp.GetLeft().Set(data);
                }
            }
            return true;
        }
        public bool Contains(T item, BinaryNode<T> temp)
        {
            if (temp == null) return false;
            if (item.CompareTo(temp.Get()) == 0) return true;
            return (Contains(item, temp.GetLeft()) || Contains(item, temp.GetRight()));
        }
        public T Extract(BinaryNode<T> temp, BinaryNode<T> parent = null)
        {
            if (_root == null)
            {
                Console.Write("Cannot remove from an empty heap!");
                return default(T);
            }
            T output = temp.Get();
            if (parent == null && temp.GetLeft() == null && temp.GetRight() == null) return output;
            if (temp.GetLeft() == null)
            {
                if (parent != null && temp == parent.GetLeft()) parent.SetLeft(null);
                else if (parent != null) parent.SetRight(null);
                return output;
            }
            if (GetMaxHeight(temp.GetLeft()) > GetMaxHeight(temp.GetRight()))
            {
                temp.Set(Extract(temp.GetLeft(), temp));
                if (parent != null && temp.Get().CompareTo(parent.Get()) < 0)
                {
                    T item = temp.Get();
                    temp.Set(parent.Get());
                    parent.Set(item);
                }
                return output;
            }
            temp.Set(Extract(temp.GetRight(), temp));
            if (parent != null && temp.Get().CompareTo(parent.Get()) < 0)
            {
                T item = temp.Get();
                temp.Set(parent.Get());
                parent.Set(item);
            }
            return output;
        }
        public int GetMinHeight(BinaryNode<T> temp = null)
        {
            int size = 0;
            if (temp != null) size++;
            else return size;
            int left, right = GetMinHeight(temp.GetRight());
            left = GetMinHeight(temp.GetLeft());
            if (left < right) size += left;
            else size += right;
            return size;
        }
        public int GetMaxHeight(BinaryNode<T> temp = null)
        {
            int size = 0;
            if (temp != null) size++;
            else return size;
            int left, right = GetMaxHeight(temp.GetRight());
            left = GetMaxHeight(temp.GetLeft());
            if (left > right) size += left;
            else size += right;
            return size;
        }
        public int GetDepth(BinaryNode<T> temp = null, BinaryNode<T> subject = null)
        {
            int depth = 0;
            if (temp == null) return 0;
            if (temp == subject) return 1;
            depth = GetDepth(temp.GetLeft(), subject);
            if (depth > 0) return ++depth;
            depth = GetDepth(temp.GetRight(), subject);
            if (depth > 0) return ++depth;
            return 0;
        }
        public LinkedList<T> GetPreorder()
        {
            _preorder.Clear();
            PreorderTraversal(_root);
            return _preorder;
        }
        public LinkedList<T> GetInorder()
        {
            _inorder.Clear();
            InorderTraversal(_root);
            return _inorder;
        }
        public LinkedList<T> GetPostorder()
        {
            _postorder.Clear();
            PostorderTraversal(_root);
            return _postorder;
        }
        private void PreorderTraversal(BinaryNode<T> temp)
        {
            if (temp == null) return;
            _preorder.Insert(_preorder.Size(), temp.Get());
            PreorderTraversal(temp.GetLeft());
            PreorderTraversal(temp.GetRight());
        }
        private void InorderTraversal(BinaryNode<T> temp)
        {
            if (temp == null) return;
            InorderTraversal(temp.GetLeft());
            _inorder.Insert(_inorder.Size(), temp.Get());
            InorderTraversal(temp.GetRight());
        }
        private void PostorderTraversal(BinaryNode<T> temp)
        {
            if (temp == null) return;
            PostorderTraversal(temp.GetLeft());
            PostorderTraversal(temp.GetRight());
            _postorder.Insert(_postorder.Size(), temp.Get());
        }
        public void Clear(BinaryNode<T> temp)
        {
            if (temp.GetLeft() != null)
            {
                Clear(temp.GetLeft());
                temp.SetLeft(null);
            }
            if (temp.GetRight() != null)
            {
                Clear(temp.GetRight());
                temp.SetRight(null);
            }
            temp = null;
            _root = null;
        }
        public BinaryNode<T> GetRoot() { return _root; }

        private BinaryNode<T> _root = null;
        private LinkedList<T> _preorder = new LinkedList<T>();
        private LinkedList<T> _inorder = new LinkedList<T>();
        private LinkedList<T> _postorder = new LinkedList<T>();

    }

    public class MaxHeap<T> : IHeap<T> where T : IComparable
    {
        public bool Insert(T item, BinaryNode<T> temp, BinaryNode<T> parent = null)
        {
            if (Contains(item, _root)) return false;
            if (_root == null)
            {
                _root = new BinaryNode<T>(item);
                return true;
            }
            else if (temp == null)
            {
                temp = new BinaryNode<T>(item);
                if (parent.GetLeft() != null) parent.SetRight(temp);
                else parent.SetLeft(temp);
                return true;
            }
            bool zoop;
            if (GetMinHeight(temp.GetRight()) < GetMinHeight(temp.GetLeft()))
            {
                zoop = Insert(item, temp.GetRight(), temp);
                if (temp.GetRight() != null && temp.Get().CompareTo(temp.GetRight().Get()) < 0)
                {
                    T data = temp.Get();
                    temp.Set(temp.GetRight().Get());
                    temp.GetRight().Set(data);
                }
            }
            else
            {
                zoop = Insert(item, temp.GetLeft(), temp);
                if (temp.GetLeft() != null && temp.Get().CompareTo(temp.GetLeft().Get()) < 0)
                {
                    T data = temp.Get();
                    temp.Set(temp.GetLeft().Get());
                    temp.GetLeft().Set(data);
                }
            }
            return true;
        }
        public bool Contains(T item, BinaryNode<T> temp)
        {
            if (temp == null) return false;
            if (item.CompareTo(temp.Get()) == 0) return true;
            return (Contains(item, temp.GetLeft()) || Contains(item, temp.GetRight()));
        }
        public T Extract(BinaryNode<T> temp, BinaryNode<T> parent = null)
        {
            if (_root == null)
            {
                Console.Write("Cannot remove from an empty heap!");
                return default(T);
            }
            T output = temp.Get();
            if (parent == null && temp.GetLeft() == null && temp.GetRight() == null) return output;
            if (temp.GetLeft() == null)
            {
                if (parent != null && temp == parent.GetLeft()) parent.SetLeft(null);
                else if (parent != null) parent.SetRight(null);
                return output;
            }
            if (GetMaxHeight(temp.GetLeft()) > GetMaxHeight(temp.GetRight()))
            {
                temp.Set(Extract(temp.GetLeft(), temp));
                if (parent != null && temp.Get().CompareTo(parent.Get()) < 0)
                {
                    T item = temp.Get();
                    temp.Set(parent.Get());
                    parent.Set(item);
                }
                return output;
            }
            temp.Set(Extract(temp.GetRight(), temp));
            if (parent != null && temp.Get().CompareTo(parent.Get()) < 0)
            {
                T item = temp.Get();
                temp.Set(parent.Get());
                parent.Set(item);
            }
            return output;
        }
        public int GetMinHeight(BinaryNode<T> temp = null)
        {
            int size = 0;
            if (temp != null) size++;
            else return size;
            int left, right = GetMinHeight(temp.GetRight());
            left = GetMinHeight(temp.GetLeft());
            if (left < right) size += left;
            else size += right;
            return size;
        }
        public int GetMaxHeight(BinaryNode<T> temp = null)
        {
            int size = 0;
            if (temp != null) size++;
            else return size;
            int left, right = GetMaxHeight(temp.GetRight());
            left = GetMaxHeight(temp.GetLeft());
            if (left > right) size += left;
            else size += right;
            return size;
        }
        public int GetDepth(BinaryNode<T> temp, BinaryNode<T> subject = null)
        {
            int depth = 0;
            if (temp == null) return 0;
            if (temp == subject) return 1;
            depth = GetDepth(temp.GetLeft(), subject);
            if (depth > 0) return ++depth;
            depth = GetDepth(temp.GetRight(), subject);
            if (depth > 0) return ++depth;
            return 0;
        }
        public LinkedList<T> GetPreorder()
        {
            _preorder.Clear();
            PreorderTraversal(_root);
            return _preorder;
        }
        public LinkedList<T> GetInorder()
        {
            _inorder.Clear();
            InorderTraversal(_root);
            return _inorder;
        }
        public LinkedList<T> GetPostorder()
        {
            _postorder.Clear();
            PostorderTraversal(_root);
            return _postorder;
        }
        private void PreorderTraversal(BinaryNode<T> temp = null)
        {
            if (temp == null) return;
            _preorder.Insert(_preorder.Size(), temp.Get());
            PreorderTraversal(temp.GetLeft());
            PreorderTraversal(temp.GetRight());
        }
        private void InorderTraversal(BinaryNode<T> temp = null)
        {
            if (temp == null) return;
            InorderTraversal(temp.GetLeft());
            _inorder.Insert(_inorder.Size(), temp.Get());
            InorderTraversal(temp.GetRight());
        }
        private void PostorderTraversal(BinaryNode<T> temp = null)
        {
            if (temp == null) return;
            PostorderTraversal(temp.GetLeft());
            PostorderTraversal(temp.GetRight());
            _postorder.Insert(_postorder.Size(), temp.Get());
        }
        public void Clear(BinaryNode<T> temp)
        {
            if (temp.GetLeft() != null)
            {
                Clear(temp.GetLeft());
                temp.SetLeft(null);
            }
            if (temp.GetRight() != null)
            {
                Clear(temp.GetRight());
                temp.SetRight(null);
            }
            temp = null;
            _root = null;
        }
        public BinaryNode<T> GetRoot() { return _root; }

        private BinaryNode<T> _root = null;
        private LinkedList<T> _preorder = new LinkedList<T>();
        private LinkedList<T> _inorder = new LinkedList<T>();
        private LinkedList<T> _postorder = new LinkedList<T>();


    }
}
