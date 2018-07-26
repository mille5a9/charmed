#ifndef MINHEAP_H
#define MINHEAP_H

namespace mille5a9 {

//node class from the pointer-based Stack assignment
//modified to include priority member
template<class T>
class Node {
public:
    Node(T item, int priority, Node<T>* next) :
        item(item),
        priority(priority),
        next(next) {};

    //Accessor and Mutator methods
    T get() { return item; }
    int getp() { return priority; }
    Node<T>* getNext() { return next; }
    void setNext(Node<T>* input) { next = input; }
    void set(T thing) { item = thing; }

private:
    T item;
    int priority;
    Node<T>* next;
};

//BinaryNode class from the BST and AVL Tree labs (6 and 7)
template <class T>
class BinaryNode {
public:
    BinaryNode(T data) : item(data) {};

    //Accessor functions
    T get() { return item; }
    BinaryNode* getLeft() { return left; }
    BinaryNode* getRight() { return right; }

    //Mutator functions
    void set(T data) { item = data; }
    void setLeft(BinaryNode *temp) { left = temp; }
    void setRight(BinaryNode *temp) { right = temp; }

    //Comparator operators for nodes
  bool operator<(const BinaryNode *two) {
        if (this->data < two->data) return true;
        else return false;
    }
    bool operator>(const BinaryNode *two) {
        if (this->data > two->data) return true;
        else return false;
    }
    bool operator==(BinaryNode *two) {
        if (this->data == two->data) return true;
        else return false;
    }
private:
    T item;
    BinaryNode *left = nullptr, *right = nullptr;
};

template <class T>
class MinHeap {
public:
    MinHeap() = default;

    bool isEmpty() const { return !root; }
    bool contains(T item, BinaryNode<T> *temp);

    //tree functionality
    bool insert(T item, BinaryNode<T> *temp,
            BinaryNode<T> *parent);
    T remove(BinaryNode<T> *temp, BinaryNode<T> *parent);

    //getHeight functions for inserting/removing
    //getDepth is for the print function
    int getMinHeight(BinaryNode<T> *temp);
    int getMaxHeight(BinaryNode<T> *temp);
    int getDepth(BinaryNode<T>*temp, BinaryNode<T> *subject);

    //utility functions to manage the tree
    Node<T>* print(BinaryNode<T> *temp, BinaryNode<T> *parent);
    void clear(BinaryNode<T> *temp);

    //root accessor
    BinaryNode<T>* getRoot() { return root; }

    //destroy by clearing all nodes
    ~MinHeap() {
        clear(root);
    }

private:
    BinaryNode<T> *root = nullptr;
};

//helper for insert
template <class T>
bool MinHeap<T>::contains(T item, BinaryNode<T> *temp) {
    if (!temp) return false;
    if (temp->get() == item) return true;
    return (contains(item, temp->getLeft()) ||
            contains(item, temp->getRight()));
}

//returns height of smallest branch, useful for insert
template <class T>
int MinHeap<T>::getMinHeight(BinaryNode<T> *temp) {
    int size = 0;
    if (temp) size++;
    else return size;
    int left, right = getMinHeight(temp->getRight());
    left = getMinHeight(temp->getLeft());
    if (left < right) size += left;
    else size += right;
    return size;
}

//returns height of longest branch, useful for remove
template <class T>
int MinHeap<T>::getMaxHeight(BinaryNode<T> *temp) {
    int size = 0;
    if (temp) size++;
    else return size;
    int left, right = getMaxHeight(temp->getRight());
    left = getMaxHeight(temp->getLeft());
    if (left > right) size += left;
    else size += right;
    return size;
}

//returns height between root and the subject node, useful for print
template<class T>
int MinHeap<T>::getDepth(BinaryNode<T> *temp, BinaryNode<T> *subject) {
    int depth = 0;
    if (!temp) return 0;
    if (temp == subject) return 1;
    depth = getDepth(temp->getLeft(), subject);
    if (depth > 0) return ++depth;
    depth = getDepth(temp->getRight(), subject);
    if (depth > 0) return ++depth;
    return 0;
}

//plants a new item in the next available spot in the heap,
//then moves the new item up its branch until it satisfies
//the properties of a minheap
template <class T>
bool MinHeap<T>::insert(T item, BinaryNode<T> *temp,
        BinaryNode<T> *parent) {
    if (contains(item, root)) return false;
    if (isEmpty()) {
        root = new BinaryNode<T>(item);
        return true;
    } else if (!temp) {
        temp = new BinaryNode<T>(item);
        if (parent->getLeft()) parent->setRight(temp);
        else parent->setLeft(temp);
        return true;
    }
    bool zoop;
    if (getMinHeight(temp->getRight())
            < getMinHeight(temp->getLeft())) {
        zoop = insert(item, temp->getRight(), temp);
        if (temp->getRight() &&
                temp->get() > temp->getRight()->get()) {
            T data = temp->get();
            temp->set(temp->getRight()->get());
            temp->getRight()->set(data);
        }
    } else {
        zoop = insert(item, temp->getLeft(), temp);
        if (temp->getLeft() &&
                temp->get() > temp->getLeft()->get()) {
            T data = temp->get();
            temp->set(temp->getLeft()->get());
            temp->getLeft()->set(data);
        }
    }
    return true;
    
}

//returns the root value of the heap, replaces root with the
//rightmost value on the bottom level, and then cycles that value
//down from root until it satisfies the properties of a minheap
template <class T>
T MinHeap<T>::remove(BinaryNode<T> *temp,
        BinaryNode<T> *parent) {
    if (isEmpty()) {
        std::cout << "Cannot remove from an empty heap!\n";
    }
    T output = temp->get();
    if (!parent && !temp->getLeft()
            && !temp->getRight()) {
        delete temp;
        root = nullptr;
        return output;
    }
    if (!temp->getLeft()) {
        if (parent && (temp == parent->getLeft())) {
            parent->setLeft(nullptr);
        } else if (parent) parent->setRight(nullptr);
        delete temp;
        return output;
    }
    if (getMaxHeight(temp->getLeft()) >
            getMaxHeight(temp->getRight())) {
        temp->set(remove(temp->getLeft(), temp));
        if (parent && temp->get() < parent->get()) {
            T item = temp->get();
            temp->set(parent->get());
            parent->set(item);
        }
        return output;
    } else {
        temp->set(remove(temp->getRight(), temp));
        if (parent && temp->get() < parent->get()) {
            T item = temp->get();
            temp->set(parent->get());
            parent->set(item);
        }
        return output;
    }
}

//prints each level of the tree by making a temporary makeshift
//priority queue but there's probably an easier way to do it
template <class T>
Node<T>* MinHeap<T>::print(BinaryNode<T> *temp, BinaryNode<T> *parent) {
    if (!temp) return nullptr;
    int height = getMaxHeight(root);
    int level = height - getDepth(root, temp);
    //Print function: initial call with root and nullptr

    Node<T> *printval = new Node<T>(temp->get(), level, nullptr);

    //Iterate recursively through tree and create priority queue
    //based on the depth of the item in the tree
    printval->setNext(print(temp->getLeft(), temp));
    Node<T> *nextval = printval;
    while (nextval->getNext()) nextval = nextval->getNext();
    nextval->setNext(print(temp->getRight(), temp));

    //exit condition for subsequent iterations
    if (parent) return printval;
    
    //Prints the priority queue
    for (int i = 0; i < height; i++) {
        nextval = printval;
        do {
            if (nextval->getp() == height - i - 1) {
                std::cout << "Value: " << nextval->get()
                    << "    Priority: " << nextval->getp() << std::endl;
            }
            nextval = nextval->getNext();
        } while (nextval);
    }
    return printval;
}

//this function has been pulled directly from the previous
//two labs that dealt with trees (6 and 7)
template <class T>
void MinHeap<T>::clear(BinaryNode<T> *temp) {
    if (!temp) return;
    clear(temp->getLeft());
    temp->setLeft(nullptr);
    clear(temp->getRight());
    temp->setRight(nullptr);
    delete temp;
    temp = nullptr;
    root = nullptr;
}
}
#endif
