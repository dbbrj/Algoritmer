#include <iostream>
#include "Stak.h";
#include "CQueue.h"
using namespace std;

string balancedBrackets(string s)
{
	Stak stak("Min stak");

	for (int i = 0; i < s.size(); i++)
	{
		if (s.at(i) != '(' and s.at(i) != ')')
			continue;
		if (s.at(i) == '(')
			stak.push('(');
		else
		{
			if (stak.isEmpty())
				return("Brackets are unbalanced");
			stak.pop();
		}
	}
	if (stak.isEmpty())
		return "Brackets are balanced";
	return "Brackets are unbalanced";

}

int main()
{
	string test = ")kkk.(ooo()"; 
	cout << balancedBrackets(test) << endl;

	CQueue q("MyQueue");
	q.enqueue(10);
	q.enqueue(20);
	q.enqueue(30);
	q.enqueue(40);

	cout << q.dequeue() << endl;			// 10
	cout << q.dequeue() << endl << endl;    // 20

	q.print();

	q.enqueue(50);
	q.enqueue(60);
	q.enqueue(70);
	q.enqueue(80);
	q.enqueue(90);
	q.enqueue(100);
	q.enqueue(110);
	q.enqueue(120);

	q.print();
	cout << q.getElements() << endl;
}


	

