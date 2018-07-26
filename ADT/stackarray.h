/* EECE2080C US18 Group 6 Lab 2 Task 1 */
#ifndef ARRAYSTACK_H

namespace mille5a9 {

class ArrayStack {
public:
    /* Change SIZE here to adjust size of the towers */
    static const int SIZE = 5;
    int items[SIZE];
    int top = -1;
    void push(int num);
    int pop();
    int size();
    int peek();
    bool isEmpty();
};

//Adds a given value to the top of the stack if the stack is not full
void ArrayStack::push(int num) {
    if(top == SIZE - 1) {
        std::cout << "Error: Stack Overflow, Stack is Full\n";
        return;
    }
    top = top + 1;
    items[top] = num;
    return;
}

//Removes the value from the top of the stack if the stack is not empty
int ArrayStack::pop() {
    if(!isEmpty()) {
        int temp = items[top];
        items[top] = 0;
        top--;
        return temp;
    }
    else{
        std::cout << "Error: Stack Underflow, Stack is Empty\n";
    }
}

//Returns the number of items in the stack
int ArrayStack::size() {
    int temp = top + 1;
    return temp;
}

//Returns the value on top of the stack if the stack is not empty
int ArrayStack::peek() {
    if(top == -1) return 0;
    return items[top];
}

//Returns true if the stack is empty, and false otherwise
bool ArrayStack::isEmpty() {
    /* Should return true if the stack is empty and false otherwise*/
    return top == -1;
}
}

#endif
