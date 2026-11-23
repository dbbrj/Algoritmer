using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        TestSum();
        Console.WriteLine();
        TestForekomster();
        Console.WriteLine();
        TestOpgave2();
        Console.WriteLine();
        TestBjerge();
    }

    private static void TestSum()
    {
        Console.WriteLine("--- Opgave 1.1: Sum ---");
        var s = new ArraySum();
        var tests = new (int[] arr, int forventet)[]
        {
            (new[] { 15, 17, 6, 21, 8 }, 67),
            (new int[0], 0),
            (new[] { -5, 5 }, 0),
        };
        foreach (var (arr, forventet) in tests)
        {
            int svar = s.Sum(arr, 0);
            Console.WriteLine($"{{{string.Join(",", arr)}}} -> {svar} {(svar == forventet ? "OK" : "FEJL")}");
        }
    }

    private static void TestForekomster()
    {
        Console.WriteLine("--- Opgave 1.2: Forekomster ---");
        var f = new ForekomstTæller();
        int[] arr = { 12, 4, 88, 19, 6, 28, 4, 39, 22 };
        foreach (var (tal, forventet) in new[] { (4, 2), (37, 0), (22, 1) })
        {
            int svar = f.Antal(arr, tal, 0);
            Console.WriteLine($"{tal} forekommer {svar} gange {(svar == forventet ? "OK" : "FEJL")}");
        }
    }

    // Sammenligner antallet af x++ med N^2,5 og N^2,5 · log N.
    private static void TestOpgave2()
    {
        Console.WriteLine("--- Opgave 2: tidskompleksitet ---");
        Console.WriteLine($"{"N",6} {"x",16} {"x/N^2,5",10} {"x/(N^2,5 log N)",16} {"ms",8}");
        foreach (int n in new[] { 16, 64, 256, 1024 })
        {
            var sw = Stopwatch.StartNew();
            long x = Opgave2Algoritme.MyMethod(n);
            sw.Stop();
            double n25 = Math.Pow(n, 2.5);
            Console.WriteLine($"{n,6} {x,16} {x / n25,10:F2} {x / (n25 * Math.Log2(n)),16:F2} {sw.ElapsedMilliseconds,8}");
        }
    }

    private static void TestBjerge()
    {
        Console.WriteLine("--- Opgave 3: bjerge ---");
        var b = new BjergFinder();
        var tests = new (int[] arr, int antal, int længste)[]
        {
            (new[] { 12, 34, 56, 1, 3, 19, 12, 55, 78, 34, 48, 6, 53, 19, 20, 7, 11 }, 0, 0),
            (new[] { 6, 4, 11, 17, 19, 9, 5, 2, 99, 11, 66, 4, 14, 51, 6, 7, 88, 76 }, 1, 7),
            (new[] { 13, 6, 67, 55, 44, 55, 89, 95, 69, 74, 36, 19, 6, 71, 44, 61, 16 }, 2, 5),
            (new[] { 17, 29, 12, 6, 7, 12, 10, 8, 25, 23, 38, 17, 19 }, 2, 5),
            (new[] { 12, 17, 14, 11 }, 1, 4),
            (new[] { 1, 2, 2, 1, 0 }, 0, 0),
            (new int[0], 0, 0),
        };
        foreach (var (arr, antal, længste) in tests)
        {
            bool har = b.HarBjerg(arr);
            int a = b.AntalBjerge(arr);
            int l = b.LængsteBjerg(arr);
            bool ok = har == (antal > 0) && a == antal && l == længste;
            Console.WriteLine($"{{{string.Join(",", arr)}}} -> har: {har}, antal: {a}, længste: {l} {(ok ? "OK" : "FEJL")}");
        }
    }
}
