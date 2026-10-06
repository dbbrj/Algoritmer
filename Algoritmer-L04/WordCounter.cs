using System.Collections.Generic;
using System.Text;

/*
 * Opgave 1 (anslået løsningstid 60 min.)
 *
 * Skriv en algoritme, der har en tekst (string) som eneste parameter og returnerer det oftest
 * forekommende ord i teksten.
 *
 * Du kan antage, at der er tale om almindelig tekst (små og store bogstaver i det engelske alfabet),
 * og at de anvendte skilletegn mellem ordene er komma, punktum eller mellemrum (se eksemplet
 * nedenfor). Du kan antage, at der altid er et mellemrum mellem to ord, og at det sidste tegn er et
 * punktum. Ord antages at være ens, selv om nogle forekomster begynder med et lille bogstav og
 * andre med et stort.
 *
 * Precondition for din algoritme er således, at teksten kun kan indeholde små bogstaver [a-z],
 * store bogstaver [A-Z], blanke/mellemrum, kommaer (,) og punktummer (.).
 *
 * Eksempel:
 * {The cattle were running back and forth, but there was no wolf to be seen, heard or smelled, so the
 * shepherd decided to take a little nap in a bed of grass and early summer flowers. Soon he was
 * awakened by a sound he had never heard before.}
 * Det korrekte svar i eksemplet er ordet a, som forekommer tre gange.
 *
 * Fra string-klassen er det kun tilladt at anvende metoder, som kan trække en enkeltkarakter ud af
 * teksten, og metoder som kan angive længden af teksten. Opdelingen i ord skal således foretages
 * ved 'håndkraft'.
 *
 * ---------------------------------------------------------------------------------------------
 * Løsning: teksten læses tegn for tegn (kun text[i] og text.Length), ord tælles i en Dictionary.
 * Tidskompleksitet: forventet O(L), hvor L er tekstens længde.
 */
public static class WordCounter
{
    // Returnerer det oftest forekommende ord (uden forskel på store/små bogstaver).
    // Teksten læses tegn for tegn; kun text[i] og text.Length bruges fra string-klassen.
    public static string MostFrequentWord(string text)
    {
        var counts = new Dictionary<string, int>();
        var current = new StringBuilder();
        string best = "";
        int bestCount = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c >= 'A' && c <= 'Z')
            {
                current.Append((char)(c + ('a' - 'A'))); // manuel konvertering til lille bogstav
            }
            else if (c >= 'a' && c <= 'z')
            {
                current.Append(c);
            }
            else if (current.Length > 0) // skilletegn (mellemrum, komma, punktum) afslutter et ord
            {
                string word = current.ToString();
                current.Clear();

                counts.TryGetValue(word, out int n);
                n++;
                counts[word] = n;

                if (n > bestCount)
                {
                    bestCount = n;
                    best = word;
                }
            }
        }

        return best;
    }
}
