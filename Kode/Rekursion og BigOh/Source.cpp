#include <string>
#include <iostream>
using namespace std;

int bigOh(int n)
{
	int x = 0;
	for (int i = 1; i <= n; i *= 2)
	{
		for (int j = 1; j <= n; j++)
		{
			for (int k = 1; k <= n; k++)
			{
				x++;
			}
		}
	}
	return x;

}


bool binarySearch(int arr[], int left, int right, int target)
{
	if (left > right) {
		return false; // Not found
	}

	int mid = left + (right - left) / 2;

	if (arr[mid] == target) {
		return true; // Found
	}
	else if (arr[mid] > target) {
		return binarySearch(arr, left, mid - 1, target); // Search left half
	}
	else {
		return binarySearch(arr, mid + 1, right, target); // Search right half
	}
}


bool linear(string s, char c, int l)
{
	if (l < 0)
		return false;
	if (s.at(l - 1) == c)
		return true;
	return linear(s, c, l - 1);
}


int fib(long n)
{
	//Base cases : fibonacci(0) returns 0, and fibonacci(1) returns 1.
	//For other values, it returns the sum of the two preceding Fibonacci numbers.
	//Fibonacci numbers: 0,1,1,2,3,5,8,13,21,34 etc.
	if (n <= 1)
		return n;
	return fib(n - 1) + fib(n - 2);
}

int sum(int n)
{
	if (n < 1)
		return 0;
	return (n * 2 - 1) + sum(n - 1);
}

int evenSquares(int n)
{
	if (n < 1)
		return 0;
	return 4 * n * n + evenSquares(n - 1);
}

int logTwo(int N)
{
	if (N == 1)
		return 0;
	return (1 + logTwo(N / 2));
}

int findNoOfLetters(string word, char letter, int index)
{
	if (index == word.length())
		return 0;
	if (letter == word.at(word.length() - 1 - index))
		return 1 + findNoOfLetters(word, letter, index + 1);
	return findNoOfLetters(word, letter, index + 1);
}

int f(int x)
{
	if (x == 0)
		return 0;
	else
	{
		return 2 * f(x - 1) + x * x;
	}
}




int main()
{
	//cout << sum(5) << endl;
	//cout << logTwo(1024) << endl;
	//cout << findNoOfLetters("bananas", 'a',0) << endl;
	//cout << evenSquares(3) << endl;
	//cout << fib(45);
	cout << "4     " << bigOh(4) << endl;
	cout << "16    " << bigOh(16) << endl;
	cout << "64    " << bigOh(64) << endl;
	cout << "256   " << bigOh(256) << endl;
	cout << "1024  " << bigOh(1024) << endl;
	return 0;
}
