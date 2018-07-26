#ifndef BST_H
#define BST_H

namespace mille5a9 {

template <class T>
class BinarySearchTree {
public:
    BinarySearchTree() = default;
    bool isEmpty() const { return !root; }
    int size(BinaryNode<T> *temp) const;
    int getHeight(BinaryNode<T> *temp) const;
    bool insert(const T& item);
    bool remove(const T& item);
    bool contains(const T& item);
    void preorderTraversal(BinaryNode<T> *temp);
    void inorderTraversal(BinaryNode<T> *temp);
    void postorderTraversal(BinaryNode<T> *temp);
    void clear(BinaryNode<T> *temp);
    ~BinarySearchTree() {
        //delete all of the nodes
        clear(root);
    }
    BinaryNode<T> *root = nullptr;
};

template <class T>
int BinarySearchTree<T>::getHeight(BinaryNode<T> *temp) const {
    int start = 0;
    if (temp) {
        start += 1;
        if (getHeight(temp->getLeft()) >
                getHeight(temp->getRight())) {
            return start + getHeight(temp->getLeft());
        } else return start + getHeight(temp->getRight());
    }
    temp = nullptr;
    return start;
}


template <class T>
int BinarySearchTree<T>::size(BinaryNode<T> *temp) const {
    int start = 0;
    if (temp) {
        start += 1;
        start += size(temp->getLeft());
        start += size(temp->getRight());
    }
    temp = nullptr;
    return start;
}

template <class T>
bool BinarySearchTree<T>::insert(const T& item) {
    if (isEmpty()) {
        root = new BinaryNode<T>(item);
        return true;
    }
    if (contains(item)) return false;
    BinaryNode<T> *temp = root;
    while (temp) {
        if (item > temp->get()) {
            if (!temp->getRight()) {
                temp->setRight(new BinaryNode<T>(item));
                temp = nullptr;
                return true;
            }
            temp = temp->getRight();
            continue;
        } else if (!temp->getLeft()) {
                temp->setLeft(new BinaryNode<T>(item));
                temp = nullptr;
                return true;
        }
        temp = temp->getLeft();
    }
    temp = nullptr;
    return false;
}

template <class T>
bool BinarySearchTree<T>::remove(const T& item) {
    if (!contains(item)) {
        return false;
    }
    BinaryNode<T> *tempprev = nullptr, *temp = root;
    while (temp->get() != item) {
        tempprev = temp;
        if (item > temp->get()) temp = temp->getRight();
        else temp = temp->getLeft();
    }
    if (!temp->getLeft() && !temp->getRight()) {
        //simply remove from tree
        if(temp == root) root = nullptr;
        else if (tempprev->getRight() == temp)
            tempprev->setRight(nullptr);
        else tempprev->setLeft(nullptr);
        delete temp;
        temp = nullptr;
        tempprev = nullptr;
        return true;
    } else if (temp->getLeft() && temp->getRight()) {
        //find inorder successor and copy contents,
        //then delete inorder successor
        //inorder = left, root, right
        //so inorder successor is the minimum value
        //in the right subtree
        BinaryNode<T> *temp2 = temp->getRight();
        tempprev = temp;
        while (temp2->getLeft()) {
            tempprev = temp2;
            temp2 = temp2->getLeft();
        }
        temp->set(temp2->get());
        tempprev->setLeft(temp2->getRight());
        delete temp2;
        temp2 = nullptr;
        tempprev = nullptr;
        return true;
    } else {
        //copy the only child to the node
        //and delete the child
        BinaryNode<T> *temp2;
        if (temp->getLeft()) {
            temp2 = temp->getLeft();
        } else {
            temp2 = temp->getRight();
        }
        temp->set(temp2->get());
        temp->setLeft(temp2->getLeft());
        temp->setRight(temp2->getRight());
        delete temp2;
        temp2 = nullptr;
        tempprev = nullptr;
        return true;
    }
}

template <class T>
bool BinarySearchTree<T>::contains(const T& item) {
    if (isEmpty()) return false;
    if (root->get() == item) return true;
    BinaryNode<T> *temp = root;
    while (temp) {
        if (temp->get() == item) {
            temp = nullptr;
            return true;
        }
        if (item > temp->get()) temp = temp->getRight();
        else temp = temp->getLeft();
    }
    temp = nullptr;
    return false;
}

template <class T>
void BinarySearchTree<T>::preorderTraversal(BinaryNode<T> *temp) {
    if (!temp) return;
    std::cout << temp->get() << std::endl;
    preorderTraversal(temp->getLeft());
    preorderTraversal(temp->getRight());
    temp = nullptr;
}

template <class T>
void BinarySearchTree<T>::inorderTraversal(BinaryNode<T> *temp) {
    if (!temp) return;
    inorderTraversal(temp->getLeft());
    std::cout << temp->get() << std::endl;
    inorderTraversal(temp->getRight());
    temp = nullptr;
}

template <class T>
void BinarySearchTree<T>::postorderTraversal(BinaryNode<T> *temp) {
    if (!temp) return;
    postorderTraversal(temp->getLeft());
    postorderTraversal(temp->getRight());
    std::cout << temp->get() << std::endl;
    temp = nullptr;
}

template <class T>
void BinarySearchTree<T>::clear(BinaryNode<T> *temp) {
    if (temp->getLeft()) {
        clear(temp->getLeft());
        temp->setLeft(nullptr);
    }
    if (temp->getRight()) {
        clear(temp->getRight());
        temp->setRight(nullptr);
    }
    delete temp;
    temp = nullptr;
    root = nullptr;
}
}
#endif
