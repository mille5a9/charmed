using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public interface ITree<T> where T : IComparable
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

    public class Tree<T> where T : IComparable
    {
        public static ITree<T> Create(bool balances)
        {
            if (balances) return new AVLTree<T>();
            else return new BinarySearchTree<T>();
        }
    }

    public class BinarySearchTree<T> : ITree<T> where T : IComparable
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
            temp = null;
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
            temp = null;
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
                if (item.CompareTo(temp.Get()) > 0)
                {
                    if (temp.GetRight() == null)
                    {
                        temp.SetRight(new BinaryNode<T>(item));
                        temp = null;
                        return true;
                    }
                    temp = temp.GetRight();
                }
                else if (temp.GetLeft() == null)
                {
                    temp.SetLeft(new BinaryNode<T>(item));
                    temp = null;
                    return true;
                }
                temp = temp.GetLeft();
            }
            temp = null;
            return false;
        }
        public bool Remove(T item)
        {
            if (Contains(item) == false) return false;
            BinaryNode<T> tempprev = null, temp = root;
            while (!(item.CompareTo(temp.Get()) == 0))
            {
                tempprev = temp;
                if (item.CompareTo(temp.Get()) > 0) temp = temp.GetRight();
                else temp = temp.GetLeft();
            }
            if (temp.GetLeft() == null && temp.GetRight() == null)
            {
                if (temp == root) root = null;
                else if (tempprev.GetRight() == temp) tempprev.SetRight(null);
                else tempprev.SetLeft(null);
                temp = null;
                tempprev = null;
                return true;
            }
            else if (temp.GetLeft() != null && temp.GetRight() != null)
            {
                BinaryNode<T> temp2 = temp.GetRight();
                tempprev = temp;
                while (temp2.GetLeft() != null)
                {
                    tempprev = temp2;
                    temp2 = temp2.GetLeft();
                }
                temp.Set(temp2.Get());
                tempprev.SetLeft(temp2.GetRight());
                temp2 = null;
                tempprev = null;
                return true;
            }
            else
            {
                BinaryNode<T> temp2;
                if (temp.GetLeft() != null) temp2 = temp.GetLeft();
                else temp2 = temp.GetRight();
                temp.Set(temp2.Get());
                temp.SetLeft(temp2.GetLeft());
                temp.SetRight(temp2.GetRight());
                temp2 = null;
                tempprev = null;
                return true;
            }
        }
        public bool Contains(T item)
        {
            if (root == null) return false;
            if (item.CompareTo(root.Get()) == 0) return true;
            BinaryNode<T> temp = root;
            while (temp != null)
            {
                if (item.CompareTo(temp.Get()) == 0)
                {
                    temp = null;
                    return true;
                }
                if (item.CompareTo(temp.Get()) > 0) temp = temp.GetRight();
                else temp = temp.GetLeft();
            }
            temp = null;
            return false;
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
            root = null;
        }

        private BinaryNode<T> root = null;
    }

    public class AVLTree<T> : ITree<T> where T : IComparable
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
            temp = null;
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
            temp = null;
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
                if (item.CompareTo(temp.Get()) > 0)
                {
                    if (temp.GetRight() == null)
                    {
                        temp.SetRight(new BinaryNode<T>(item));
                        temp = null;
                        root = Balance(root);
                        return true;
                    }
                    temp = temp.GetRight();
                }
                else if (temp.GetLeft() == null)
                {
                    temp.SetLeft(new BinaryNode<T>(item));
                    temp = null;
                    root = Balance(root);
                    return true;
                }
                temp = temp.GetLeft();
            }
            temp = null;
            return false;
        }
        public bool Remove(T item)
        {
            if (Contains(item) == false) return false;
            BinaryNode<T> tempprev = null, temp = root;
            while (!(item.CompareTo(temp.Get()) == 0))
            {
                tempprev = temp;
                if (item.CompareTo(temp.Get()) > 0) temp = temp.GetRight();
                else temp = temp.GetLeft();
            }
            if (temp.GetLeft() == null && temp.GetRight() == null)
            {
                if (temp == root) root = null;
                else if (tempprev.GetRight() == temp) tempprev.SetRight(null);
                else tempprev.SetLeft(null);
                temp = null;
                tempprev = null;
                root = Balance(root);
                return true;
            }
            else if (temp.GetLeft() != null && temp.GetRight() != null)
            {
                BinaryNode<T> temp2 = temp.GetRight();
                tempprev = temp;
                while (temp2.GetLeft() != null)
                {
                    tempprev = temp2;
                    temp2 = temp2.GetLeft();
                }
                temp.Set(temp2.Get());
                tempprev.SetLeft(temp2.GetRight());
                temp2 = null;
                tempprev = null;
                root = Balance(root);
                return true;
            }
            else
            {
                BinaryNode<T> temp2;
                if (temp.GetLeft() != null) temp2 = temp.GetLeft();
                else temp2 = temp.GetRight();
                temp.Set(temp2.Get());
                temp.SetLeft(temp2.GetLeft());
                temp.SetRight(temp2.GetRight());
                temp2 = null;
                tempprev = null;
                root = Balance(root);
                return true;
            }
        }
        public bool Contains(T item)
        {
            if (root == null) return false;
            if (item.CompareTo(root.Get()) == 0) return true;
            BinaryNode<T> temp = root;
            while (temp != null)
            {
                if (item.CompareTo(temp.Get()) == 0)
                {
                    temp = null;
                    return true;
                }
                if (item.CompareTo(temp.Get()) > 0) temp = temp.GetRight();
                else temp = temp.GetLeft();
            }
            temp = null;
            return false;
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
            root = null;
        }

        public BinaryNode<T> RotateRight(BinaryNode<T> temp)
        {
            BinaryNode<T> zoop = temp.GetLeft();
            temp.SetLeft(zoop.GetRight());
            zoop.SetRight(temp);
            return zoop;
        }
        public BinaryNode<T> RotateLeft(BinaryNode<T> temp)
        {
            BinaryNode<T> zoop = temp.GetRight();
            temp.SetRight(zoop.GetLeft());
            zoop.SetLeft(temp);
            return zoop;
        }
        public BinaryNode<T> Balance(BinaryNode<T> temp)
        {
            if (temp == null) return null;
            temp.SetLeft(Balance(temp.GetLeft()));
            temp.SetRight(Balance(temp.GetRight()));
            int bf = Height(temp.GetLeft()) - Height(temp.GetRight());
            if (bf > 1) temp = RotateRight(temp);
            else if (bf < -1) temp = RotateLeft(temp);
            return temp;
        }

        private BinaryNode<T> root = null;
    }
}
