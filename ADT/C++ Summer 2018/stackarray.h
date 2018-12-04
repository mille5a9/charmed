#ifndef ARRAYSTACK_H
#define ARRAYSTACK_H

namespace mille5a9 {

template <class T, size_t max = 100>
class ArrayStack {
public:
    ArrayStack() = default;
    ~ArrayStack() { delete[] items; }
    void push(int num);
    T pop();
    T peek() { return items[top]; }
    bool isEmpty() { top == -1; }
    int getSize() { return size; }
private:
    T items[max];
    int top = -1;
    int size = 0;
};

//Adds a given value to the top of the stack if the stack is not full
template <class T, size_t max>
void ArrayStack::push(int num) {
    if(top == max - 1) {
        std::cout << "Error: Stack Overflow, Stack is Full\n";
        return;
    }
    top = top + 1;
    items[top] = num;
    size++;
    return;
}

//Removes the value from the top of the stack if the stack is not empty
template <class T, size_t max>
T ArrayStack::pop() {
    if(!isEmpty()) {
        top--;
        size--;
        return items[top + 1];
    }
    else{
        std::cout << "Error: Stack Underflow, Stack is Empty\n";
    }
}
}
#endif
