/*
 * Supplerende øvelse (lektion 9): Implementer din egen graf med klasserne
 * Edge, Vertex og Graph, og lav øvelse 9.13 a og b i lærebogen.
 *
 * 9.13 A bipartite graph, G = (V, E), is a graph such that V can be partitioned
 *      into two subsets V1 and V2 and no edge has both its vertices in the same
 *      subset.
 *   a. Give a linear algorithm to determine whether a graph is bipartite.
 *   b. The bipartite matching problem is to find the largest subset E' of E
 *      such that no vertex is included in more than one edge. [...]
 *      Show how the bipartite matching problem can be used to solve the
 *      following problem: We have a set of instructors, a set of courses, and a
 *      list of courses that each instructor is qualified to teach. If no
 *      instructor is required to teach more than one course, and only one
 *      instructor may teach a given course, what is the maximum number of
 *      courses that can be offered?
 *
 * Grafen gemmes som adjacency list (god til sparse grafer, plads O(|E| + |V|)).
 * Forklaringer står i "Øvelse 2 graf og opgave 9.13.md".
 */
public class Graph
{
    private readonly Dictionary<string, Vertex> vertices = new Dictionary<string, Vertex>();

    public Graph(bool directed)
    {
        Directed = directed;
    }

    public bool Directed { get; }

    public IEnumerable<Vertex> Vertices => vertices.Values;

    public int VertexCount => vertices.Count;

    // Antal kanter. En uorienteret kant tælles én gang, selvom den ligger
    // i begge knuders adjacency list.
    public int EdgeCount
    {
        get
        {
            int count = vertices.Values.Sum(v => v.Adjacent.Count);
            return Directed ? count : count / 2;
        }
    }

    public Vertex AddVertex(string name)
    {
        if (!vertices.TryGetValue(name, out Vertex? v))
        {
            v = new Vertex(name);
            vertices.Add(name, v);
        }
        return v;
    }

    public Vertex GetVertex(string name)
    {
        return vertices[name];
    }

    // Opretter knuderne, hvis de ikke findes. I en uorienteret graf
    // lægges kanten i begge knuders adjacency list.
    public void AddEdge(string from, string to, int weight = 1)
    {
        Vertex v = AddVertex(from);
        Vertex w = AddVertex(to);

        v.Adjacent.Add(new Edge(v, w, weight));
        if (!Directed)
            w.Adjacent.Add(new Edge(w, v, weight));
    }

    public void Print()
    {
        foreach (Vertex v in vertices.Values)
            Console.WriteLine($"  {v.Name}: {string.Join(", ", v.Adjacent.Select(e => e.To.Name))}");
    }

    // =====================================================================
    // 9.13 a: Er grafen bipartit?
    //
    // Bredde-først søgning, hvor hver knude farves 0 eller 1. Naboen til en
    // knude med farve c skal have farven 1 - c. Møder vi en nabo, der allerede
    // har samme farve som knuden selv, er der en kant inden for samme
    // delmængde, og grafen er ikke bipartit.
    //
    // Ydre løkke over alle knuder, så grafer med flere komponenter også
    // håndteres. Hver knude sættes i køen én gang, og hver kant kigges på
    // højst to gange: O(|V| + |E|), altså lineært.
    //
    // side: knude -> 0 (V1) eller 1 (V2). Er kun gyldig, hvis metoden
    // returnerer true.
    // =====================================================================
    public bool IsBipartite(out Dictionary<Vertex, int> side)
    {
        side = new Dictionary<Vertex, int>();
        var queue = new Queue<Vertex>();

        foreach (Vertex start in vertices.Values)
        {
            if (side.ContainsKey(start))
                continue; // allerede farvet i en tidligere komponent

            side[start] = 0;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Vertex v = queue.Dequeue();

                foreach (Edge e in v.Adjacent)
                {
                    Vertex w = e.To;

                    if (!side.ContainsKey(w))
                    {
                        side[w] = 1 - side[v];
                        queue.Enqueue(w);
                    }
                    else if (side[w] == side[v])
                    {
                        return false; // kant inden for samme delmængde
                    }
                }
            }
        }

        return true;
    }

    // =====================================================================
    // 9.13 b: Maksimal bipartit matching
    //
    // left er knuderne i V1 (fx instruktørerne). Algoritmen bruger
    // augmenting paths (Kuhns algoritme): for hver knude i V1 forsøger vi at
    // finde en ledig partner i V2. Er partneren optaget, prøver vi at flytte
    // dens nuværende partner til en anden ledig knude - rekursivt. Det er den
    // samme idé som augmenting paths i network flow (9.13 c).
    //
    // Køretid: for hver af de |V1| knuder laves én dybde-først søgning, der
    // højst kigger på hver kant én gang: O(|V| * |E|).
    //
    // Returnerer de valgte kanter (fra V1 til V2).
    // =====================================================================
    public List<Edge> MaximumBipartiteMatching(IEnumerable<Vertex> left)
    {
        var partnerOf = new Dictionary<Vertex, Edge>(); // knude i V2 -> valgt kant

        foreach (Vertex v in left)
        {
            var visited = new HashSet<Vertex>();
            TryAugment(v, visited, partnerOf);
        }

        return partnerOf.Values.ToList();
    }

    private bool TryAugment(Vertex v, HashSet<Vertex> visited, Dictionary<Vertex, Edge> partnerOf)
    {
        foreach (Edge e in v.Adjacent)
        {
            Vertex w = e.To;
            if (!visited.Add(w))
                continue; // allerede prøvet i denne søgning

            // w er ledig, eller w's nuværende partner kan flyttes til en anden
            if (!partnerOf.TryGetValue(w, out Edge? current)
                || TryAugment(current.From, visited, partnerOf))
            {
                partnerOf[w] = e;
                return true;
            }
        }

        return false;
    }
}
