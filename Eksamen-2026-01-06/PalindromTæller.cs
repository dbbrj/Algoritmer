/*
 * Eksamen 6. januar 2026 – Opgave 3 (35 %)
 *
 * Opgaven:
 *   Et palindrom er et ord som staves ens, når man læser det forfra, og når man
 *   læser det bagfra. Eksempler: otto mellem kajak
 *   Længden på et palindrom antages at være minimum tre karakterer. Der skelnes
 *   mellem store og små bogstaver. Otto er således ikke et palindrom.
 *
 *   Skriv en algoritme, som kan tjekke en tekst for indholdet af palindromer og
 *   returnere antallet. Hvis teksten indeholder et eller flere ulovlige tegn,
 *   returneres -1.
 *
 *   Lovlige tegn:
 *     - Store og små bogstaver i det engelske alfabet: a-z og A-Z
 *     - Følgende tegn . , ; ! ?
 *
 *   For fuld point må der kun bruges str[i] til at hente enkelttegn.
 *   Signatur (C#): public int AntalPalindromer(string tekst)
 *
 *   Eksempler:
 *     "Min hat, den har tre buler. Tre buler har min hat!" -> 0
 *     "Anna betalte sine regninger"                        -> 1 (regninger)
 *     "Olfert+ roede en tur i sin kajak"                   -> -1 (+ er ulovligt)
 *
 *   Analysér algoritmens tidskompleksitet (Store-O).
 *
 * Antagelse: Mellemrum er ikke nævnt som lovligt tegn, men eksemplerne indeholder
 * mellemrum, så mellemrum behandles som lovlig ordadskiller.
 *
 * Tidskompleksitet: O(n), se "Opgave 3 palindromer.md".
 */
public class PalindromTæller
{
    public int AntalPalindromer(string tekst)
    {
        int antal = 0;
        int start = -1; // start-indeks på det ord, vi er i gang med (-1 = intet ord)

        // Vi går én plads ud over enden, så det sidste ord også bliver afsluttet.
        for (int i = 0; i <= tekst.Length; i++)
        {
            bool slut = i == tekst.Length;
            char c = slut ? ' ' : tekst[i];

            if (ErBogstav(c))
            {
                if (start < 0)
                    start = i;          // nyt ord begynder
            }
            else if (ErAdskiller(c))
            {
                if (start >= 0)         // et ord er netop slut: tekst[start..i-1]
                {
                    if (i - start >= 3 && ErPalindrom(tekst, start, i - 1))
                        antal++;
                    start = -1;
                }
            }
            else
            {
                return -1;              // ulovligt tegn
            }
        }
        return antal;
    }

    // To pegepinde mod midten: højst (længde/2) sammenligninger.
    private bool ErPalindrom(string tekst, int venstre, int højre)
    {
        while (venstre < højre)
        {
            if (tekst[venstre] != tekst[højre])
                return false;
            venstre++;
            højre--;
        }
        return true;
    }

    private static bool ErBogstav(char c) =>
        (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

    private static bool ErAdskiller(char c) =>
        c == ' ' || c == '.' || c == ',' || c == ';' || c == '!' || c == '?';
}
