//List ADT Base class header
#ifndef _LIST_H
#define _LIST_H

namespace mille5a9 {

template <class T>
class List {
public:
    virtual bool isEmpty(){}
    virtual void insert(T item){}
    virtual void remove(int position){}
    virtual T getItem(int position){}
    virtual void setItem(int position, T item){}
    virtual void clear(){}
};

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
}

#endif
