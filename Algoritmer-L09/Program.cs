/*
 * Testprogram til øvelserne i lektion 9 (og 10):
 *   Øvelse 1: Opgave i aktivitetsplanlægning - kritisk vej (Aktivitet.cs, Aktivitetsplan.cs)
 *   Øvelse 2: Egen graf (Vertex.cs, Edge.cs, Graph.cs) og opgave 9.13 a og b
 *
 * Datafilerne kopieres til output-mappen (se .csproj). Man kan også give et
 * filnavn som argument: dotnet run -- minfil.txt
 */
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            new Aktivitetsplan(args[0]).Udskriv();
            return;
        }

        AktivitetsplanTest();
        Console.WriteLine();
        BipartitTest();
        Console.WriteLine();
        MatchingTest();
    }

    // ---------------------------------------------------------------------
    // Øvelse 1: aktivitetsplanlægning
    // ---------------------------------------------------------------------
    private static void AktivitetsplanTest()
    {
        Console.WriteLine("=== Øvelse 1: aktivitetsplanlægning ===");

        // filnavn, forventet antal, gennemsnit, kritisk vej, tasks
        var tests = new (string fil, int antal, double gns, int varighed, string tasks)[]
        {
            ("data.txt",  10, 3.7,  26, "B, D, E, H, J"), // opgavens data
            ("data2.txt",  6, 4.5,  14, "M, A/K, C"),     // tasks ikke i alfabetisk orden + uafgjort i event 2
            ("data3.txt",  1, 7.0,   7, "X"),             // kun én aktivitet
            ("data4.txt",  4, 2.5,  10, "Design, Kode, Test, Release"), // ren kæde: alt er kritisk
        };

        foreach (var t in tests)
        {
            Console.WriteLine($"--- {t.fil} ---");
            var plan = new Aktivitetsplan(Path.Combine(AppContext.BaseDirectory, t.fil));
            plan.Udskriv();

            int varighed = plan.KritiskVej(out List<string> tasks);
            bool ok = plan.AntalAktiviteter() == t.antal
                   && Math.Abs(plan.GennemsnitligVarighed() - t.gns) < 1e-9
                   && varighed == t.varighed
                   && string.Join(", ", tasks) == t.tasks;
            Console.WriteLine(ok ? "OK" : $"FEJL - forventet {t.antal}, {t.gns}, {t.varighed}, {t.tasks}");
        }
    }

    // ---------------------------------------------------------------------
    // Øvelse 2: 9.13 a - er grafen bipartit?
    // ---------------------------------------------------------------------
    private static void BipartitTest()
    {
        Console.WriteLine("=== Øvelse 2: 9.13 a - er grafen bipartit? ===");

        // Lige kreds (4 knuder): bipartit
        var square = new Graph(directed: false);
        square.AddEdge("1", "2");
        square.AddEdge("2", "3");
        square.AddEdge("3", "4");
        square.AddEdge("4", "1");
        CheckBipartite("Kreds med 4 knuder", square, true);

        // Ulige kreds (trekant): ikke bipartit
        var triangle = new Graph(directed: false);
        triangle.AddEdge("A", "B");
        triangle.AddEdge("B", "C");
        triangle.AddEdge("C", "A");
        CheckBipartite("Trekant          ", triangle, false);

        // To komponenter, hvor kun den anden har en ulige kreds
        var twoParts = new Graph(directed: false);
        twoParts.AddEdge("A", "B");
        twoParts.AddEdge("B", "C");
        twoParts.AddEdge("X", "Y");
        twoParts.AddEdge("Y", "Z");
        twoParts.AddEdge("Z", "X");
        CheckBipartite("To komponenter, den ene med trekant", twoParts, false);

        // Instruktører og kurser fra 9.13 b er bipartit pr. konstruktion
        CheckBipartite("Instruktører/kurser", InstructorGraph(), true);
    }

    private static void CheckBipartite(string label, Graph g, bool expected)
    {
        bool result = g.IsBipartite(out Dictionary<Vertex, int> side);
        string ok = result == expected ? "OK  " : "FEJL";
        Console.WriteLine($"{ok} {label}: {(result ? "bipartit" : "ikke bipartit")}");

        if (result)
        {
            // Kontrol: ingen kant må gå inden for samme delmængde
            bool valid = g.Vertices.All(v => v.Adjacent.All(e => side[e.From] != side[e.To]));
            var v1 = g.Vertices.Where(v => side[v] == 0).Select(v => v.Name);
            var v2 = g.Vertices.Where(v => side[v] == 1).Select(v => v.Name);
            Console.WriteLine($"     V1 = {{{string.Join(", ", v1)}}}, V2 = {{{string.Join(", ", v2)}}}" +
                              (valid ? "" : "  FEJL: kant inden for samme delmængde"));
        }
    }

    // ---------------------------------------------------------------------
    // Øvelse 2: 9.13 b - instruktører og kurser
    // ---------------------------------------------------------------------
    private static readonly string[] Instructors = { "Ole", "Anne", "Peter", "Mette", "Lars" };

    private static Graph InstructorGraph()
    {
        var g = new Graph(directed: false);
        foreach (string course in new[] { "ADS", "OOP", "DB", "Net", "Math" })
            g.AddVertex(course);

        g.AddEdge("Ole", "ADS");
        g.AddEdge("Ole", "OOP");
        g.AddEdge("Anne", "ADS");
        g.AddEdge("Peter", "OOP");
        g.AddEdge("Peter", "DB");
        g.AddEdge("Mette", "DB");
        g.AddEdge("Mette", "Net");
        g.AddEdge("Lars", "Net");
        // Ingen er kvalificeret til Math
        return g;
    }

    private static void MatchingTest()
    {
        Console.WriteLine("=== Øvelse 2: 9.13 b - instruktører og kurser ===");

        Graph g = InstructorGraph();
        Console.WriteLine("Hvem kan undervise i hvad:");
        foreach (string name in Instructors)
        {
            Vertex v = g.GetVertex(name);
            Console.WriteLine($"  {name}: {string.Join(", ", v.Adjacent.Select(e => e.To.Name))}");
        }

        var left = Instructors.Select(g.GetVertex).ToList();
        List<Edge> matching = g.MaximumBipartiteMatching(left);

        Console.WriteLine("Fordeling:");
        foreach (Edge e in matching.OrderBy(e => Array.IndexOf(Instructors, e.From.Name)))
            Console.WriteLine($"  {e.From.Name} underviser i {e.To.Name}");

        int best = BruteForceMatching(left, 0, new HashSet<Vertex>());
        Console.WriteLine($"{(matching.Count == best && IsValidMatching(matching) ? "OK  " : "FEJL")} " +
                          $"Maksimalt antal kurser: {matching.Count} (brute force: {best})");

        // Tilfældige bipartite grafer sammenlignet med brute force
        var rnd = new Random(1);
        int passed = 0;
        const int runs = 300;
        for (int run = 0; run < runs; run++)
        {
            int nLeft = rnd.Next(1, 7), nRight = rnd.Next(1, 7);
            var r = new Graph(directed: false);
            for (int i = 0; i < nLeft; i++) r.AddVertex("L" + i);
            for (int j = 0; j < nRight; j++) r.AddVertex("R" + j);
            for (int i = 0; i < nLeft; i++)
                for (int j = 0; j < nRight; j++)
                    if (rnd.NextDouble() < 0.35)
                        r.AddEdge("L" + i, "R" + j);

            var l = Enumerable.Range(0, nLeft).Select(i => r.GetVertex("L" + i)).ToList();
            List<Edge> m = r.MaximumBipartiteMatching(l);
            if (IsValidMatching(m) && m.Count == BruteForceMatching(l, 0, new HashSet<Vertex>()))
                passed++;
        }
        Console.WriteLine($"{(passed == runs ? "OK  " : "FEJL")} Tilfældige grafer: {passed}/{runs} har samme størrelse som brute force");
    }

    // Ingen knude må indgå i mere end én kant
    private static bool IsValidMatching(List<Edge> matching)
    {
        var used = new HashSet<Vertex>();
        return matching.All(e => used.Add(e.From) && used.Add(e.To));
    }

    // Prøv alle muligheder: knude i bliver enten ikke matchet eller matchet
    // med en ledig nabo. Eksponentiel - kun til kontrol på små grafer.
    private static int BruteForceMatching(List<Vertex> left, int i, HashSet<Vertex> usedRight)
    {
        if (i == left.Count)
            return 0;

        int best = BruteForceMatching(left, i + 1, usedRight);
        foreach (Edge e in left[i].Adjacent)
        {
            if (usedRight.Add(e.To))
            {
                best = Math.Max(best, 1 + BruteForceMatching(left, i + 1, usedRight));
                usedRight.Remove(e.To);
            }
        }
        return best;
    }
}
