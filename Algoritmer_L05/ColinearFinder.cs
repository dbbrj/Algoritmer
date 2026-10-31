/*
 * Opgave 7.38 (Weiss)
 *
 * Opgaven:
 *   Write a program that reads N points in a plane and outputs any group of
 *   four or more colinear points (i.e., points on the same line). The obvious
 *   brute-force algorithm requires O(N^4) time. However, there is a better
 *   algorithm that makes use of sorting and runs in O(N^2 log N) time.
 *
 * Forudsætning: punkterne er forskellige.
 */
public class ColinearFinder
{
    // Idé: for hvert punkt p beregnes retningen (hældningen) fra p til alle
    // andre punkter. Retningerne sorteres, så punkter med samme hældning ligger
    // ved siden af hinanden. Har 3 eller flere punkter samme hældning set fra p,
    // ligger de og p på samme linje: en gruppe på 4 eller flere.
    //
    // Tid: N punkter * (N beregninger + sortering af N retninger)
    //      = N * O(N log N) = O(N^2 log N).
    public List<List<Point>> FindGroups(Point[] points)
    {
        var result = new List<List<Point>>();

        foreach (Point p in points)
        {
            // Retning fra p til hvert andet punkt
            var others = new List<(int dx, int dy, Point q)>();
            foreach (Point q in points)
            {
                if (q.X == p.X && q.Y == p.Y)
                    continue;
                var (dx, dy) = Direction(p, q);
                others.Add((dx, dy, q));
            }

            // Sortér efter retning. Retningerne er normaliserede, så samme
            // hældning giver præcis samme (dx, dy) og ender ved siden af hinanden.
            others.Sort((u, v) => u.dx != v.dx ? u.dx.CompareTo(v.dx) : u.dy.CompareTo(v.dy));

            // Find løb af ens retninger
            int start = 0;
            while (start < others.Count)
            {
                int end = start;
                while (end + 1 < others.Count
                       && others[end + 1].dx == others[start].dx
                       && others[end + 1].dy == others[start].dy)
                    end++;

                int runLength = end - start + 1;
                if (runLength >= 3)
                {
                    var group = new List<Point> { p };
                    for (int i = start; i <= end; i++)
                        group.Add(others[i].q);

                    // Samme linje findes fra hvert af dens punkter. For kun at
                    // udskrive den én gang beholdes den kun, når p er linjens
                    // mindste punkt.
                    if (group.All(g => p.CompareTo(g) <= 0))
                    {
                        group.Sort((a, b) => a.CompareTo(b));
                        result.Add(group);
                    }
                }
                start = end + 1;
            }
        }
        return result;
    }

    // Retningen fra p til q som et forkortet heltalspar (dx, dy).
    // Heltal i stedet for double undgår afrundingsfejl ved sammenligning.
    // Fortegnet gøres entydigt (dx > 0, eller dx == 0 og dy > 0), så fx
    // (2, 4) og (-1, -2) begge bliver til (1, 2).
    private static (int dx, int dy) Direction(Point p, Point q)
    {
        int dx = q.X - p.X;
        int dy = q.Y - p.Y;

        int g = Gcd(Math.Abs(dx), Math.Abs(dy));
        dx /= g;
        dy /= g;

        if (dx < 0 || (dx == 0 && dy < 0))
        {
            dx = -dx;
            dy = -dy;
        }
        return (dx, dy);
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            int t = a % b;
            a = b;
            b = t;
        }
        return a;
    }
}
