/*
 * Reeksamen 27. februar 2024 – Opgave 7, første del
 *
 * Opgaven:
 *   I tabellen er angivet en fejlbehæftet slutkonfiguration for traversering
 *   af grafen ved anvendelse af Dijkstras algoritme med start i node s.
 *   Første del af opgaven går ud på at rette konfigurationen, så den bliver
 *   korrekt.
 *
 * Dijkstra med tabellen (known, dv, pv) som i bogen (Weiss kap. 9.3.2).
 * Hver gang en node gøres known, skrives det ud, så rækkefølgen kan følges.
 */
public class Dijkstra
{
    public record Række(char V, bool Known, int Dv, char? Pv);

    public Dictionary<char, Række> Kør(Graf graf, char start, List<string> log)
    {
        var known = graf.Noder.ToDictionary(n => n, _ => false);
        var dv = graf.Noder.ToDictionary(n => n, _ => int.MaxValue);
        var pv = graf.Noder.ToDictionary(n => n, _ => (char?)null);
        dv[start] = 0;

        while (true)
        {
            // Vælg den ukendte node med mindst dv.
            char? v = null;
            foreach (char n in graf.Noder.OrderBy(n => n))
            {
                if (!known[n] && dv[n] != int.MaxValue && (v == null || dv[n] < dv[v.Value]))
                    v = n;
            }
            if (v == null)
                break;

            known[v.Value] = true;
            var opdateringer = new List<string>();
            foreach (var kant in graf.UdgåendeKanter(v.Value))
            {
                if (known[kant.Til])
                    continue;
                int ny = dv[v.Value] + kant.Vægt;
                if (ny < dv[kant.Til])
                {
                    dv[kant.Til] = ny;
                    pv[kant.Til] = v.Value;
                    opdateringer.Add($"{kant.Til}={ny}");
                }
            }
            log.Add($"known {v} (d={dv[v.Value]}): opdaterer {(opdateringer.Count > 0 ? string.Join(", ", opdateringer) : "ingen")}");
        }

        return graf.Noder.ToDictionary(n => n, n => new Række(n, known[n], dv[n], pv[n]));
    }
}
