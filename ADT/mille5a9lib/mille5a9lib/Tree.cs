using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public interface ITree<T>
    {
        int Size(BinaryNode<T> temp);
        int Height(BinaryNode<T> temp);
        bool Insert(T item);
        bool Remove(T item);
        bool Contains(T item);
        ArrayList<T> PreorderTraversal(BinaryNode<T> temp);
        ArrayList<T> InorderTraversal(BinaryNode<T> temp);
        ArrayList<T> PostorderTraversal(BinaryNode<T> temp);
        void Clear(BinaryNode<T> temp);
    }

    class Tree<T>
    {
        public static ITree<T> Create(bool balances)
        {
            if (balances) return new AVLTree<T>();
            else return new BinarySearchTree<T>();
        }
    }

    class BinarySearchTree<T> : ITree<T>
    {

        public int Size(BinaryNode<T> temp)
        {
            int start = 0;
            if (temp != null)
            {
                start += 1;
                start += Size(temp.GetLeft());
                start += Size(temp.GetRight());
            }
            return start;
        }
        public int Height(BinaryNode<T> temp)
        {
            int start = 0;
            if (temp != null)
            {
                start += 1;
                if (Height(temp.GetRight()) > Height(temp.GetLeft()))
                {
                    return start + Height(temp.GetRight());
                }
                else return start + Height(temp.GetLeft());
            }
            return start;
        }
        public bool Insert(T item)
        {
            if (root == null)
            {
                root = new BinaryNode<T>(item);
                return true;
            }
            if (Contains(item)) return false;
            BinaryNode<T> temp = root;
            while (temp != null)
            {
                if (item > temp.Get())
                {
                    if (temp.GetRight() == null)
                    {
                        temp.SetRight(new BinaryNode<T>(item));
                        return true;
                    }
                    temp = temp.GetRight()
                }
            }
        }
        public bool Remove(T item)
        {

        }
        public bool Contains(T item)
        {

        }
        public ArrayList<T> PreorderTraversal(BinaryNode<T> temp)
        {
            ArrayList<T> output = new ArrayList<T>(Size(temp));
            if (temp == null) return output;
            output.Insert(output.Size(), temp.Get());
            ArrayList<T> left = PreorderTraversal(temp.GetLeft());
            foreach (T x in left) output.Insert(output.Size(), x);
            ArrayList<T> right = PreorderTraversal(temp.GetRight());
            foreach (T x in right) output.Insert(output.Size(), x);
            return output;
        }
        public ArrayList<T> InorderTraversal(BinaryNode<T> temp)
        {
            ArrayList<T> output = new ArrayList<T>(Size(temp));
            if (temp == null) return output;
            ArrayList<T> left = PreorderTraversal(temp.GetLeft());
            foreach (T x in left) output.Insert(output.Size(), x);
            output.Insert(output.Size(), temp.Get());
            ArrayList<T> right = PreorderTraversal(temp.GetRight());
            foreach (T x in right) output.Insert(output.Size(), x);
            return output;
        }
        public ArrayList<T> PostorderTraversal(BinaryNode<T> temp)
        {
            ArrayList<T> output = new ArrayList<T>(Size(temp));
            if (temp == null) return output;
            ArrayList<T> left = PreorderTraversal(temp.GetLeft());
            foreach (T x in left) output.Insert(output.Size(), x);
            ArrayList<T> right = PreorderTraversal(temp.GetRight());
            foreach (T x in right) output.Insert(output.Size(), x);
            output.Insert(output.Size(), temp.Get());
            return output;
        }
        public void Clear(BinaryNode<T> temp)
        {

        }

        private BinaryNode<T> root = null;
    }

    class AVLTree<T> : ITree<T>
    {

        public int Size(BinaryNode<T> temp)
        {

        }
        public int Height(BinaryNode<T> temp)
        {

        }
        public bool Insert(T item)
        {

        }
        public bool Remove(T item)
        {

        }
        public bool Contains(T item)
        {

        }
        public ArrayList<T> PreorderTraversal(BinaryNode<T> temp)
        {

        }
        public ArrayList<T> InorderTraversal(BinaryNode<T> temp)
        {

        }
        public ArrayList<T> PostorderTraversal(BinaryNode<T> temp)
        {

        }
        public void Clear(BinaryNode<T> temp)
        {

        }

        private BinaryNode<T> root = null;
    }
}
