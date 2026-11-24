/*
 * Supplerende øvelse (lektion 9): Implementer din egen graf med klasserne
 * Edge, Vertex og Graph, og lav øvelse 9.13 a og b i lærebogen.
 *
 * En knude (vertex) med navn og adjacency list (de kanter, der går ud fra den).
 */
public class Vertex
{
    public Vertex(string name)
    {
        Name = name;
    }

    public string Name { get; }

    // Adjacency list: kanterne ud fra denne knude
    public List<Edge> Adjacent { get; } = new List<Edge>();

    public override string ToString()
    {
        return Name;
    }
}
