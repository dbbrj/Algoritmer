using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        TestOpgave1();
        Console.WriteLine();
        TestOpgave2();
        Console.WriteLine();
        TestOpgave3();
        Console.WriteLine();
        TestOpgave4();
        Console.WriteLine();
        TestOpgave5();
        Console.WriteLine();
        TestOpgave6();
        Console.WriteLine();
        TestOpgave7();
    }

    private static void TestOpgave1()
    {
        Console.WriteLine("--- Opgave 1: sum af to tal ---");
        var t = new ToTalSum();
        int[] arr = { 1, 3, 5, 8, 12, 15 };
        var tests = new (int x, bool forventet)[]
        {
            (20, true),   // 5 + 15
            (4, true),    // 1 + 3
            (27, true),   // 12 + 15
            (10, false),  // 5 + 5 er ikke tilladt
            (2, false),   // 1 + 1 er ikke tilladt
            (30, false),  // 15 + 15 er ikke tilladt
            (7, false),
        };
        foreach (var (x, forventet) in tests)
        {
            bool v1 = t.SumAfToTalLigParameterV1(arr, arr.Length, x);
            bool v2 = t.SumAfToTalLigParameterV2(arr, arr.Length, x);
            bool ok = v1 == forventet && v2 == forventet;
            Console.WriteLine($"X={x,3}: V1={v1,-5} V2={v2,-5} {(ok ? "OK" : "FEJL")}");
        }
        bool tom = t.SumAfToTalLigParameterV2(new int[0], 0, 5) || t.SumAfToTalLigParameterV1(new int[0], 0, 5);
        Console.WriteLine($"Tomt array: {(tom ? "FEJL" : "OK")}");

        // Tilfældig sammenligning af de to versioner.
        var rnd = new Random(1);
        bool alleEns = true;
        for (int r = 0; r < 2000; r++)
        {
            int[] a = Enumerable.Range(1, 60).OrderBy(_ => rnd.Next()).Take(rnd.Next(0, 20)).OrderBy(v => v).ToArray();
            int x = rnd.Next(1, 120);
            if (t.SumAfToTalLigParameterV1(a, a.Length, x) != t.SumAfToTalLigParameterV2(a, a.Length, x))
                alleEns = false;
        }
        Console.WriteLine($"V1 og V2 giver samme svar på 2000 tilfældige tests: {(alleEns ? "OK" : "FEJL")}");
    }

    // Sammenligner antallet af x++ med N · log10 N.
    private static void TestOpgave2()
    {
        Console.WriteLine("--- Opgave 2: tidskompleksitet ---");
        Console.WriteLine($"{"N",7} {"x",14} {"x/N",10} {"x/(N·(⌊log10 N⌋+1))",22} {"ms",7}");
        foreach (int n in new[] { 9, 10, 99, 100, 1000, 10000 })
        {
            var sw = Stopwatch.StartNew();
            long x = Opgave2Algoritme.MyM(n);
            sw.Stop();
            int cifre = (int)Math.Floor(Math.Log10(n)) + 1;
            Console.WriteLine($"{n,7} {x,14} {x / (double)n,10:F0} {x / ((double)n * cifre),22:F0} {sw.ElapsedMilliseconds,7}");
        }
    }

    private static void TestOpgave3()
    {
        Console.WriteLine("--- Opgave 3: antal vokaler ---");
        var v = new VokalTæller();
        var tests = new (string str, int forventet)[]
        {
            ("stationsbygninger", 6),
            ("", 0),
            ("bcd", 0),
            ("aeiouy", 6),
        };
        foreach (var (str, forventet) in tests)
        {
            int svar = v.AntalVokaler(str, str.Length - 1);
            Console.WriteLine($"\"{str}\" -> {svar} {(svar == forventet ? "OK" : "FEJL")}");
        }
    }

    private static void TestOpgave4()
    {
        Console.WriteLine("--- Opgave 4: tællesortering ---");
        var s = new TælleSortering();

        var rnd = new Random(42);
        int[] arr = new int[100];
        for (int i = 0; i < arr.Length; i++)
            arr[i] = rnd.Next(1, 201);

        int[] sorteret = s.Sorteret(arr);
        int[] kontrol = (int[])arr.Clone();
        Array.Sort(kontrol); // kun til kontrol af resultatet
        Console.WriteLine($"100 tilfældige tal i 1..200: {(sorteret.SequenceEqual(kontrol) ? "OK" : "FEJL")}");

        Console.Write("Eksempel {7, 200, 1, 7, 55, 3}: ");
        s.MinSortering(new[] { 7, 200, 1, 7, 55, 3 });
    }

    // Tjekker det rettede array fra "Opgave 5 prioritetskø.md".
    private static void TestOpgave5()
    {
        Console.WriteLine("--- Opgave 5: prioritetskø ---");
        int[] original = { 0, 9, 23, 106, 10, 36, 38, 98, 84, 12, 14, 50, 55, 35, 68 };
        int[] rettet = { 0, 9, 23, 20, 24, 36, 38, 98, 84, 25, 37, 50, 55, 39, 99 };

        Console.WriteLine("Brud på heap-order i det oprindelige array (forælder > barn):");
        for (int i = 2; i < original.Length; i++)
        {
            if (original[i / 2] > original[i])
                Console.WriteLine($"  indeks {i / 2} ({original[i / 2]}) > indeks {i} ({original[i]})");
        }

        bool heapOrder = true;
        for (int i = 2; i < rettet.Length; i++)
            if (rettet[i / 2] >= rettet[i]) heapOrder = false;
        bool ingenDubletter = rettet.Skip(1).Distinct().Count() == rettet.Length - 1;
        Console.WriteLine($"Rettet array {{{string.Join(",", rettet)}}}: heap-order {(heapOrder ? "OK" : "FEJL")}, ingen dubletter {(ingenDubletter ? "OK" : "FEJL")}");
    }

    private static void TestOpgave6()
    {
        Console.WriteLine("--- Opgave 6: hashtabeller ---");

        Console.WriteLine("Tabel 1 (linear probing):");
        var t1 = new HashTabel(16, kvadratisk: false);
        foreach (char c in "DEMOCRAT")
            Console.WriteLine("  " + t1.Indsæt(c));
        Console.WriteLine(t1);

        Console.WriteLine("Tabel 2 (quadratic probing):");
        var t2 = new HashTabel(16, kvadratisk: true);
        foreach (char c in "REPUBLICAN")
            Console.WriteLine("  " + t2.Indsæt(c));
        Console.WriteLine(t2);
    }

    private static void TestOpgave7()
    {
        Console.WriteLine("--- Opgave 7: Dijkstra ---");
        var graf = Graf.Opgave7Graf();
        var log = new List<string>();
        var tabel = new Dijkstra().Kør(graf, 's', log);
        foreach (string linje in log)
            Console.WriteLine("  " + linje);
        Console.WriteLine($"{"v",2} {"known",6} {"dv",4} {"pv",3}");
        foreach (char v in "sabcdefgh")
        {
            var r = tabel[v];
            Console.WriteLine($"{r.V,2} {r.Known,6} {r.Dv,4} {(r.Pv?.ToString() ?? "-"),3}");
        }

        Console.WriteLine();
        Console.WriteLine("--- Opgave 7: Kruskal (uorienteret) ---");
        var mstLog = new List<string>();
        var mst = new Kruskal().Kør(graf, mstLog);
        foreach (string linje in mstLog)
            Console.WriteLine("  " + linje);
        Console.WriteLine($"Antal kanter: {mst.Count}, samlet vægt: {mst.Sum(k => k.Vægt)}");
    }
}
