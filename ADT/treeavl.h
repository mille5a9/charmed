#ifndef AVLT_H
#define AVLT_H

namespace mille5a9 {

template <class T>
class AVLTree {
public:
    //default constructor creates the root ptr
    AVLTree() = default;
    bool isEmpty() const { return !rootptr; }
    int size(BinaryNode<T> *temp) const;
    int getHeight(BinaryNode<T> *temp) const;

    //Utility functions to add to, and subtract from, the tree
    bool insert(BinaryNode<T> *temp, const T& item);
    bool remove(BinaryNode<T> *temp,
        BinaryNode<T> *parent, const T& item);
    bool contains(BinaryNode<T> *temp, const T& item);

    //Utility functions to visualize the tree
    void preorderTraversal(BinaryNode<T> *temp, int distance);
    void inorderTraversal(BinaryNode<T> *temp, int distance);
    void postorderTraversal(BinaryNode<T> *temp, int distance);

    //Three AVL-specific balancing functions for use inside
    //of insert() and remove()
    BinaryNode<T>* rotateRight(BinaryNode<T> *temp);
    BinaryNode<T>* rotateLeft(BinaryNode<T> *temp);
    BinaryNode<T>* balance(BinaryNode<T> *temp);

    void clear(BinaryNode<T> *temp);
    ~AVLTree() {
        //delete all of the nodes
        clear(root);
    }

    //Accessor and Mutator functions for root
    BinaryNode<T>* root() { return rootptr; }
    void setRoot(BinaryNode<T>* temp) { rootptr = temp; }

private:
    BinaryNode<T> *rootptr = nullptr;
};

template <class T>
int AVLTree<T>::getHeight(BinaryNode<T> *temp) const {
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
int AVLTree<T>::size(BinaryNode<T> *temp) const {
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
bool AVLTree<T>::insert(BinaryNode<T> *temp, const T& item) {
    if(isEmpty()) {
        rootptr = new BinaryNode<T>(item);
        return true;
    }
    if (contains(temp, item)) return false;
    if (item < temp->get()) {
        if (!temp->getLeft()) {
            temp->setLeft(new BinaryNode<T>(item));
            rootptr = balance(rootptr);
            return true;
        }
        return insert(temp->getLeft(), item);
    }
    if (item > temp->get()) {
        if (!temp->getRight()) {
            temp->setRight(new BinaryNode<T>(item));
            rootptr = balance(rootptr);
            return true;
        }
        return insert(temp->getRight(), item);
    }
    return false;
}

template <class T>
bool AVLTree<T>::remove(BinaryNode<T> *temp,
    BinaryNode<T> *parent, const T& item) {
    if (temp == rootptr && !contains(temp, item)) return false;
    if (!temp) return false;
    if (item > temp->get()) return remove(temp->getRight(), temp, item);
    if (item < temp->get()) return remove(temp->getLeft(), temp, item);
    BinaryNode<T> *left = temp->getLeft(), *right = temp->getRight();
    
    //if theres only one child
    if ((left && !right) || (!left && right)) {
        if (right) {
            temp->set(right->get());
            temp->setLeft(right->getLeft());
            temp->setRight(right->getRight());
            delete right;
            rootptr = balance(rootptr);
            return true;
        } else {
            temp->set(left->get());
            temp->setLeft(left->getLeft());
            temp->setRight(left->getRight());
            delete left;
            rootptr = balance(rootptr);
            return true;
        }
    }

    //if theres two children to worry about
    if (left && right) {
        left = right;
        BinaryNode<T> *prevleft = nullptr;
        while (left->getLeft()) {
            prevleft = left;
            left = left->getLeft();
        }
        if (prevleft) prevleft->setLeft(left->getRight());
        else temp->setRight(right->getRight());
        temp->set(left->get());
        bool dummy = remove(temp->getRight(), temp, left->get());
        return true;
    }
    
    //if there are no children to speak of
    if (!parent) rootptr = nullptr;
    else if (parent->getLeft() == temp) parent->setLeft(nullptr);
    else parent->setRight(nullptr);
    delete temp;
    temp = nullptr;
    rootptr = balance(rootptr);
    return true;
}

template <class T>
bool AVLTree<T>::contains(BinaryNode<T> *temp, const T& item) {
    if (!temp) return false;
    if (temp->get() == item) return true;
    return (contains(temp->getLeft(), item) ||
        contains(temp->getRight(), item));
}

template <class T>
void AVLTree<T>::preorderTraversal(BinaryNode<T> *temp, int distance) {
    if (!temp) return;
    for (int i = -1; i < distance; i++) {
        if (i > -1) std::cout << "-";
    }
    std::cout << temp->get() << std::endl;
    preorderTraversal(temp->getLeft(), distance + 1);
    preorderTraversal(temp->getRight(), distance + 1);
    temp = nullptr;
}

template <class T>
void AVLTree<T>::inorderTraversal(BinaryNode<T> *temp, int distance) {
    if (!temp) return;
    inorderTraversal(temp->getLeft(), distance + 1);
    for (int i = -1; i < distance; i++) {
        if (i > -1) std::cout << "-";
    }
    std::cout << temp->get() << std::endl;
    inorderTraversal(temp->getRight(), distance + 1);
    temp = nullptr;
}

template <class T>
void AVLTree<T>::postorderTraversal(BinaryNode<T> *temp, int distance) {
    if (!temp) return;
    postorderTraversal(temp->getLeft(), distance + 1);
    postorderTraversal(temp->getRight(), distance + 1);
    for (int i = -1; i < distance; i++) {
        if (i > -1) std::cout << "-";
    }
    std::cout << temp->get() << std::endl;
    temp = nullptr;
}

template <class T>
BinaryNode<T>* AVLTree<T>::rotateRight(BinaryNode<T> *temp) {
    BinaryNode<T> *zoop = temp->getLeft();
    temp->setLeft(zoop->getRight());
    zoop->setRight(temp);
    return zoop;
}

template <class T>
BinaryNode<T>* AVLTree<T>::rotateLeft(BinaryNode<T> *temp) {
    BinaryNode<T> *zoop = temp->getRight();
    temp->setRight(zoop->getLeft());
    zoop->setLeft(temp);
    return zoop;
}

template <class T>
BinaryNode<T>* AVLTree<T>::balance(BinaryNode<T> *temp) {
    if (!temp) return nullptr;
    temp->setLeft(balance(temp->getLeft()));
    temp->setRight(balance(temp->getRight()));
    int bf = getHeight(temp->getLeft()) - getHeight(temp->getRight());
    if (bf > 1) {
        temp = rotateRight(temp);
    } else if (bf < -1) {
        temp = rotateLeft(temp);
    }
    return temp;
}

template <class T>
void AVLTree<T>::clear(BinaryNode<T> *temp) {
    if (isEmpty()) return;
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
    rootptr = nullptr;
}
}
#endif
