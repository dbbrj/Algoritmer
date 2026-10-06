public class Program
{
    public static void Main(string[] args)
    {
        // Test logTwo method
        Console.WriteLine(logTwo(32)); // Output: 5
        Console.WriteLine(logTwo(4096)); // Output: 12
        // Test countCharOccurrences method
        Console.WriteLine(countCharOccurrences("banana", 'a')); // Output: 3
        // Test sum method
        Console.WriteLine(sum(5)); // Output: 25 (1 + 3 + 5 + 7 + 9)
        // Test evenSquares method
        Console.WriteLine(evenSquares(3)); // Output: 56 (2^2 + 4^2 + 6^2)
        // Test fib method
        Console.WriteLine(fib(6)); // Output: 8 (0, 1, 1, 2, 3, 5, 8)
        // Test linear method
        Console.WriteLine(linear("hello", 'e', 5)); // Output: True
        Console.WriteLine(linear("hello", 'a', 5)); // Output: False
        // Test binarySearch method
        int[] arr = { 1, 2, 3, 4, 5 };
        Console.WriteLine(binarySearch(arr, 3)); // Output: True
        Console.WriteLine(binarySearch(arr, 6)); // Output: False
    }


    // Opgave 1
    /*Skriv en rekursiv algoritme med følgendesignatur:
    int logTwo(int N)
    Algoritmen returnerer totals-logaritmen af N, og det er en precondition, at N er et naturligttalogenpotensaf 2.
    Kaldt med N = 32 returneres 5, og med N = 4096 returneres 12.*/

    public static int logTwo(int N)
    {
        if (N == 1)
            return 0;
        else
            return 1 + logTwo(N / 2);
    }

    // Opgave 2
    /* Skriv en rekursiv metode, som har en string og en char som parameter og returnerer det antal gange char forekommer i string. 
     Kaldt med “banana” returneres 3*/
    public static int countCharOccurrences(string str, char c)
    {
        if (str.Length == 0)
            return 0;
        else
            return (str[0] == c ? 1 : 0) + countCharOccurrences(str.Substring(1), c);
    }

    // Opgave 3
    /* Løs følgende opgaver med rekursion:*/

    // int sum(int n);     //returns the sum of the first odd natural numbers
    public static int sum(int n)
    {
        if (n <= 0)
            return 0;
        else
            return (2 * n - 1) + sum(n - 1);
    }

    // int evenSquares(int n); //returns the sum of the first n even numbers’ squares
    public static int evenSquares(int n)
    {
        if (n <= 0)
            return 0;
        else
            return (2 * n) * (2 * n) + evenSquares(n - 1);
    }


    // intfib(intn);   //returns the nth Fibonacci number
    public static int fib(int n)
    {
        if (n <= 1)
            return n;
        else
            return fib(n - 1) + fib(n - 2);
    }

    // bool linear(string s, char c, intl);    //returns true if string s with the length l contains char c, otherwise false.
    public static bool linear(string s, char c, int l)
    {
        if (l <= 0)
            return false;
        else
            return (s[l - 1] == c) || linear(s, c, l - 1);
    }


    // bool binarySearch(int arr[], int value) //returns true if value is in arr, otherwise false the elements in arrare sorted
    public static bool binarySearch(int[] arr, int value)
    {
        if (arr.Length == 0)
            return false;
        else
            return binarySearchHelper(arr, value, 0, arr.Length - 1);
    }
    private static bool binarySearchHelper(int[] arr, int value, int low, int high)
    {
        if (low > high)
            return false;
        else
        {
            int mid = (low + high) / 2;
            if (arr[mid] == value)
                return true;
            else if (arr[mid] > value)
                return binarySearchHelper(arr, value, low, mid - 1);
            else
                return binarySearchHelper(arr, value, mid + 1, high);
        }
    }
}
