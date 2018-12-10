using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mille5a9lib
{
    public class Graph<T> where T : IComparable
    {
        public bool HasVertex(T item)
        {
            int size = _vertices.Size();
            for (int i = 0; i < size; i++) if (_vertices.GetItem(i).CompareTo(item) == 0) return true;
            return false;
        }
        int HasVertex(T item, LinkedList<T> temp)
        {
            int size = temp.Size();
            for (int i = 0; i < size; i++) if (temp.GetItem(i).CompareTo(item) == 0) return i;
            return -1;
        }
        public bool AddVertex(T item)
        {
            if (HasVertex(item)) return false;
            _vertices.Append(item);
            return true;
        }
        public bool RemoveVertex(T item)
        {
            if (HasVertex(item) == false) return false;
            int size = _vertices.Size();
            for (int i = 0; i < size; i++)
                if (_vertices.GetItem(i).CompareTo(item) == 0)
                {
                    _vertices.Remove(i);
                    break;
                };
            return true;
        }
        public bool HasEdge(T from, T towards)
        {
            int size = _edges.Size();
            for (int i = 0; i < size; i++)
            {
                if (_edges.GetItem(i).Item1.CompareTo(from) == 0 && _edges.GetItem(i).Item2.CompareTo(towards) == 0) return true;
            }
            return false;
        }
        public bool AddEdge(T from, T towards)
        {
            if (HasEdge(from, towards)) return false;
            if (HasVertex(from) == false) AddVertex(from);
            if (HasVertex(towards) == false) AddVertex(towards);
            Tuple<T, T> x = new Tuple<T, T>(from, towards);
            _edges.Append(x);
            return true;
        }
        public bool RemoveEdge(T from, T towards)
        {
            if (HasEdge(from, towards) == false) return false;
            Tuple<T, T> x = new Tuple<T, T>(from, towards);
            int size = _edges.Size();
            for (int i = 0; i < size; i++)
            {
                if (_edges.GetItem(i).Item1.CompareTo(x.Item1) == 0 && _edges.GetItem(i).Item2.CompareTo(x.Item2) == 0)
                {
                    _edges.Remove(i);
                    break;
                }
            }
            return true;
        }
        public LinkedList<T> GetAdjacentVertices(T from)
        {
            LinkedList<T> output = new LinkedList<T>();
            if (HasVertex(from) == false) return output;

            int size = _edges.Size();
            for (int i = 0; i < size; i++)
            {
                Tuple<T, T> current = _edges.GetItem(i);
                if (current.Item1.CompareTo(from) == 0) output.Append(current.Item2);
            }
            return output;
        }
        public LinkedList<T> DepthFirstSearch(T from, LinkedList<T> temp)
        {
            if (temp == null && HasVertex(from) == false) throw new NullReferenceException();
            LinkedList<T> output = new LinkedList<T>();
            LinkedList<T> adj = new LinkedList<T>();
            LinkedList<T> recur = new LinkedList<T>();
            if (temp == null)
            {
                temp = new LinkedList<T>();
                int vertsize = _vertices.Size();
                for (int i = 0; i < vertsize; i++) temp.Append(_vertices.GetItem(i));
            }

            int HasVert = HasVertex(from, temp);
            if (HasVert > -1)
            {
                output.Append(from);
                temp.Remove(HasVert);
            }
            else return output;

            adj = GetAdjacentVertices(from);
            int size = adj.Size();
            for (int i = 0; i < size; i++)
            {
                recur = DepthFirstSearch(adj.GetItem(i), temp);
                int recursize = recur.Size();
                if (recursize == 0) continue;
                for (int k = 0; k < recursize; k++)
                {
                    HasVert = HasVertex(recur.GetItem(k), temp);
                    output.Append(recur.GetItem(k));
                    if (HasVert > -1) temp.Remove(HasVert);
                }
            }
            return output;
        }
        public LinkedList<T> BreadthFirstSearch(T from, LinkedList<T> temp)
        {
            if (temp == null && HasVertex(from) == false) throw new NullReferenceException();
            LinkedList<T> output = new LinkedList<T>();
            LinkedList<T> adj = new LinkedList<T>();

            if (temp == null)
            {
                temp = new LinkedList<T>();
                int vertsize = _vertices.Size();
                for (int i = 0; i < vertsize; i++) temp.Append(_vertices.GetItem(i));
            }

            int hasvert = HasVertex(from, temp);
            if (hasvert > -1)
            {
                output.Append(from);
                temp.Remove(hasvert);
            }
            else return output;

            int outsize = output.Size();
            for (int i = 0; i < outsize; i++)
            {
                adj = GetAdjacentVertices(output.GetItem(i));
                int size = adj.Size();
                for (int k = 0; k < size; k++)
                {
                    if (size == 0) break;
                    hasvert = HasVertex(adj.GetItem(k), temp);
                    if (hasvert > -1)
                    {
                        output.Append(adj.GetItem(k));
                        temp.Remove(hasvert);
                    }
                }
            }
            return output;
        }
        public LinkedList<T> ShortestPath(T from, T towards)
        {
            LinkedList<T> bfs = BreadthFirstSearch(from, null);
            LinkedList<T> output = new LinkedList<T>();
            int size = bfs.Size();

            for (int i = 0; i < size; i++)
            {
                if (bfs.GetItem(i).CompareTo(towards) == 0)
                {
                    int spot = HasVertex(output.GetItem(0), bfs);
                    bfs.Remove(spot);
                    output.Remove(0);
                    i = -1;
                }
                else if (HasEdge(bfs.GetItem(i), output.GetItem(0)))
                {
                    output.Insert(0, bfs.GetItem(i));
                    i = -1;
                }
                if (output.GetItem(0).CompareTo(from) == 0) break;
            }
            return output;
        }

        LinkedList<T> _vertices = new LinkedList<T>();
        LinkedList<Tuple<T, T>> _edges = new LinkedList<Tuple<T, T>>();
    }
}
