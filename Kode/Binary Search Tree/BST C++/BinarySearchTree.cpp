#include "BinarySearchTree.h"

BinarySearchTree::BinarySearchTree() : root{ nullptr }
{
}

BinarySearchTree::~BinarySearchTree()
{
    makeEmpty();
}

int BinarySearchTree::findMin() const
{
    if (isEmpty())
        return -1;
    return findMin(root)->element;
}

int BinarySearchTree::findMax() const
{
    if (isEmpty())
        return -1;
    return findMax(root)->element;
}

bool BinarySearchTree::contains(int x) const
{
    return contains(x, root);
}

bool BinarySearchTree::isEmpty() const
{
    return root == nullptr;
}

void BinarySearchTree::printTree() const
{
    if (isEmpty())
        cout << "Empty tree" << endl;
    else
        printTree(root);
}

void BinarySearchTree::makeEmpty()
{
    root = makeEmpty(root);
}

void BinarySearchTree::insert(int x)
{
    root = insert(x, root);
}

void BinarySearchTree::remove(int x)
{
    root = remove(x, root);
}

BinaryNode* BinarySearchTree::insert(int x, BinaryNode* t)
{
    if (t == nullptr)
        return new BinaryNode{ x, nullptr, nullptr };

    if (x < t->element)
        t->left = insert(x, t->left);
    else if (x > t->element)
        t->right = insert(x, t->right);

    return t;
}

BinaryNode* BinarySearchTree::remove(int x, BinaryNode* t)
{
    if (t == nullptr)
        return nullptr;

    if (x < t->element)
        t->left = remove(x, t->left);
    else if (x > t->element)
        t->right = remove(x, t->right);
    else if (t->left != nullptr && t->right != nullptr)
    {
        t->element = findMin(t->right)->element;
        t->right = remove(t->element, t->right);
    }
    else
    {
        BinaryNode* oldNode = t;
        t = (t->left != nullptr) ? t->left : t->right;
        delete oldNode;
    }

    return t;
}

BinaryNode* BinarySearchTree::findMin(BinaryNode* t) const
{
    if (t == nullptr)
        return nullptr;
    if (t->left == nullptr)
        return t;
    return findMin(t->left);
}

BinaryNode* BinarySearchTree::findMax(BinaryNode* t) const
{
    if (t != nullptr)
        while (t->right != nullptr)
            t = t->right;
    return t;
}

bool BinarySearchTree::contains(int x, BinaryNode* t) const
{
    if (t == nullptr)
        return false;
    if (x < t->element)
        return contains(x, t->left);
    if (x > t->element)
        return contains(x, t->right);
    return true;
}

BinaryNode* BinarySearchTree::makeEmpty(BinaryNode* t)
{
    if (t != nullptr)
    {
        makeEmpty(t->left);
        makeEmpty(t->right);
        delete t;
    }
    return nullptr;
}

void BinarySearchTree::printTree(BinaryNode* t) const
{
    if (t != nullptr)
    {
        printTree(t->left);
        cout << t->element << endl;
        printTree(t->right);
    }
}
