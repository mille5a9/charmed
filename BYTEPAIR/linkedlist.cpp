//linkedlist derived class
#include <iostream>
#include "list.h"
#include "linkedstack.h"

namespace mille5a9 {
/*
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
*/
//linked list derived from List base class
template<class T>
class Linkedlist : public List<T> {

public:
    bool isEmpty();
    void insert(int position, T item);
    void remove(int position);
    T getItem(int position);
    void setItem(int position, T item);
    void clear();
    int itemcount = 0;

private:
    Node<T>* head = nullptr;
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
        if(position > itemcount) throw Invalid();
        else {

            //if inserting the first item or inserting after head
            if(!head || position == itemcount) {
                head = new Node<T>(item, head);
                itemcount++;
                return;
            }

            //iterate with two temp pointers to change the "next"
            //member of the node before the new node
            Node<T>* temp2, *temp = head;
            for(int i = 0; i < itemcount - position; i++) {
                temp2 = temp;
                temp = temp->getNext();
            }

            itemcount++;
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
        if(position >= itemcount || position < 0) throw Invalid();
        else {
            Node<T>* temp = head;

            //if removing head
            if(position == itemcount - 1) {
                head = temp->getNext();
                temp = nullptr;
                itemcount--;
                return;
            }

            /* iterate with two temp pointers to be able to set
               the next pointer to link the list after an item
               is removed from the middle */
            Node<T>* temp2;
            for(int i = 1; i < itemcount - position; i++) {
                temp2 = temp;
                temp = temp->getNext(); 
            }

            temp2->setNext(temp->getNext());
            delete temp; //delete last reference to free memory
            temp2 = nullptr;
            itemcount--;
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
        if(position < 0 || position >= itemcount) {
            throw Invalid();
        }else {

            //return head's item
            if(position == itemcount - 1) {
                return head->get();
            }

            //iterate through list to find item
            Node<T>* temp = head;
            for(int i = 1; i < itemcount - position; i++) {
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
        if(position < 0 || position >= itemcount) {
            throw Invalid();
        }else {

            //in the case of position being head
            if(position == itemcount - 1) {
                head->set(item);
                return;
            }

            //iterate through list to set item at position
            Node<T>* temp = head;
            for(int i = 1; i < itemcount - position; i++) {
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
    while(temp2) {
        temp = temp2->getNext();
        delete temp2;
        temp2 = temp;
    }
    head = nullptr;
    itemcount = 0;
}
}
