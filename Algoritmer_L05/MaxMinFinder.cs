/*
 * Opgave 7.40 (Weiss)
 *
 * Opgaven:
 *   The following divide-and-conquer algorithm is proposed for finding the
 *   simultaneous maximum and minimum: If there is one item, it is the maximum
 *   and minimum, and if there are two items, then compare them and in one
 *   comparison you can find the maximum and minimum. Otherwise, split the input
 *   into two halves, divided as evenly as possibly (if N is odd, one of the two
 *   halves will have one more element than the other). Recursively find the
 *   maximum and minimum of each half, and then in two additional comparisons
 *   produce the maximum and minimum for the entire problem.
 *   a. Suppose N is a power of 2. What is the exact number of comparisons used
 *      by this algorithm?
 *   b. Suppose N is of the form 3 * 2^k. What is the exact number of
 *      comparisons used by this algorithm?
 *   c. Modify the algorithm as follows: When N is even, but not divisible by
 *      four, split the input into sizes of N/2 - 1 and N/2 + 1. What is the
 *      exact number of comparisons used by this algorithm?
 *
 * Klassen implementerer algoritmen og tæller sammenligninger, så svarene i
 * md-filen kan efterprøves.
 */
public class MaxMinFinder
{
    // true = brug den modificerede deling fra c.
    private readonly bool _modified;

    public int Comparisons { get; private set; }

    public MaxMinFinder(bool modified)
    {
        _modified = modified;
    }

    public (int min, int max) Find(int[] a)
    {
        Comparisons = 0;
        return Find(a, 0, a.Length - 1);
    }

    private (int min, int max) Find(int[] a, int left, int right)
    {
        int n = right - left + 1;

        if (n == 1)
            return (a[left], a[left]);   // 0 sammenligninger

        if (n == 2)
        {
            Comparisons++;               // 1 sammenligning
            return a[left] < a[right] ? (a[left], a[right]) : (a[right], a[left]);
        }

        // Størrelsen af venstre halvdel
        int leftSize = n / 2;
        if (_modified && n % 2 == 0 && n % 4 != 0)
            leftSize = n / 2 - 1;        // højre halvdel får så n/2 + 1

        var l = Find(a, left, left + leftSize - 1);
        var r = Find(a, left + leftSize, right);

        Comparisons += 2;                // 2 ekstra sammenligninger
        int min = l.min < r.min ? l.min : r.min;
        int max = l.max > r.max ? l.max : r.max;
        return (min, max);
    }
}
