//linkedlist class from lab 4
#ifndef LINKEDLIST_H
#define LINKEDLIST_H
#include <iostream>

//This class holds the error message to be thrown
//if there is ever an invalid position given
class Invalid {
public:
    Invalid(const char* str = "Error: that position is not valid in this list\n") : 
        msg(str) {}
    const char* what() const {return msg;}
private:
    const char* msg;
};

//node class from the pointer-based Stack assignment
template<class T>
class Node {

public:
    Node(T item, Node* next) : item(item), next(next) {};

    //Accessor and Mutator methods
    T get() { return item; }
    Node* getNext() { return next; }
    void setNext(Node<T>* input) { next = input; }
    void set(T thing) { item = thing; }

private:
    T item;
    Node<T>* next;
};

//linked list ADT from lab 4
//thoroughly adjusted for lab 9
template<class T>
class Linkedlist {

public:
    Linkedlist() = default;
    bool isEmpty();
    void insert(int position, T item);
    void remove(int position);
    T getItem(int position);
    void setItem(int position, T item);
    int getSize() const { return size; }
    Node<T>* getHead() const { return head; }
    void clear();
    ~Linkedlist() {
        clear();
    }

    Linkedlist<T>& operator=(const Linkedlist<T> &a) {
        head = a.getHead();
        size = a.getSize();
        return *this;
    }

private:
    Node<T>* head = nullptr;
    int size = 0;
};

//checks if head is null to see if the list is empty
template<class T>
bool Linkedlist<T>::isEmpty() { return !head; }

//puts a new value at a valid point in the list, thereby
//increasing the size of the list
template<class T>
void Linkedlist<T>::insert(int position, T item) {
    try {

        //checking the validity of position parameter
        if(position > size) throw Invalid();
        else {

            //if inserting the first item or inserting after head
            if(!head || position == size) {
                head = new Node<T>(item, head);
                size++;
                return;
            }

            //iterate with two temp pointers to change the "next"
            //member of the node before the new node
            Node<T>* temp2, *temp = head;
            for(int i = 0; i < size - position; i++) {
                temp2 = temp;
                temp = temp->getNext(); 
            }

            size++;
            temp = new Node<T>(item, temp);
            temp2->setNext(temp);
        }
    }catch(const Invalid& a) {
        std::cout << a.what();
    }
}

//removes an item from the list, and reconnects the links,
//reducing the size of the list by one
template<class T>
void Linkedlist<T>::remove(int position) {
    try {

        //throw custom exception if position is too big or small
        if(position >= size || position < 0) throw Invalid();
        else {
            Node<T>* temp = head;

            //if removing head
            if(position == size - 1) {
                head = temp->getNext();
                temp = nullptr;
                size--;
                return;
            }

            /* iterate with two temp pointers to be able to set
               the next pointer to link the list after an item
               is removed from the middle */
            Node<T>* temp2;
            for(int i = 1; i < size - position; i++) {
                temp2 = temp;
                temp = temp->getNext(); 
            }

            size--;
            temp2->setNext(temp->getNext());
            delete temp; //delete last reference to free memory
            temp2 = nullptr;
        }
    }catch(const Invalid& a) {
        std::cout << a.what();
    }
}

//returns an item at a certain position, without changing
//the list
template<class T>
T Linkedlist<T>::getItem(int position) {
    T thing;
    try {

        //throw custom exception if position is too high or low
        if (position < 0 || position >= size) {
            throw Invalid();
        } else {

            //return head's item
            if (position == size - 1) {
                return head->get();
            }

            //iterate through list to find item
            Node<T>* temp = head;
            for (int i = 1; i < size - position; i++) {
                temp = temp->getNext();
            }

            thing = temp->get();
            temp = nullptr;
            return thing;
        }
    }catch(const Invalid& a) {
        std::cout << a.what();
    }
}

//changes an item that already exists in the list without
//altering the size of the list
template<class T>
void Linkedlist<T>::setItem(int position, T item) {
    try {

        //throw custom exception if position is too high or low
        if (position < 0 || position >= size) {
            throw Invalid();
        } else {

            //in the case of position being head
            if (position == size - 1) {
                head->set(item);
                return;
            }

            //iterate through list to set item at position
            Node<T>* temp = head;
            for (int i = 1; i < size - position; i++) {
                temp = temp->getNext();
            }

            temp->set(item);
            temp = nullptr;
            return;
        }
    }catch(const Invalid& a) {
        std::cout << a.what();
    }
}

//empties the list by deleting all references to each of the
//nodes, and then setting head to null
template<class T>
void Linkedlist<T>::clear() {
    Node<T>* temp, *temp2 = head;
    while (temp2) {
        temp = temp2->getNext();
        delete temp2;
        temp2 = temp;
    }
    head = nullptr;
    size = 0;
}
#endif
