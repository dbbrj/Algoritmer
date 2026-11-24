/*
 * Reeksamen 24. februar 2026 – Opgave 1.2 (11 %)
 *
 * Opgaven:
 *   Skriv en rekursiv algoritme, der kaldes med et array af heltal og et bestemt
 *   heltal. Antallet af forekomster af dette heltal i arrayet skal returneres.
 *   Eksempel: i arrayet {12,4,88,19,6,28,4,39,22} forekommer 4 to gange og 37
 *   nul gange.
 */
public class ForekomstTæller
{
    // Kaldes med Antal(arr, tal, 0).
    // Basistilfælde: index er forbi sidste element -> 0 forekomster.
    // Rekursivt tilfælde: (1 hvis arr[index] == tal, ellers 0) + forekomster i resten.
    // N+1 kald -> O(N).
    public int Antal(int[] arr, int tal, int index)
    {
        if (index >= arr.Length)
            return 0;
        int her = arr[index] == tal ? 1 : 0;
        return her + Antal(arr, tal, index + 1);
    }
}
