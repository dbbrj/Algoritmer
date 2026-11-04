/*
 * Opgave i rekurrens: QuickSelect (lektion 6)
 *
 * Opgaven:
 *   I starten af kurset blev I introduceret til selektionsproblemet, dvs.
 *   opgaven med at finde det k.-største eller mindste element i en tabel af
 *   usorterede heltal.
 *
 *   En løsning med kvadratisk tidskompleksitet er indlysende: sortér tabellen
 *   med en af de simple sorteringsalgoritmer og returner det k. element.
 *   Anvendelse af en O(N log N) sortering vil naturligvis forbedre performance.
 *
 *   Det kan imidlertid lade sig gøre at udføre opgaven i lineær tid ved at
 *   bruge principperne i QuickSort. Nedenfor er angivet koden for QuickSelect
 *   skrevet i C#. public-metoden returnerer det k.-mindste element.
 *
 *   Som I kan se, anvender koden ikke insertionSort, når tabelstørrelsen
 *   kommer under et vist niveau.
 *
 *   Opgaven går ud på at opstille rekurrens-ligningen for QuickSelect, løse
 *   den og dermed bevise, at tidskompleksiteten er lineær, altså O(N).
 *
 *   Det giver naturligvis god mening at lade sig inspirere af analysen af
 *   average case QuickSort.
 *
 * Koden nedenfor er underviserens QuickSelect. Den eneste tilføjelse er en
 * tæller for antal sammenligninger med pivot, så vi kan måle, at arbejdet
 * vokser lineært med N. Selve analysen står i "Opgave QuickSelect rekurrens.md".
 */
public class QuickSelect
{
    // Antal sammenligninger a[j] < pivot siden sidste nulstilling.
    public long Comparisons { get; private set; }

    public void ResetCount() => Comparisons = 0;

    // Returnerer det position.-mindste element (position er 1-baseret).
    // OBS: tabellen bliver omarrangeret undervejs (partitionering sker in-place).
    public int Select(int[] a, int position)
    {
        if (position < 1 || position > a.Length)
            throw new ArgumentOutOfRangeException(nameof(position));

        return Select(a, 0, a.Length - 1, position - 1);
    }

    // k er 0-baseret: det element, der ville stå på plads k i sorteret orden.
    private int Select(int[] a, int left, int right, int k)
    {
        // Kun ét element tilbage
        if (left == right)
            return a[left];

        // Vælg elementet længst til højre som pivot
        int pivot = a[right];
        int i = left;

        // Partitionér: alt mindre end pivot flyttes til venstre for i.
        // Løkken kører right - left gange, dvs. N - 1 sammenligninger -> cN-leddet.
        for (int j = left; j < right; j++)
        {
            Comparisons++;
            if (a[j] < pivot)
            {
                int temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                i++;
            }
        }

        // Sæt pivot på sin endelige plads
        int temp2 = a[i];
        a[i] = a[right];
        a[right] = temp2;

        // i er nu pivots position. Vi går kun videre i ÉN af de to dele.
        if (k == i)
            return a[i];

        if (k < i)
            return Select(a, left, i - 1, k);
        else
            return Select(a, i + 1, right, k);
    }
}
