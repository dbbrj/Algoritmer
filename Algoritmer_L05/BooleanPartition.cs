/*
 * Opgave 7.44 (Weiss)
 *
 * Opgaven:
 *   Suppose you have an array of N elements containing only two distinct keys,
 *   true and false. Give an O(N) algorithm to rearrange the list so that all
 *   false elements precede the true elements. You may use only constant extra
 *   space.
 */
public class BooleanPartition
{
    // To pegere, ligesom partitioneringen i quicksort:
    // - left leder fra venstre efter et true (som står forkert).
    // - right leder fra højre efter et false (som står forkert).
    // Når begge har fundet et, byttes de. Hvert element besøges højst én gang,
    // så tiden er O(N), og der bruges kun to int-variabler: O(1) ekstra plads.
    public void Rearrange(bool[] a)
    {
        int left = 0;
        int right = a.Length - 1;

        while (left < right)
        {
            while (left < right && !a[left])  // false står rigtigt i venstre side
                left++;
            while (left < right && a[right])  // true står rigtigt i højre side
                right--;

            if (left < right)
            {
                // a[left] er true og a[right] er false: byt dem
                a[left] = false;
                a[right] = true;
                left++;
                right--;
            }
        }
    }
}
