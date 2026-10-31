
class ProgramSolution
{
    static bool BinarySearch(int[] array, int target)
    {
        return BinarySearch(array, 0, array.Length - 1, target);
    }

    static bool BinarySearch(int[] arr, int left, int right, int target)
    {
        if (left > right)
            return false; // Not found

        int mid = left + (right - left) / 2;

        if (arr[mid] == target)
            return true; // Found
        
        if (arr[mid] > target)
            return BinarySearch(arr, left, mid - 1, target); // Search left half
        
        return BinarySearch(arr, mid + 1, right, target);    // Search right half
     }

    static bool Linear(string s, char c)
    {
        return Linear(s, c, s.Length - 1);
    }
    static bool Linear(string s, char c, int l)
    {
        if (l < 1)
            return false;
        if (s[l] == c)
            return true;
        return Linear(s, c, l - 1);
    }
    static void Main()
    {
        int[] Numbers = { 3, 7, 12, 18, 25, 31, 42, 56 };

        Console.WriteLine(BinarySearch(Numbers, 27));

        string Text = "This";
         
        Console.WriteLine(Linear(Text, 's'));
    }
}


