#ifndef HASHTABLE_H
#define HASHTABLE_H

namespace mille5a9 {

//Node class for the data in the HashTable
template <class T>
class Node {
public:
    Node(T data) : item(data) {}
    ~Node() { delete item; }
    T get() { return item; }
    void set(T data) { item = data; }
private:
    T item;
};

template <class T, size_t max = 100>
class HashTable {
public:
    //Constructor and Destructor
    HashTable() = default;
    ~HashTable() {
        delete[] table;
        delete size;
    }
    int hash(std::string key);
    bool add(std::string key, Node<T> *value);
    Node<T>* remove(std::string key);
    Node<T>* get(std::string key);
    int getLength() { return size; }

    //overloading for nonstring operands
    std::string intprocess(Node<T> *key);
    bool add(Node<T> *key, Node<T> *value);
    Node<T>* remove(Node<T> *key);
    Node<T>* get(Node<T> *key);

private:
    int size = 0;
    std::pair<std::string, Node<T>*> table[max];
};

//function to hash strings into an integer value
template <class T, size_t max>
int HashTable<T, max>::hash(std::string key) {
    int i = 0, sum = 0;
    while (key[i] != '\0') {
        sum += key[i];
        i++;
    }
    int hashnum = sum % max;
    while (table[hashnum].second) {
        if (table[hashnum].first == key) return hashnum;
        hashnum++;
        if (hashnum == max) hashnum = 0;
        else if (hashnum == sum % max) break;
    }
    return hashnum;
}

//adds a given value to a certain spot in the table
//based on the hash of the given string, resolves
//collisions with linear probing
template <class T, size_t max>
bool HashTable<T, max>::add(std::string key, Node<T> *value) {
    if (size == max) return false;
    int hashnum = hash(key);
    std::pair<std::string, Node<T>*> temp(key, value);
    table[hashnum] = temp;
    size++;
    return true;
}

template <class T, size_t max>
std::string HashTable<T, max>::intprocess(Node<T> *key) {
    int hashnum = key->get();
    std::string out = "";
    for (int i = 0; i <= hashnum / 256; i++) {
        if (i == hashnum / 256) out[i] = hashnum % 256;
        out[i] = 255;
    }
    std::cout << out;
    return out;
}

//this function is overloaded due to the
//task 3 specification
template<class T, size_t max>
bool HashTable<T, max>::add(Node<T> *key, Node<T> *value) {
    if (size == max) return false;
    std::string keystr = intprocess(key);
    return add(keystr, value);
}

//removes data from a space in the hash table so
//that the spot can be added to again
template <class T, size_t max>
Node<T>* HashTable<T, max>::remove(std::string key) {
    int hashnum = hash(key);
    if (table[hashnum].first != key) return nullptr;
    Node<T> *out = table[hashnum].second;
    table[hashnum].first = "";
    table[hashnum].second = nullptr;
    size--;
    return out;
}

//this function is overloaded due to
//the task 4 specification
template <class T, size_t max>
Node<T>* HashTable<T, max>::remove(Node<T> *key) {
    std::string keystr = intprocess(key);
    return remove(keystr);
}

//returns the data at a certain space in the table
//without clearing out the spot
template <class T, size_t max>
Node<T>* HashTable<T, max>::get(std::string key) {
    int hashnum = hash(key);
    if (table[hashnum].first != key) return nullptr;
    return table[hashnum].second;
}

template <class T, size_t max>
Node<T>* HashTable<T, max>::get(Node<T> *key) {
    std::string keystr = intprocess(key);
    return get(keystr);
}
}
#endif
