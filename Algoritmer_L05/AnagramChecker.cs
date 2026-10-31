/*
 * Øvelse 2 – sortering (lektion 5/6)
 *
 * Opgaven:
 *   Ordene stale og least er anagrammer, dvs. de indeholder nøjagtig de samme
 *   karakterer. Skriv en metode, som kan afgøre, om to ord (strings) er
 *   anagrammer.
 */
public class AnagramChecker
{
    // Udgave 1 – sortering, O(N log N).
    // To ord er anagrammer, hvis de er ens, når bogstaverne er sorteret:
    // "stale" -> "aelst" og "least" -> "aelst".
    public bool IsAnagramSort(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        char[] ca = a.ToCharArray();
        char[] cb = b.ToCharArray();
        Array.Sort(ca);
        Array.Sort(cb);

        for (int i = 0; i < ca.Length; i++)
        {
            if (ca[i] != cb[i])
                return false;
        }
        return true;
    }

    // Udgave 2 – optælling (samme idé som bucketSort), O(N).
    // Tæl op for hvert tegn i a og ned for hvert tegn i b.
    // Er alle tællere 0 til sidst, indeholder ordene præcis de samme tegn.
    // En Dictionary bruges i stedet for et array med fast størrelse,
    // så æ, ø, å og andre tegn også virker.
    public bool IsAnagramCount(string a, string b)
    {
        if (a.Length != b.Length)
            return false;

        var counts = new Dictionary<char, int>();

        foreach (char c in a)
            counts[c] = counts.GetValueOrDefault(c) + 1;

        foreach (char c in b)
        {
            int n = counts.GetValueOrDefault(c) - 1;
            if (n < 0)
                return false; // b har flere af dette tegn end a
            counts[c] = n;
        }

        // Længderne er ens, og ingen tæller blev negativ,
        // så alle tællere må være 0.
        return true;
    }
}
