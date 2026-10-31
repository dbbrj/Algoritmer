/*
 * Opgave 7.43 (Weiss)
 *
 * Opgaven:
 *   Suppose arrays A and B are both sorted and both contain N elements. Give an
 *   O(log N) algorithm to find the median of A ∪ B.
 *
 * A ∪ B har 2N elementer (et lige antal), så medianen er gennemsnittet af det
 * N'te og det (N+1)'te mindste element.
 */
public class MedianFinder
{
    // Idé: del A ∪ B i en venstre halvdel med N elementer og en højre halvdel
    // med N elementer. Tager vi i elementer fra starten af A, skal vi tage
    // N - i fra starten af B. Delingen er korrekt, når alt i venstre halvdel
    // er <= alt i højre halvdel, dvs.
    //     A[i-1] <= B[N-i]   og   B[N-i-1] <= A[i]
    // Vi binærsøger efter det rigtige i i intervallet [0, N].
    // Hvert trin er O(1), og intervallet halveres hver gang: O(log N).
    public double Median(int[] a, int[] b)
    {
        int n = a.Length;
        if (n == 0 || b.Length != n)
            throw new ArgumentException("A og B skal have samme længde N > 0.");

        int low = 0;
        int high = n;

        while (low <= high)
        {
            int i = (low + high) / 2; // antal fra A i venstre halvdel
            int j = n - i;            // antal fra B i venstre halvdel

            // Kanterne: tager vi 0 eller N fra et array, findes elementet ikke.
            int aLeft  = i == 0 ? int.MinValue : a[i - 1];
            int aRight = i == n ? int.MaxValue : a[i];
            int bLeft  = j == 0 ? int.MinValue : b[j - 1];
            int bRight = j == n ? int.MaxValue : b[j];

            if (aLeft > bRight)
            {
                high = i - 1;  // for mange fra A: tag færre
            }
            else if (bLeft > aRight)
            {
                low = i + 1;   // for få fra A: tag flere
            }
            else
            {
                // Korrekt deling. N'te mindste = største i venstre halvdel,
                // (N+1)'te mindste = mindste i højre halvdel.
                int leftMax = Math.Max(aLeft, bLeft);
                int rightMin = Math.Min(aRight, bRight);
                return ((long)leftMax + rightMin) / 2.0;
            }
        }

        // Nås aldrig, når A og B er sorterede.
        throw new InvalidOperationException("Arrays er ikke sorterede.");
    }
}
