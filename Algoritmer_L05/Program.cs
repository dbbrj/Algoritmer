public class Program
{
    public static void Main()
    {
        TestTwoSum();
        Console.WriteLine();
        TestAnagram();
        Console.WriteLine();
        TestBooleanPartition();
        Console.WriteLine();
        TestColinear();
        Console.WriteLine();
        TestMedian();
        Console.WriteLine();
        TestMaxMin();
    }

    private static void TestBooleanPartition()
    {
        Console.WriteLine("--- 7.44: false før true ---");
        var partition = new BooleanPartition();

        var testCases = new bool[][]
        {
            new[] { true, false, true, false, false, true },
            new[] { true, true, true },
            new[] { false, false },
            new[] { true, false },
            new bool[0],
        };

        int passed = 0;
        foreach (bool[] a in testCases)
        {
            string before = Show(a);
            int trues = a.Count(x => x);
            partition.Rearrange(a);

            // Korrekt, hvis de første N - trues er false og resten true
            bool ok = true;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != (i >= a.Length - trues)) ok = false;
            if (ok) passed++;

            Console.WriteLine($"{(ok ? "OK  " : "FEJL")} - {before} -> {Show(a)}");
        }
        Console.WriteLine($"{passed}/{testCases.Length} test bestået.");
    }

    private static string Show(bool[] a) =>
        "[" + string.Join(" ", a.Select(x => x ? "T" : "F")) + "]";

    private static void TestColinear()
    {
        Console.WriteLine("--- 7.38: grupper med 4 eller flere punkter på linje ---");
        var finder = new ColinearFinder();

        Point[] points =
        {
            // 5 punkter på y = x
            new(0, 0), new(1, 1), new(2, 2), new(3, 3), new(5, 5),
            // 4 punkter på den lodrette linje x = 4
            new(4, 0), new(4, 2), new(4, 7), new(4, 9),
            // y = -2x + 10 (hældning -2). (4, 2) ovenfor og (0, 10) nedenfor
            // ligger også på linjen, så gruppen får 6 punkter.
            new(1, 8), new(2, 6), new(3, 4), new(6, -2),
            // 3 punkter på y = 10 (kun 3, skal IKKE med)
            new(0, 10), new(7, 10), new(9, 10),
        };
        // Forventet: y = x (5 punkter), x = 4 (4 punkter), y = -2x + 10 (6 punkter)

        var groups = finder.FindGroups(points);
        foreach (var g in groups)
            Console.WriteLine("  " + string.Join(" ", g));

        bool ok = groups.Count == 3;
        Console.WriteLine($"{(ok ? "OK  " : "FEJL")} - fandt {groups.Count} grupper (forventet 3)");
    }

    private static void TestMedian()
    {
        Console.WriteLine("--- 7.43: medianen af A ∪ B ---");
        var finder = new MedianFinder();

        var testCases = new (int[] a, int[] b, double expected)[]
        {
            (new[] { 1, 3, 5, 7 }, new[] { 2, 4, 6, 8 }, 4.5),   // flettet: 1..8
            (new[] { 1, 2, 3, 4 }, new[] { 5, 6, 7, 8 }, 4.5),   // alt i A er mindst
            (new[] { 5, 6, 7, 8 }, new[] { 1, 2, 3, 4 }, 4.5),   // alt i B er mindst
            (new[] { 1, 2, 2, 9 }, new[] { 2, 2, 3, 10 }, 2.0),  // dubletter
            (new[] { 3 }, new[] { 8 }, 5.5),                     // N = 1
        };

        int passed = 0;
        foreach (var (a, b, expected) in testCases)
        {
            double m = finder.Median(a, b);
            bool ok = m == expected;
            if (ok) passed++;
            Console.WriteLine($"{(ok ? "OK  " : "FEJL")} - A = [{string.Join(", ", a)}], B = [{string.Join(", ", b)}]: median = {m} (forventet {expected})");
        }
        Console.WriteLine($"{passed}/{testCases.Length} test bestået.");
    }

    private static void TestMaxMin()
    {
        Console.WriteLine("--- 7.40: antal sammenligninger ---");
        var original = new MaxMinFinder(modified: false);
        var modified = new MaxMinFinder(modified: true);
        int passed = 0, total = 0;

        // a. N = 2^k: 3N/2 - 2
        for (int n = 2; n <= 1024; n *= 2)
            passed += Check("a", original, n, 3 * n / 2 - 2, ref total);

        // b. N = 3 * 2^k: 5N/3 - 2
        for (int n = 3; n <= 3 * 512; n *= 2)
            passed += Check("b", original, n, 5 * n / 3 - 2, ref total);

        // c. Modificeret, N = 3 * 2^k med k >= 1 (N lige): 3N/2 - 2
        for (int n = 6; n <= 3 * 512; n *= 2)
            passed += Check("c", modified, n, 3 * n / 2 - 2, ref total);

        Console.WriteLine($"{passed}/{total} test bestået.");
    }

    private static int Check(string part, MaxMinFinder finder, int n, int expected, ref int total)
    {
        total++;
        int[] a = Enumerable.Range(0, n).Select(i => (i * 37) % (n + 11)).ToArray();
        var (min, max) = finder.Find(a);
        bool ok = finder.Comparisons == expected && min == a.Min() && max == a.Max();
        Console.WriteLine($"{(ok ? "OK  " : "FEJL")} - {part}) N = {n,4}: {finder.Comparisons,5} sammenligninger (forventet {expected})");
        return ok ? 1 : 0;
    }

    private static void TestTwoSum()
    {
        Console.WriteLine("--- Øvelse 1: to tal med summen X ---");
        var twoSum = new TwoSum();
        int[] tabel = { 1, 3, 4, 8, 11, 15, 20 };
        Console.WriteLine($"Tabel: [{string.Join(", ", tabel)}]");

        // (X, forventet resultat)
        var testCases = new (int x, bool expected)[]
        {
            (12, true),   // 1 + 11, 4 + 8
            (35, true),   // 15 + 20
            (4, true),    // 1 + 3
            (2, false),   // 1 + 1 kræver samme element to gange
            (40, false),  // større end de to største tilsammen
            (10, false),  // intet par giver 10
        };

        int passed = 0;
        foreach (var (x, expected) in testCases)
        {
            bool q = twoSum.HasPairQuadratic(tabel, x);
            bool l = twoSum.HasPairLinear(tabel, x);
            bool ok = q == expected && l == expected;
            if (ok) passed++;
            string label = ok ? "OK  " : "FEJL";
            Console.WriteLine($"{label} - X = {x,3}: kvadratisk = {q,-5} lineær = {l,-5} (forventet {expected})");
        }

        // Kanttilfælde: tom tabel og tabel med ét element
        bool tom = twoSum.HasPairLinear(new int[0], 5) || twoSum.HasPairQuadratic(new int[0], 5);
        bool et = twoSum.HasPairLinear(new[] { 5 }, 10) || twoSum.HasPairQuadratic(new[] { 5 }, 10);
        Console.WriteLine($"{(!tom ? "OK  " : "FEJL")} - tom tabel giver false");
        Console.WriteLine($"{(!et ? "OK  " : "FEJL")} - tabel med ét element giver false");
        if (!tom) passed++;
        if (!et) passed++;

        Console.WriteLine($"{passed}/{testCases.Length + 2} test bestået.");
    }

    private static void TestAnagram()
    {
        Console.WriteLine("--- Øvelse 2: anagrammer ---");
        var checker = new AnagramChecker();

        var testCases = new (string a, string b, bool expected)[]
        {
            ("stale", "least", true),
            ("listen", "silent", true),
            ("aab", "abb", false),    // samme bogstaver, forskelligt antal
            ("abc", "abcd", false),   // forskellig længde
            ("rød", "ødr", true),     // danske bogstaver
            ("", "", true),           // to tomme strenge
            ("Stale", "least", false) // store og små bogstaver er forskellige tegn
        };

        int passed = 0;
        foreach (var (a, b, expected) in testCases)
        {
            bool s = checker.IsAnagramSort(a, b);
            bool c = checker.IsAnagramCount(a, b);
            bool ok = s == expected && c == expected;
            if (ok) passed++;
            string label = ok ? "OK  " : "FEJL";
            Console.WriteLine($"{label} - (\"{a}\", \"{b}\"): sortering = {s,-5} optælling = {c,-5} (forventet {expected})");
        }

        Console.WriteLine($"{passed}/{testCases.Length} test bestået.");
    }
}
