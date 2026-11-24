/*
 * Reeksamen 27. februar 2024 – Opgave 4 (15 %)
 *
 * Opgaven:
 *   Opgaven går ud på at skrive en metode, der kan sortere et array, som
 *   altid består af 100 positive heltal, hvorom det gælder, at alle tal i
 *   tabellen er større end eller lig med en (1) og mindre end eller lig med 200.
 *
 *   Metoden har ingen returværdi, men skal blot udskrive tallene fra tabellen
 *   i stigende rækkefølge, og det er ikke tilladt at benytte sorteringsmetoder
 *   fra standardbibliotekerne.
 *
 *   For at få fuldt points skal din metode have lineær - O(N) -
 *   tidskompleksitet.
 *
 *   Metoden kunne have følgende signatur:
 *
 *     void minSortering(int[] arr);
 */
public class TælleSortering
{
    private const int MaxVærdi = 200;

    // Counting sort (tællesortering).
    // 1) Tæl hvor mange gange hvert tal 1..200 forekommer: én gang gennem arr -> O(N).
    // 2) Gå tællerne igennem fra 1 til 200 og udskriv hvert tal det antal gange,
    //    det forekommer: O(K + N), hvor K = 200 er en konstant.
    // I alt O(N + K) = O(N), fordi K ikke afhænger af N.
    // Ingen sammenligninger mellem elementerne, så O(N log N)-grænsen for
    // sammenligningsbaseret sortering gælder ikke.
    public void MinSortering(int[] arr)
    {
        int[] antal = new int[MaxVærdi + 1]; // indeks 0 bruges ikke

        foreach (int tal in arr)
            antal[tal]++;

        var sb = new System.Text.StringBuilder();
        for (int værdi = 1; værdi <= MaxVærdi; værdi++)
        {
            for (int i = 0; i < antal[værdi]; i++)
                sb.Append(værdi).Append(' ');
        }
        Console.WriteLine(sb.ToString().TrimEnd());
    }

    // Samme algoritme, men returnerer det sorterede array i stedet for at
    // udskrive det. Bruges kun til at teste, at resultatet er korrekt.
    public int[] Sorteret(int[] arr)
    {
        int[] antal = new int[MaxVærdi + 1];
        foreach (int tal in arr)
            antal[tal]++;

        int[] resultat = new int[arr.Length];
        int pos = 0;
        for (int værdi = 1; værdi <= MaxVærdi; værdi++)
        {
            for (int i = 0; i < antal[værdi]; i++)
                resultat[pos++] = værdi;
        }
        return resultat;
    }
}
