/*
 * Reeksamen 27. februar 2024 – Opgave 2 (10 %)
 *
 * Opgaven:
 *   Hvad er Store O (Big-Oh) tidskompleksiteten for nedenstående metode?
 *   Begrund dit svar.
 *
 *     public static long myM(int N) {
 *         long x = 0;
 *         for (int i = 0; i < 10000; i++) {
 *             for (int j = 0; j < N*10; j++) {
 *                 for (int k = N; k > 0; k = k/10) {
 *                     x++;
 *                 }
 *             }
 *         }
 *         return x;
 *     }
 *
 *   En brugbar strategi for at løse opgaven, udover at analysere koden, kan
 *   være at lave nogle eksperimenter med koden, hvor du tæller antal af
 *   operationer, dvs. ser på returværdien for forskellige værdier af
 *   parameteren (N) for at skitsere et estimat af Store O tidskompleksiteten.
 *
 * Svar: O(N log N) – se "Opgave 2 tidskompleksitet.md".
 */
public class Opgave2Algoritme
{
    // Algoritmen fra opgaven, uændret.
    public static long MyM(int n)
    {
        long x = 0;
        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < n * 10; j++)
            {
                for (int k = n; k > 0; k = k / 10)
                {
                    x++;
                }
            }
        }
        return x;
    }
}
