#ifndef ARRAYQUEUE_H
#define ARRAYQUEUE_H
#include <iostream>

namespace mille5a9 {

template <class T, size_t max = 100>
class ArrayQueue {
public:
    bool enqueue(int num);
    T dequeue();
    T peekFront();
    bool isEmpty();
    int size();
private:
    int items[max];
    int front = -1;
    int rear = -1;
};

//Adds a given value to the rear of the queue if the queue is not full
template <class T, size_t max>
bool ArrayQueue::enqueue(int num) {
    if(rear == front - 1 || (rear == max - 1 && front == 0)) {
       return false;

    //first enqueue
    }else if(front == -1) {
        front = 0;
        rear = 0;
        items[rear] = num;
        
    //check if rear needs to wrap around
    }else if(rear == max - 1 && front != 0){
        rear = 0;
        items[rear] = num;
        
    }else{
        rear++;
        items[rear] = num;
    }
    return true;
}

//Removes the front value from the queue if the queue is not empty
template <class T, size_t max>
T ArrayQueue::dequeue() {
    if(isEmpty()) {
        std::cout << "Error: The queue is empty.";
    }
    int temp = items[front];
    front++;
    
    //check if dequeue caused queue to become empty and reset
    if(isEmpty()) {
        front = -1;
        rear = -1;
    }
    
    return temp;
}

//Returns the front value of the queue if the queue is not empty
template <class T, size_t max>
T ArrayQueue::peekFront() {
    if(isEmpty()) {
        std::cout << "The queue is empty.";
    }
    return items[front];
}

//Returns true if the queue is empty and false otherwise
template <class T, size_t max>
bool ArrayQueue::isEmpty() {
    if(front == -1 || front == rear + 1) return true;
    return false;
}

//Returns the number of values stored in the queue
template <class T, size_t max>
int ArrayQueue::size() {
    if (rear + 1 < front) return rear + max - front;
    return rear + 1 - front;
}
}

#endif
