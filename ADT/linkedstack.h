/* EECE2080C US18 Group 6 Lab 3 Task 1 */

#ifndef _LINKEDSTACK_H
#define _LINKEDSTACK_H

namespace mille5a9 {

template <class T>
class Node {
public:
    Node(T item, Node* next) : item(item), next(next) {};
    //Accessor and Mutator methods
    T get() { return item; }
    Node* getNext() { return next; }
    void set(T thing) { item = thing; }
    
private:
    T item;
    Node* next;

};

template <class T>
class Stack {
public:
    void push(T item);
    T pop();
    T peek();
    int size();
    bool isEmpty();

private:
    Node<T>* top = nullptr;
};

//Creates a new Node and updates top
template <class T>
void Stack<T>::push(T item) {
    top = new Node<T>(item, top);
    return;
}

//Returns the value of the top Node, removes top Node
//updates top and doesn't work if the stack is empty
template <class T>
T Stack<T>::pop() {
    if(!isEmpty()) {
        Node<T>* outgoing = top;
        T temp = top->get();
        if(!(top->getNext())) {
            top = nullptr;
        }else{
            top = top->getNext();
        delete outgoing;
        }
        return temp;
    }
    else{
        std::cout << "Error: Stack Underflow, Stack is Empty\n";
        return 0;
    }
}

//Returns the value on top of the stack if the stack is not empty
template <class T>
T Stack<T>::peek() {
    if(isEmpty()) {
        std::cout << "Cannot peek: stack is empty.";
        return 0;
    }
    return top->get();
}

//Returns the number of items in the stack by
//iterating through all of the nodes and counting
template <class T>
int Stack<T>::size() {
    Node<T>* current = top;
    int i = 0;
    for(i = 0; current; i++) {
        if(isEmpty()) break;
        current = current->getNext();
    }
    return i;
}

//Returns true if the stack is empty, and false otherwise
template <class T>
bool Stack<T>::isEmpty() {
    return !top;
}
}

#endif
