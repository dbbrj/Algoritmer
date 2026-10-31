/*
 * Øvelse 1 – sortering (lektion 5/6)
 *
 * Opgaven:
 *   En tabel indeholder N sorterede heltal. Opgaven går ud på at afgøre om
 *   tabellen indeholder to tal, hvis sum er lig med parameteren X. Metoden
 *   skal skrives i to udgaver: én med kvadratisk tidskompleksitet og en med
 *   lineær.
 */
public class TwoSum
{
    // O(N^2): prøv alle par (i, j) med i < j.
    // Den yderste løkke kører N gange, den inderste op til N-1 gange,
    // i alt N(N-1)/2 sammenligninger i værste fald.
    public bool HasPairQuadratic(int[] a, int x)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[i] + a[j] == x)
                    return true;
            }
        }
        return false;
    }

    // O(N): to pegere, én i hver ende af den sorterede tabel.
    // - Er summen for lille, skal vi have et større tal: flyt left til højre.
    // - Er summen for stor, skal vi have et mindre tal: flyt right til venstre.
    // Hver iteration flytter én peger ét skridt, så der er højst N-1 iterationer.
    public bool HasPairLinear(int[] a, int x)
    {
        int left = 0;
        int right = a.Length - 1;

        while (left < right)
        {
            // long, så summen af to store int'er ikke giver overflow
            long sum = (long)a[left] + a[right];

            if (sum == x)
                return true;
            else if (sum < x)
                left++;
            else
                right--;
        }
        return false;
    }
}
