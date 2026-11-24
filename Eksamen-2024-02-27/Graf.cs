/*
 * Reeksamen 27. februar 2024 – Opgave 7 (15 %)
 *
 * Grafen fra opgaven (orienteret, aflæst fra figuren):
 *
 *   s->a 5   s->h 2   h->a 2   a->b 7   a->f 3   f->b 2   f->h 2
 *   f->e 6   h->e 9   b->c 8   b->e 5   b->g 7   e->g 2   g->c 3   c->d 4
 *
 * (Opgaven siger, at pletten mellem f og h ikke er et minus: vægten er 2.)
 *
 * Klassen holder kanterne. Dijkstra bruger dem som orienterede kanter,
 * Kruskal bruger dem som uorienterede.
 */
public class Graf
{
    public record Kant(char Fra, char Til, int Vægt);

    public List<char> Noder { get; } = new();
    public List<Kant> Kanter { get; } = new();

    public void TilføjKant(char fra, char til, int vægt)
    {
        if (!Noder.Contains(fra)) Noder.Add(fra);
        if (!Noder.Contains(til)) Noder.Add(til);
        Kanter.Add(new Kant(fra, til, vægt));
    }

    public IEnumerable<Kant> UdgåendeKanter(char node) => Kanter.Where(k => k.Fra == node);

    public static Graf Opgave7Graf()
    {
        var g = new Graf();
        g.TilføjKant('s', 'a', 5);
        g.TilføjKant('s', 'h', 2);
        g.TilføjKant('h', 'a', 2);
        g.TilføjKant('a', 'b', 7);
        g.TilføjKant('a', 'f', 3);
        g.TilføjKant('f', 'b', 2);
        g.TilføjKant('f', 'h', 2);
        g.TilføjKant('f', 'e', 6);
        g.TilføjKant('h', 'e', 9);
        g.TilføjKant('b', 'c', 8);
        g.TilføjKant('b', 'e', 5);
        g.TilføjKant('b', 'g', 7);
        g.TilføjKant('e', 'g', 2);
        g.TilføjKant('g', 'c', 3);
        g.TilføjKant('c', 'd', 4);
        return g;
    }
}
