/*
 * Reeksamen 24. februar 2026 – Opgave 2 (15 %)
 *
 * Opgaven:
 *   Betragt følgende algoritme:
 *
 *     public static int myMethod(int N)
 *     {
 *         int x = 0;
 *         for (int i = 0; i < N * Math.sqrt(N); i++)
 *         {
 *             for (int j = 0; j < N; j++)
 *             {
 *                 for (int k = 0; k < N * 10; k++)
 *                 {
 *                     x++;
 *                 }
 *                 j *= 2;
 *             }
 *         }
 *         return x;
 *     }
 *
 *   Giv dit bedste bud på algoritmens tidskompleksitet i Store-O notationen.
 *   Dit svar skal begrundes detaljeret, gerne ved hjælp af testresultater.
 *
 * Bud: O(N^2,5 · log N) – se "Opgave 2 tidskompleksitet.md".
 */
public class Opgave2Algoritme
{
    // Algoritmen fra opgaven. Eneste ændring: x er long i stedet for int,
    // fordi int løber over allerede ved N = 1024 (x ≈ 3,7 mia.).
    public static long MyMethod(int n)
    {
        long x = 0;
        for (int i = 0; i < n * Math.Sqrt(n); i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n * 10; k++)
                {
                    x++;
                }
                j *= 2;
            }
        }
        return x;
    }
}
