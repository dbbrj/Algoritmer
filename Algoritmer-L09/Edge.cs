/*
 * Supplerende øvelse (lektion 9): Implementer din egen graf med klasserne
 * Edge, Vertex og Graph, og lav øvelse 9.13 a og b i lærebogen.
 *
 * En kant fra From til To med en vægt (cost). Uvægtede grafer bruger vægt 1.
 */
public class Edge
{
    public Edge(Vertex from, Vertex to, int weight = 1)
    {
        From = from;
        To = to;
        Weight = weight;
    }

    public Vertex From { get; }
    public Vertex To { get; }
    public int Weight { get; }

    public override string ToString()
    {
        return $"{From.Name} - {To.Name}";
    }
}
