#ifndef BINARY_SEARCH_TREE_H
#define BINARY_SEARCH_TREE_H

#include <iostream>
using namespace std;

class BinaryNode
{
public:
    int element;
    BinaryNode* left;
    BinaryNode* right;

    BinaryNode(int theElement, BinaryNode* lt, BinaryNode* rt)
        : element{ theElement }, left{ lt }, right{ rt } { }
};

class BinarySearchTree
{
public:
    BinarySearchTree();
    ~BinarySearchTree();

    int findMin() const;
    int findMax() const;
    bool contains(int x) const;
    bool isEmpty() const;
    void printTree() const;

    void makeEmpty();
    void insert(int x);
    void remove(int x);

private:
    BinaryNode* root;

    BinaryNode* insert(int x, BinaryNode* t);
    BinaryNode* remove(int x, BinaryNode* t);
    BinaryNode* findMin(BinaryNode* t) const;
    BinaryNode* findMax(BinaryNode* t) const;
    bool contains(int x, BinaryNode* t) const;
    BinaryNode* makeEmpty(BinaryNode* t);
    void printTree(BinaryNode* t) const;
};

#endif
