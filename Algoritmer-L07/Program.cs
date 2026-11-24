/*
 * Testprogram til øvelserne i lektion 7 (træer del 1).
 * Selve metoderne står i BinarySearchTree.cs, forklaringerne i md-filerne.
 *
 * Testtræ 1 er underviserens eksempel fra den udleverede Main:
 *   indsæt 7, 4, 28, 3, 55, 2, 51, 60, 1, 48, 58, 69, 40, 57, 35 og slet 55 og 3.
 * Testtræ 2 har samme form som slide-eksemplet med internal path length 19.
 */
public class Program
{
    public static void Main()
    {
        BinarySearchTree t = TeacherTree();

        Console.WriteLine("=== Øvelse 1: traverseringer (underviserens træ) ===");
        Check("Pre-order  ", t.PreOrder(), new[] { 7, 4, 2, 1, 28, 57, 51, 48, 40, 35, 60, 58, 69 });
        Check("In-order   ", t.InOrder(), new[] { 1, 2, 4, 7, 28, 35, 40, 48, 51, 57, 58, 60, 69 });
        Check("Post-order ", t.PostOrder(), new[] { 1, 2, 4, 35, 40, 48, 51, 58, 69, 60, 57, 28, 7 });
        Check("Level order", t.LevelOrder(), new[] { 7, 4, 28, 2, 57, 1, 51, 60, 48, 58, 69, 40, 35 });

        Console.WriteLine();
        Console.WriteLine("=== Øvelse 2: internal path length ===");
        Check("Underviserens træ", t.InternalPathLength(), 38);
        Check("Slide-eksemplet   ", SlideTree().InternalPathLength(), 19);
        Check("Tomt træ          ", new BinarySearchTree().InternalPathLength(), 0);

        Console.WriteLine();
        Console.WriteLine("=== Øvelse 3: opgave 4.31 (underviserens træ) ===");
        Check("a. Antal noder      ", BinarySearchTree.CountNodes(t.Root), 13);
        Check("b. Antal blade      ", BinarySearchTree.CountLeaves(t.Root), 4);
        Check("c. Antal fulde noder", BinarySearchTree.CountFullNodes(t.Root), 3);

        Console.WriteLine();
        Console.WriteLine("=== Øvelse 4: perfekt binært træ med højden h ===");
        for (int h = 1; h <= 4; h++)
        {
            BinarySearchTree p = BinarySearchTree.CreatePerfectTree(h);
            int n = (1 << (h + 1)) - 1;
            int leaves = 1 << h;
            int ipl = (h - 1) * (1 << (h + 1)) + 2; // sum af d * 2^d for d = 0..h

            Console.WriteLine($"h = {h}: level order = {string.Join(", ", p.LevelOrder())}");
            Check($"  højde       ", p.Height(), h);
            Check($"  noder       ", BinarySearchTree.CountNodes(p.Root), n);
            Check($"  blade       ", BinarySearchTree.CountLeaves(p.Root), leaves);
            Check($"  fulde noder ", BinarySearchTree.CountFullNodes(p.Root), n - leaves);
            Check($"  IPL         ", p.InternalPathLength(), ipl);
            Check($"  sorteret    ", p.InOrder(), Enumerable.Range(1, n).ToArray());
        }
    }

    private static BinarySearchTree TeacherTree()
    {
        var t = new BinarySearchTree();
        foreach (int x in new[] { 7, 4, 28, 3, 55, 2, 51, 60, 1, 48, 58, 69, 40, 57, 35 })
            t.Insert(x);

        t.Remove(55);
        t.Remove(3);
        return t;
    }

    // Samme form som træet A-J på sliden: 1 rod, 2 noder i dybde 1,
    // 4 i dybde 2 og 3 i dybde 3 -> 2*1 + 4*2 + 3*3 = 19
    private static BinarySearchTree SlideTree()
    {
        var t = new BinarySearchTree();
        foreach (int x in new[] { 8, 4, 12, 2, 6, 10, 14, 1, 3, 5 })
            t.Insert(x);
        return t;
    }

    private static void Check(string label, int actual, int expected)
    {
        Console.WriteLine($"{(actual == expected ? "OK  " : "FEJL")} {label}: {actual} (forventet {expected})");
    }

    private static void Check(string label, List<int> actual, int[] expected)
    {
        bool ok = actual.SequenceEqual(expected);
        Console.WriteLine($"{(ok ? "OK  " : "FEJL")} {label}: {string.Join(", ", actual)}");
        if (!ok)
            Console.WriteLine($"     forventet: {string.Join(", ", expected)}");
    }
}
