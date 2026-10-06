using System;

public class Program
{
    static void RunExperiment(BallsAndBins bb, int n, int trials)
    {
        double sumSingle = 0, sumPow2 = 0;
        int overallMaxSingle = 0, overallMaxPow2 = 0;

        for (int t = 0; t < trials; t++)
        {
            int s = bb.SingleChoiceMax(n, n);
            int p = bb.PowerOfTwoChoicesMax(n, n);
            sumSingle += s;
            sumPow2 += p;
            if (s > overallMaxSingle) overallMaxSingle = s;
            if (p > overallMaxPow2) overallMaxPow2 = p;
        }

        Console.WriteLine($"\n=== N = {n} ===");
        Console.WriteLine($"Single choice:        avg-max = {sumSingle / trials:F2}, " +
                           $"overall max = {overallMaxSingle}  (teori: {BallsAndBins.TheorySingle(n):F2})");
        Console.WriteLine($"Power of two choices: avg-max = {sumPow2 / trials:F2}, " +
                           $"overall max = {overallMaxPow2}  (teori: {BallsAndBins.TheoryPow2(n):F2})");
    }

    static void RunCuckooDemo()
    {
        Console.WriteLine("\n=== Cuckoo hashing (øvelse 4) ===");

        var cuckoo = new CuckooHashTable(capacity: 11);
        int[] keys = { 4, 15, 26, 37, 48, 8, 19, 30, 5, 16, 27, 9 };

        foreach (int key in keys)
        {
            bool ok = cuckoo.Insert(key);
            Console.WriteLine($"Insert({key}) -> {(ok ? "OK" : "fejlede")}, " +
                               $"loadfaktor = {cuckoo.LoadFactor:F2}, kapacitet = {cuckoo.Capacity}");
        }

        cuckoo.Print();

        // Lookup-test
        int[] lookups = { 26, 99 };
        foreach (int key in lookups)
        {
            Console.WriteLine($"Contains({key}) -> {cuckoo.Contains(key)}");
        }
    }

    static void Main()
    {
        var bb = new BallsAndBins();

        // Opgave 1 og 2: single choice vs. power of two choices
        RunExperiment(bb, 10007, 20);
        RunExperiment(bb, 32749, 20);

        // Opgave 5: test af Theorem 5.2 (M = N^2)
        Console.WriteLine("\n=== Theorem 5.2: N bolde i M = N^2 beholdere ===");
        int[] testValues = { 10, 20, 50, 100, 200 };
        foreach (int n in testValues)
        {
            double p = bb.ProbNoCollision(n, 3000);
            Console.WriteLine($"N={n,4}, M=N^2={n * n,7}  ->  P(ingen kollision) ~= {p:F3}");
        }

        // Øvelse 4: Cuckoo hashing
        RunCuckooDemo();
    }
}
