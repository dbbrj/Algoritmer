#pragma once
#ifndef STAK_H
#define STAK_H
#include<vector>
#include<string>
using namespace std;

class Stak
{
public:
	Stak();
	Stak(string);
	bool isEmpty() const;
	string getNavn() const;
	int getStakSize() const;
	void push(char p);			// Tilføjer et element øverst i stakken
	char pop();					// Fjerner det øverste element fra stakken
	char top() const;			// Viser øverste element uden at fjerne det
	~Stak();
private:
	string navn;
	vector<char> stakken;
	int stakSize;

};

#endif

