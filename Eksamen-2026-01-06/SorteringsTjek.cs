/*
 * Eksamen 6. januar 2026 – Opgave 1.2 (9 %)
 *
 * Opgaven:
 *   Skriv en rekursiv algoritme, som kan afgøre, om et array er sorteret eller ej.
 *   Signaturforslag Java og C#: boolean isSorted(int[] arr, int index)
 *   {23,44,61,90,112} returnerer true.
 *   {23,44,31,90,112} returnerer false.
 */
public class SorteringsTjek
{
    // Basistilfælde: er vi ved sidste element (eller arrayet er tomt),
    // er resten trivielt sorteret.
    // Ellers: arr[index] <= arr[index+1] OG resten fra index+1 er sorteret.
    // Hvert kald rykker index én frem -> O(N).
    public bool IsSorted(int[] arr, int index)
    {
        if (index >= arr.Length - 1)
            return true;
        if (arr[index] > arr[index + 1])
            return false;
        return IsSorted(arr, index + 1);
    }
}
