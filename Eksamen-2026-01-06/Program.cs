using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        TestFakultet();
        Console.WriteLine();
        TestIsSorted();
        Console.WriteLine();
        TestOpgave2();
        Console.WriteLine();
        TestPalindromer();
    }

    private static void TestFakultet()
    {
        Console.WriteLine("--- Opgave 1.1: N! ---");
        var f = new Fakultet();
        foreach (int n in new[] { 0, 1, 5, 10, 20 })
            Console.WriteLine($"{n}! = {f.Beregn(n)}");
    }

    private static void TestIsSorted()
    {
        Console.WriteLine("--- Opgave 1.2: IsSorted ---");
        var s = new SorteringsTjek();
        var tests = new (int[] arr, bool forventet)[]
        {
            (new[] { 23, 44, 61, 90, 112 }, true),
            (new[] { 23, 44, 31, 90, 112 }, false),
            (new int[0], true),
            (new[] { 7 }, true),
        };
        foreach (var (arr, forventet) in tests)
        {
            bool svar = s.IsSorted(arr, 0);
            Console.WriteLine($"{{{string.Join(",", arr)}}} -> {svar} {(svar == forventet ? "OK" : "FEJL")}");
        }
    }

    // Kører algoritmen for stigende N og sammenligner antallet af x++
    // med N log N og N log² N. Den kvotient, der går mod en konstant,
    // afslører væksten.
    private static void TestOpgave2()
    {
        Console.WriteLine("--- Opgave 2: tidskompleksitet ---");
        Console.WriteLine($"{"N",8} {"x",12} {"x/(N log N)",12} {"x/(N log² N)",13} {"ms",6}");
        for (int p = 8; p <= 18; p += 2)
        {
            long n = 1L << p;
            var sw = Stopwatch.StartNew();
            long x = Opgave2Algoritme.MyMethod(n);
            sw.Stop();
            double lg = Math.Log2(n);
            Console.WriteLine($"{n,8} {x,12} {x / (n * lg),12:F3} {x / (n * lg * lg),13:F3} {sw.ElapsedMilliseconds,6}");
        }
    }

    private static void TestPalindromer()
    {
        Console.WriteLine("--- Opgave 3: palindromer ---");
        var p = new PalindromTæller();
        var tests = new (string tekst, int forventet)[]
        {
            ("Min hat, den har tre buler. Tre buler har min hat!", 0),
            ("Anna betalte sine regninger", 1),
            ("Olfert+ roede en tur i sin kajak", -1),
            ("otto mellem kajak", 3),
            ("Otto", 0),
            ("aa bob, wow!", 2),
            ("", 0),
        };
        foreach (var (tekst, forventet) in tests)
        {
            int svar = p.AntalPalindromer(tekst);
            Console.WriteLine($"\"{tekst}\" -> {svar} {(svar == forventet ? "OK" : "FEJL")}");
        }
    }
}
