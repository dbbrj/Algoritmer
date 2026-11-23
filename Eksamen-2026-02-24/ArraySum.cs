/*
 * Reeksamen 24. februar 2026 – Opgave 1.1 (9 %)
 *
 * Opgaven:
 *   Skriv en rekursiv algoritme, som kan returnere summen af elementerne i et
 *   array bestående af heltal.
 *   Kaldt med arrayet {15,17,6,21,8} returneres 67.
 */
public class ArraySum
{
    // Kaldes med Sum(arr, 0).
    // Basistilfælde: index er forbi sidste element -> summen af "ingenting" er 0.
    // Rekursivt tilfælde: arr[index] + summen af resten.
    // N+1 kald -> O(N).
    public int Sum(int[] arr, int index)
    {
        if (index >= arr.Length)
            return 0;
        return arr[index] + Sum(arr, index + 1);
    }
}
