#include <stdexcept>
#include <iostream>
#include "Stak.h"
Stak::Stak()
{
	stakSize = 0;
	navn = "";
}

Stak::Stak(string n)
{
	stakSize = 0;
	navn = n;
}

bool Stak::isEmpty() const
{
	return stakken.empty();
}

string Stak::getNavn() const
{
	return navn;
}

int Stak::getStakSize() const
{
	return stakSize;
}

void Stak::push(char p)
{
	stakken.insert(stakken.begin(), p);
	stakSize++;
}

char Stak::pop()
{
	char p = ' ';

	if (isEmpty())
	{
		cout << "Stakken er tom" << endl;
		return p;
	}
	
	p = stakken[0];
	stakken.erase(stakken.begin());
	stakSize--;
	return p;

}

char Stak::top() const
{
	return stakken.at(0);
}

Stak::~Stak() {}
