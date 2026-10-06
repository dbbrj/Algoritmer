#ifndef CQUEUE_H
#define CQUEUE_H
#include <string>
using namespace std;
class CQueue
{
public:
	CQueue();
	CQueue(string);

	bool enqueue(int);
	int dequeue();
	bool isFull() const;
	bool isEmpty() const;
	string getNavn() const;
	int getElements() const;
	void print() const;
	
	~CQueue();
private:
	static const int SIZE = 10;
	string navn;
	int front = 0;
	int rear = 0;
	int elements = 0;
	int queue[SIZE]; // Indeholder naturlige tal
};

#endif
