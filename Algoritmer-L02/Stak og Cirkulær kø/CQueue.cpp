#include "CQueue.h"
#include <stdexcept>
#include <iostream>
CQueue::CQueue()
{
	front = 0;
	rear = 0;
	elements = 0;
	navn = "";
}

CQueue::CQueue(string n)
{
	front = 0;
	rear = 0;
	elements = 0;
	navn = n;
}

bool CQueue::isEmpty() const
{
	return elements == 0;
}
bool CQueue::isFull() const
{
	return elements == 10;
}
string CQueue::getNavn() const
{
	return navn;
}
int CQueue::getElements() const
{
	return elements;
}

bool CQueue::enqueue(int x)
{
	if (elements == SIZE || x <= 0)
		return false;
	queue[rear] = x;
	rear = (rear + 1) % 10;
	elements++;
	return true;
}

int CQueue::dequeue()
{
	if (elements == 0)
		return -1;
	int ret = queue[front];
	front = (front + 1) % 10;
	elements--;
	return ret;
}

void CQueue::print() const
{
	for (int i = front; i < front + elements; i++)
		cout << queue[i % 10] << endl;
}

CQueue::~CQueue() {}
