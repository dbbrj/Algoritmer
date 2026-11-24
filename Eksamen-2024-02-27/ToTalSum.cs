/*
 * Reeksamen 27. februar 2024 – Opgave 1 (12 %)
 *
 * Opgaven:
 *   Skriv en metode/algoritme med følgende signatur:
 *
 *     bool sumAfToTalLigParameter(int[] arr, int l, int X)
 *
 *   Den første parameter er et array af positive, sorterede heltal uden
 *   dubletter; anden parameter er længden af arrayet; og tredje parameter er
 *   den værdi, der ledes efter.
 *
 *   Metoden skal finde ud af, om der findes to tal i arrayet, hvis sum er lig
 *   med parameteren X. Hvis svaret er ja, returneres true. Hvis svaret er nej,
 *   returneres false. Det samme tal må ikke bruges to gange.
 *
 *   Opgaven skal løses i to versioner således at:
 *     • Version 1 har kvadratisk tidskompleksitet – O(N^2).
 *     • Version 2 har lineær tidskompleksitet O(N), det vil sige, at hvert
 *       element i tabellen kun må læses én gang.
 *
 *   De to løsninger tæller lige meget, altså 6 %.
 */
public class ToTalSum
{
    // Version 1 – O(N^2)
    // Prøver alle par (i, j) med i < j. j starter efter i, så samme tal
    // aldrig bruges to gange, og hvert par kun tjekkes én gang.
    // Antal par: N(N-1)/2 -> O(N^2).
    public bool SumAfToTalLigParameterV1(int[] arr, int l, int x)
    {
        for (int i = 0; i < l - 1; i++)
        {
            for (int j = i + 1; j < l; j++)
            {
                if (arr[i] + arr[j] == x)
                    return true;
            }
        }
        return false;
    }

    // Version 2 – O(N)
    // To pegere: venstre starter ved det mindste tal, højre ved det største.
    //   sum == x -> fundet.
    //   sum <  x -> summen skal være større, og arr[venstre] kan ikke indgå i
    //               noget par (det største mulige partner er arr[højre]),
    //               så venstre rykkes én frem.
    //   sum >  x -> tilsvarende kan arr[højre] ikke indgå i noget par,
    //               så højre rykkes én tilbage.
    // Hvert skridt flytter én pegepind, og hver pegepind passerer hvert
    // element højst én gang -> højst N-1 skridt -> O(N).
    // Løkken stopper, når venstre == højre, så samme tal bruges aldrig to gange.
    public bool SumAfToTalLigParameterV2(int[] arr, int l, int x)
    {
        int venstre = 0;
        int højre = l - 1;
        while (venstre < højre)
        {
            int sum = arr[venstre] + arr[højre];
            if (sum == x)
                return true;
            if (sum < x)
                venstre++;
            else
                højre--;
        }
        return false;
    }
}
