/*
 * Reeksamen 24. februar 2026 – Opgave 3 (30 %)
 *
 * Opgaven:
 *   I en tabel/array af heltal definerer vi et bjerg, som en række af tal, hvor
 *   minimum to stigende tal er efterfulgt af minimum to faldende, fx {12,17,14,11}.
 *
 *   Skriv tre algoritmer med følgende egenskaber:
 *     1. Indeholder arrayet et bjerg (true/false)?          (5 %)
 *     2. Hvor mange bjerge indeholder arrayet?              (10 %)
 *     3. Hvad er længden på det længste bjerg i arrayet?    (15 %)
 *
 *   Eksempler:
 *     {12,34,56,1,3,19,12,55,78,34,48,6,53,19,20,7,11}  ingen bjerge
 *     {6,4,11,17,19,9,5,2,99,11,66,4,14,51,6,7,88,76}   ét bjerg; længde: 7
 *     {13,6,67,55,44,55,89,95,69,74,36,19,6,71,44,61,16} to bjerge; det længste: 5
 *
 *   Et tal kan godt indgå i flere bjerge: I {17,29,12,6,7,12,10,8,25,23,38,17,19}
 *   indgår 6 både i {17,29,12,6} og i {6,7,12,10,8}.
 *
 * Fortolkning (ud fra {12,17,14,11}):
 *   "to stigende tal"  = mindst ét skridt op   (12 < 17)
 *   "to faldende tal"  = mindst to skridt ned  (17 > 14 > 11)
 *   Et bjerg tages så langt som muligt: fra dalen før opstigningen til
 *   bunden af nedstigningen. Lige store nabotal bryder et bjerg.
 *
 * Alle tre metoder er O(n): hvert indeks besøges én gang.
 */
public class BjergFinder
{
    // 1. Stopper ved det første bjerg, der findes.
    public bool HarBjerg(int[] a)
    {
        int i = 1;
        while (i < a.Length)
        {
            int op = Stig(a, ref i);
            int ned = Fald(a, ref i);
            if (op >= 1 && ned >= 2)
                return true;
            if (op == 0 && ned == 0)
                i++;                    // to ens tal i træk
        }
        return false;
    }

    // 2.
    public int AntalBjerge(int[] a)
    {
        int antal = 0;
        int i = 1;
        while (i < a.Length)
        {
            int op = Stig(a, ref i);
            int ned = Fald(a, ref i);
            if (op >= 1 && ned >= 2)
                antal++;
            if (op == 0 && ned == 0)
                i++;
        }
        return antal;
    }

    // 3. Længden = antal tal i bjerget = op-skridt + ned-skridt + 1 (toppen).
    //    Returnerer 0, hvis der ikke er noget bjerg.
    public int LængsteBjerg(int[] a)
    {
        int længste = 0;
        int i = 1;
        while (i < a.Length)
        {
            int op = Stig(a, ref i);
            int ned = Fald(a, ref i);
            if (op >= 1 && ned >= 2)
                længste = Math.Max(længste, op + ned + 1);
            if (op == 0 && ned == 0)
                i++;
        }
        return længste;
    }

    // Går frem så længe a[i] > a[i-1] og returnerer antal skridt op.
    private static int Stig(int[] a, ref int i)
    {
        int skridt = 0;
        while (i < a.Length && a[i] > a[i - 1])
        {
            skridt++;
            i++;
        }
        return skridt;
    }

    // Går frem så længe a[i] < a[i-1] og returnerer antal skridt ned.
    // Bemærk: i stopper PÅ bunden (a[i-1]), så bunden kan være starten
    // på næste bjerg – derfor kan et tal indgå i to bjerge.
    private static int Fald(int[] a, ref int i)
    {
        int skridt = 0;
        while (i < a.Length && a[i] < a[i - 1])
        {
            skridt++;
            i++;
        }
        return skridt;
    }
}
