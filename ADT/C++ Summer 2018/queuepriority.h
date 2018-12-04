#ifndef PRIORITYQUEUE_H
#define PRIORITYQUEUE_H
#include <iostream>

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

//link-based Priority Queue
template <class T>
class PriorityQueue {
public:
    PriorityQueue() = default;
    bool isEmpty() const { return !front; }
    void insert(T item, int priority);
    void print();
    T remove();

private:
    Node<T> *front = nullptr;
    int size = 0;
};

//inserts an item by iterating from the front and sliding it
//to the end of the part of the list that has the same priority
template <class T>
void PriorityQueue<T>::insert(T item, int priority) {

    //isEmpty case
    if (!front) {
        front = new Node<T>(item, priority, nullptr);
        return;
    }
    Node<T> *tempfront = front, *temp =
        new Node<T>(item, priority, nullptr);

    if (front->getp() < priority) {
        temp->setNext(front);
        front = temp;
    } else {
        while (tempfront->getNext() && 
            tempfront->getNext()->getp() >= priority)
            tempfront = tempfront->getNext();
        temp->setNext(tempfront->getNext());
        tempfront->setNext(temp);
    }
}

template <class T>
T PriorityQueue<T>::remove() {

    if (isEmpty()) {
        std::cout << "Error: The queue is empty.\n";
        return -1;
    }

    //The queue is inserted so that the first-in
    //highest-priority will always be the front item
    T data = front->get();
    Node<T> *next = front->getNext();
    delete front;
    front = next;
    next = nullptr;
    return data;
}

template <class T>
void PriorityQueue<T>::print() {
    //Print function
    Node<T> *temp = front;
    do {
        std::cout << "Value: " << temp->get() << "    Priority: "
            << temp->getp() << std::endl;
        temp = temp->getNext();
    } while (temp);
    temp = nullptr;
}
}
#endif
