#ifndef _LINKEDQUEUE_H
#define _LINKEDQUEUE_H

namespace mille5a9 {

template <class T>
class Node {
public:
    Node(T item, Node* next)
    :   item(item),
        next(next) {};

    //Accessor and Mutator methods
    T get() { return item; }
    Node* getNext() { return next; }
    void set(T thing) { item = thing; }
    
private:
    T item;
    Node* next;

};

template <class T>
class Queue {
public:
    void enqueue(T item);
    T dequeue();
    T peekFront();
    bool isEmpty();
    int size();

private:
    Node<T>* front = nullptr;
    Node<T>* back = nullptr;
};

//Adds a new item to the back of the queue
template <class T>
void Queue<T>::enqueue(T item) {
        back = new Node<T>(item, back);
    //first enqueue
    if(!front) {
        front = back;
    }
    return;
}

//Removes the front value from the queue if the queue is not empty
template <class T>
T Queue<T>::dequeue() {
    if(isEmpty()) {
        std::cout << "Error: The queue is empty.";
    }
    
    Node<T>* temp = front;
    T data = temp->get();
    
    if(front == back) {
        front = nullptr;
        back = nullptr;
    }else{
        front = back;
        while(front->getNext() != temp) {
            front = front->getNext();
        }
    }
    delete temp;
    return data;
}

//Returns the front item of the queue if the queue is not empty
template <class T>
T Queue<T>::peekFront() {
    if(isEmpty()) {
        std::cout << "The queue is empty.";
    }
    return front->get();
}

//Returns true if the queue is empty and false otherwise
template <class T>
bool Queue<T>::isEmpty() {
    if(!front) return true;
    return false;
}

//Returns the number of items stored in the queue
template <class T>
int Queue<T>::size() {
    int i = 0;
    if(back) {
        Node<T>* temp = back;
        i++;
        while(temp != front) {
            i++;
            temp = temp->getNext();            
        }
    }
    return i;
}
}

#endif
