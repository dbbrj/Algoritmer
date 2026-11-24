/*
 * Reeksamen 27. februar 2024 – Opgave 7, anden del
 *
 * Opgaven:
 *   Anden del af opgaven er at udarbejde et minimum spanning tree for grafen,
 *   hvis den var undirected. Angiv hvilken algoritme, der er anvendt og
 *   træets vægt.
 *
 *   Du behøver ikke at tegne grafen; men du skal, under alle omstændigheder,
 *   skrive, i hvilken rækkefølge kanterne (edges) tilføjes.
 *
 * Kruskal: sortér kanterne efter vægt og tag dem én ad gangen, hvis de
 * forbinder to forskellige komponenter. Komponenterne holdes styr på med
 * union-find (disjoint sets, Weiss kap. 8).
 */
public class Kruskal
{
    private readonly Dictionary<char, char> forælder = new();

    private char Find(char x)
    {
        while (forælder[x] != x)
            x = forælder[x];
        return x;
    }

    // Returnerer de valgte kanter i den rækkefølge, de tilføjes.
    // Ved lige vægt bevares rækkefølgen fra grafen (OrderBy er stabil).
    public List<Graf.Kant> Kør(Graf graf, List<string> log)
    {
        foreach (char n in graf.Noder)
            forælder[n] = n;

        var valgt = new List<Graf.Kant>();
        foreach (var kant in graf.Kanter.OrderBy(k => k.Vægt))
        {
            if (valgt.Count == graf.Noder.Count - 1)
                break;

            char a = Find(kant.Fra);
            char b = Find(kant.Til);
            if (a == b)
            {
                log.Add($"{kant.Fra}-{kant.Til} ({kant.Vægt}): afvist – laver kreds");
                continue;
            }
            forælder[a] = b;
            valgt.Add(kant);
            log.Add($"{kant.Fra}-{kant.Til} ({kant.Vægt}): valgt som kant nr. {valgt.Count}");
        }
        return valgt;
    }
}
