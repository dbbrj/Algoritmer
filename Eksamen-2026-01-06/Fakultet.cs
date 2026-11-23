/*
 * Eksamen 6. januar 2026 – Opgave 1.1 (6 %)
 *
 * Opgaven:
 *   Skriv en rekursiv algoritme, som kan returnere værdien af N! (N fakultet).
 *   Kaldt med N = 5, returneres 120 (5*4*3*2*1).
 */
public class Fakultet
{
    // Basistilfælde: 0! = 1! = 1.
    // Rekursivt tilfælde: N! = N * (N-1)!
    // N kald i alt -> O(N).
    public long Beregn(int n)
    {
        if (n <= 1)
            return 1;
        return n * Beregn(n - 1);
    }
}
