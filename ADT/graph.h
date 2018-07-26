//graph ADT for lab 9
#ifndef GRAPH_H
#define GRAPH_H
#include "listlinked.h"

namespace mille5a9 {

template <class T>
class Graph {
public:
    //constructor and destructor
    Graph() = default;
    ~Graph() {
        delete vertices;
        delete edges;
    }

    //vertex management
    bool hasVertex(T item);
    int hasVertex(LinkedList<T> *temp, T item);
    bool addVertex(T item);
    bool removeVertex(T item);

    //edge management
    bool hasEdge(T from, T towards);
    bool addEdge(T from, T towards);
    bool removeEdge(T from, T towards);

    LinkedList<T>* getAdjacentVertices(T from);

    //task 3 (bonus)
    LinkedList<T>* depthFirstSearch(T from, LinkedList<T> *temp);
    LinkedList<T>* breadthFirstSearch(T from, LinkedList<T> *temp);

    //maze-solving function
    LinkedList<T>* shortestPath(T from, T towards);

private:
    typedef std::pair<T, T> U;
    LinkedList<T>* vertices = new LinkedList<T>();
    LinkedList<U>* edges = new LinkedList<U>();
};

//tests to see if a given item exists in the graph
//returns boolean as opposed to an integer
template<class T>
bool Graph<T>::hasVertex(T item) {
    int size = vertices->getSize();
    for (int i = 0; i < size; i++)
        if (vertices->getItem(i) == item) return true;
    return false;
}

//returns position of vertex or -1 if it doesn't exist
template <class T>
int Graph<T>::hasVertex(LinkedList<T> *temp, T item) {
    int size = temp->getSize();
    for (int i = 0; i < size; i++)
        if (temp->getItem(i) == item) return i;
    return -1;
}

//creates a vertex if an edge is added where no
//vertex exists
template <class T>
bool Graph<T>::addVertex(T item) {
    if (hasVertex(item)) return false;
    vertices->insert(vertices->getSize(), item);
    return true;
}

//removes a vertex, currently not implemented in
//the other functions but it could be useful in
//the future
template <class T>
bool Graph<T>::removeVertex(T item) {
    if (!hasVertex(item)) return false;
    int size = vertices->getSize();
    for (int i = 0; i < size; i++) {
        if (vertices->getItem(i) == item) {
            vertices->remove(i);
            break;
        }
    }
    return true;
}

//returns a boolean value relating to the
//existence of a given edge
template <class T>
bool Graph<T>::hasEdge(T from, T towards) {
    int size = edges->getSize();
    for (int i = 0; i < size; i++) {
        if (edges->getItem(i).first == from
                && edges->getItem(i).second == towards)
            return true;
    }
    return false;
}

//creates a new edge, and maybe creates vertices
//if the new edge includes never-before-seen points
template <class T>
bool Graph<T>::addEdge(T from, T towards) {
    if (hasEdge(from, towards)) return false;
    if (!hasVertex(from)) addVertex(from);
    if (!hasVertex(towards)) addVertex(towards);
    std::pair<T, T> subject(from, towards);
    edges->insert(edges->getSize(), subject);
    return true;
}

//takes an edge away
template <class T>
bool Graph<T>::removeEdge(T from, T towards) {
    if (!hasEdge(from, towards)) return false;
    std::pair<T, T> subject(from, towards);
    int size = edges->getSize();
    for (int i = 0; i < size; i++) {
        if (edges->getItem(i) == subject) {
            edges->remove(i);
            break;
        }
    }
    return true;
}

//returns a list of vertices that the given point
//has an edge toward
template <class T>
LinkedList<T>* Graph<T>::getAdjacentVertices(T from) {
    LinkedList<T> *out = new LinkedList<T>();
    if (!hasVertex(from)) return out;

    //iterates through the edges list
    int size = edges->getSize();
    for (int i = 0; i < size; i++) {
        std::pair<T, T> iItem = edges->getItem(i);

        //if the first part of the edge matches the arg,
        //adds the second part of the edge to the out list
        if (iItem.first == from) {
            out->insert(out->getSize(), iItem.second);
        }
    }
    return out;
}

//temp argument should first be given as the list of vertices,
//temp should be removed from during recursion
template <class T>
LinkedList<T>* Graph<T>::depthFirstSearch(T from, LinkedList<T>* temp) {
    try {
        if (!temp && !hasVertex(from)) throw Invalid();
    } catch (const Invalid& a) {
        std::cout << a.what(); 
    }//throw error if the vertex doesn't exist

    LinkedList<T> *out = new LinkedList<T>(),
                  *adj = new LinkedList<T>(),
                  *recur = new LinkedList<T>();

    //populates a copy of the list of vertices in the graph
    //but only if this is the very first iteration and not
    //a recursive call
    if (!temp) {
        temp = new LinkedList<T>();
        int vertsize = vertices->getSize();
        for (int i = 0; i < vertsize; i++) {
            temp->insert(temp->getSize(), vertices->getItem(i));
        }
    }

    //adds "from" to the output list and removes it from the
    //temporary vertex list
    int hasvert = hasVertex(temp, from);
    if (hasvert > -1) {
        out->insert(out->getSize(), from);
        temp->remove(hasvert);
    } else return out;

    //iterates through adjacent vertices, recursively calling
    //this function to keep finding new vertices and removing
    //them from the temporary vertex list
    adj = getAdjacentVertices(from);
    int size = adj->getSize();
    for (int i = 0; i < size; i++) {
        recur = depthFirstSearch(adj->getItem(i), temp);

        //iterates through the result of the recursive call
        //to add that list to the output list of the current call
        int recursize = recur->getSize();
        if (recursize == 0) continue;
        for (int k = 0; k < recursize; k++) {
            hasvert = hasVertex(temp, recur->getItem(k));
            out->insert(out->getSize(), recur->getItem(k));
            if (hasvert > -1) temp->remove(hasvert);
        }
    }
    return out;
}

template <class T>
LinkedList<T>* Graph<T>::breadthFirstSearch(T from, LinkedList<T> *temp) {
    try {
        if (!temp && !hasVertex(from)) throw Invalid();
    } catch (const Invalid& a) {
        std::cout << a.what(); 
    }//throw error if the vertex doesn't exist
    LinkedList<T> *out = new LinkedList<T>(),
                  *adj = new LinkedList<T>();

    //populates a copy of the list of vertices in the graph
    if (!temp) {
        temp = new LinkedList<T>();
        int vertsize = vertices->getSize();
        for (int i = 0; i < vertsize; i++) {
            temp->insert(temp->getSize(), vertices->getItem(i));
        }
    }

    //adds the current "from" point to the output list and
    //removes "from" from the temporary vertex list
    int hasvert = hasVertex(temp, from);
    if (hasvert > -1) {
        out->insert(out->getSize(), from);
        temp->remove(hasvert);
    } else return out;

    //iterates through the output list to add new vertices
    //that are adjacent to the current ones
    for (int i = 0; i < out->getSize(); i++) {
        adj = getAdjacentVertices(out->getItem(i));
        int size = adj->getSize();
        for (int i = 0; i < size; i++) {
            if (!size) break;
            hasvert = hasVertex(temp, adj->getItem(i));
            if (hasvert > -1) {
                out->insert(out->getSize(), adj->getItem(i));
                temp->remove(hasvert);
            }
        }
    }
    return out;
}

template <class T>
LinkedList<T>* Graph<T>::shortestPath(T from, T towards) {
    try {
        //if (!temp && (!hasVertex(from) || !hasVertex(towards))) throw Invalid();
    } catch (const Invalid& a) {
        std::cout << a.what(); 
    }
    LinkedList<T> *bfs = breadthFirstSearch(from, nullptr);
    LinkedList<T> *out = new LinkedList<T>();
    int size = bfs->getSize();
    out->insert(0, towards);

    for (int i = 0; i < size; i++) {
        if (bfs->getItem(i) == towards) {
            int spot = hasVertex(bfs, out->getItem(0));
            bfs->remove(spot);
            out->remove(0);
            i = -1;
        } else if (hasEdge(bfs->getItem(i), out->getItem(0))) {
            out->insert(0, bfs->getItem(i));
            i = -1;
        }
        if (out->getItem(0) == from) break;
    }
    return out;
}
}
#endif
