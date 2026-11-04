public class Program
{
    public static void Main()
    {
        TestTeacherExample();
        Console.WriteLine();
        TestCorrectness();
        Console.WriteLine();
        MeasureAverageCase();
        Console.WriteLine();
        MeasureWorstCase();
    }

    // Eksemplet fra opgaveteksten: det 6.-mindste element skal være 8.
    private static void TestTeacherExample()
    {
        Console.WriteLine("--- QuickSelect: eksemplet fra opgaven ---");
        int[] a = { 8, 12, 6, 33, 9, 4, 7, 2, 5, 17, 23, 11 };
        int result = new QuickSelect().Select(a, 6);
        Console.WriteLine($"{(result == 8 ? "OK  " : "FEJL")} - QuickSelect(a, 6) = {result} (forventet 8)");
    }

    // Sammenlign med "sortér og slå op" for alle positioner i mange tilfældige tabeller,
    // også tabeller med dubletter.
    private static void TestCorrectness()
    {
        Console.WriteLine("--- QuickSelect: sammenlignet med sortering ---");
        var rnd = new Random(42);
        var qs = new QuickSelect();
        int tests = 0, passed = 0;

        for (int run = 0; run < 200; run++)
        {
            int n = rnd.Next(1, 40);
            int maxValue = run % 2 == 0 ? 1000 : 5; // hver anden tabel får mange dubletter
            int[] original = new int[n];
            for (int i = 0; i < n; i++) original[i] = rnd.Next(maxValue);

            int[] sorted = (int[])original.Clone();
            Array.Sort(sorted);

            for (int pos = 1; pos <= n; pos++)
            {
                int[] copy = (int[])original.Clone(); // Select ændrer tabellen
                tests++;
                if (qs.Select(copy, pos) == sorted[pos - 1]) passed++;
            }
        }
        Console.WriteLine($"{passed}/{tests} test bestået.");
    }

    // Average case: tilfældige tabeller og tilfældig position.
    // Er køretiden O(N), skal sammenligninger/N holde sig konstant, når N vokser.
    private static void MeasureAverageCase()
    {
        Console.WriteLine("--- Average case: sammenligninger / N (tilfældigt input) ---");
        var rnd = new Random(7);
        var qs = new QuickSelect();
        const int runs = 20;

        foreach (int n in new[] { 1_000, 10_000, 100_000, 1_000_000 })
        {
            qs.ResetCount();
            for (int r = 0; r < runs; r++)
            {
                int[] a = new int[n];
                for (int i = 0; i < n; i++) a[i] = rnd.Next();
                qs.Select(a, rnd.Next(1, n + 1));
            }
            double perRun = (double)qs.Comparisons / runs;
            Console.WriteLine($"N = {n,9:N0}: {perRun,14:N0} sammenligninger, pr. element {perRun / n:F2}");
        }
    }

    // Worst case: sorteret input og det mindste element.
    // Pivot (længst til højre) er så altid det største, og hver partitionering
    // fjerner kun ét element: T(N) = T(N-1) + cN -> N(N-1)/2 sammenligninger.
    private static void MeasureWorstCase()
    {
        Console.WriteLine("--- Worst case: sorteret input, position 1 ---");
        var qs = new QuickSelect();

        foreach (int n in new[] { 1_000, 2_000, 4_000 })
        {
            int[] a = new int[n];
            for (int i = 0; i < n; i++) a[i] = i;

            qs.ResetCount();
            qs.Select(a, 1);
            long expected = (long)n * (n - 1) / 2;
            Console.WriteLine($"N = {n,5:N0}: {qs.Comparisons,10:N0} sammenligninger " +
                              $"(N(N-1)/2 = {expected:N0}), pr. N^2 {(double)qs.Comparisons / n / n:F2}");
        }
    }
}
