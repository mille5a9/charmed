#ifndef ARRAYQUEUE_H

namespace mille5a9 {

class Queue {
    public:
        void enqueue(int);
        int dequeue();
        int peekFront();
        bool isEmpty();
        int size();
    private:
        // Change the size of the queue here
        static const int SIZE = 500;
        int items[SIZE];
        int front = -1;
        int rear = -1;
};

//Adds a given value to the rear of the queue if the queue is not full
void Queue::enqueue(int num) {
    if(rear == front - 1 || (rear == SIZE - 1 && front == 0)) {
        std::cout << "Error: Cannot enqueue, queue is full";
        
    //first enqueue
    }else if(front == -1) {
        front = 0;
        rear = 0;
        items[rear] = num;
        
    //check if rear needs to wrap around
    }else if(rear == SIZE - 1 && front != 0){
        rear = 0;
        items[rear] = num;
        
    }else{
        rear++;
        items[rear] = num;
    }
    return;
}

//Removes the front value from the queue if the queue is not empty
int Queue::dequeue() {
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
int Queue::peekFront() {
    if(isEmpty()) {
        std::cout << "The queue is empty.";
    }
    return items[front];
}

//Returns true if the queue is empty and false otherwise
bool Queue::isEmpty() {
    if(front == -1 || front == rear + 1) return true;
    return false;
}

//Returns the number of values stored in the queue
int Queue::size() {
    if (rear + 1 < front) return rear + SIZE - front;
    return rear + 1 - front;
}
}

#endif