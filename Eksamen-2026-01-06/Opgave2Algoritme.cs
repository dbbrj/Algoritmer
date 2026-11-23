/*
 * Eksamen 6. januar 2026 – Opgave 2 (20 %)
 *
 * Opgaven:
 *   Betragt følgende algoritme:
 *
 *     public static long myMethod(long N) {
 *         long x = 0;
 *         for (int i = 0; i < N; i++) {
 *             for (int j = 0; j < N; j += j+1) {
 *                 for (int k = 0; k < N; k += (i+j+5)) {
 *                     x++; } } if (i > 1024) i++; } return x;
 *     }
 *
 *   Giv dit bedste bud på algoritmens tidskompleksitet i Store-O notationen.
 *   Dit svar skal begrundes detaljeret, helst ved hjælp af testresultater.
 *
 * Bud: O(N log² N) – se "Opgave 2 tidskompleksitet.md".
 */
public class Opgave2Algoritme
{
    // Algoritmen fra opgaven, uændret.
    public static long MyMethod(long n)
    {
        long x = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j += j + 1)
            {
                for (int k = 0; k < n; k += (i + j + 5))
                {
                    x++;
                }
            }
            if (i > 1024) i++;
        }
        return x;
    }
}
