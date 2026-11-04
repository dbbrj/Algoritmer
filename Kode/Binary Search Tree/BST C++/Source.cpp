#include "BinarySearchTree.h"
#include <iostream>
using namespace std;

int main()
{
	BinarySearchTree bst;

	bst.insert(3);
	bst.insert(8);
	bst.insert(5);
	bst.insert(33);
	bst.insert(38);
	bst.insert(35);
	bst.insert(23);
	bst.insert(28);
	bst.insert(25);
	bst.insert(13);
	bst.insert(18);
	bst.insert(15);
	bst.remove(35);
	cout << "Contains " << bst.contains(48) << endl;

	bst.printTree();

}