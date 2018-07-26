#ifndef ALIST_H
#define ALIST_H
#include <iostream>

namespace mille5a9 {

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

template <class T>
class ArrayList {
public:
    bool isEmpty();
    void insert(int position, T item);
    void remove(int position);
    T getItem(int position);
    void setItem(int position, T item);
    void clear() { itemcount = 0; }
private:
    T items[1000];
    int itemcount = 0;
};

//used to detect if the list is empty, which
//prevents other functions from working properly
template <class T>
bool ArrayList<T>::isEmpty() {
    if(itemcount == 0) return true;
    return false;
}

//Inserts an item into a position, capable of increasing
//the size of the list
template <class T>
void ArrayList<T>::insert(int position, T item) {
    try {
        if(position > itemcount || position < 0) throw Invalid();
        else {
            //shift items that are larger indexes
            for(int i = itemcount; i >= position; i--) {
                items[i + 1] = items[i];
            }
            items[position] = item;
            itemcount++;
        }
    //display invalid position error message if the position
    //is below zero or larger than the number of items
    }catch(const Invalid& a) {
        std::cout << a.what() << "\ninsert function\n";
    }
}

//removes an item and scoots any later items over
//to fill in the gap. shrinks the size of the list
template <class T>
void ArrayList<T>::remove(int position) {
    try {
        if(position >= itemcount) throw Invalid();
        else{
            for(int i = position; i < itemcount; i++) {
                items[i] = items[i + 1];
            }
            itemcount--;
        }
    }catch(const Invalid& a) {
        std::cout << a.what() << "\nremove function\n";
    }
}

//returns the item at a certain position
template <class T>
T ArrayList<T>::getItem(int position) {
    try {
        if(position < 0 || position >= itemcount) throw Invalid();
        else return items[position];
    }catch(const Invalid& a) {
        std::cout << a.what() << "\ngetItem function\n";
    }
}

//overwrites the item in a position that is already
//a part of the list
template <class T>
void ArrayList<T>::setItem(int position, T item) {
    try {
        if(position < 0 || position >= itemcount) throw Invalid();
        items[position] = item;
    }catch(const Invalid& a) {
        std::cout << a.what() << "\nsetItem function\n";
    }
}
}
#endif
