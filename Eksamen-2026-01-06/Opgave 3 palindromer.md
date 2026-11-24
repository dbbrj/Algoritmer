# Opgave 3 (35 %) – Palindromer

**Opgave:** Et *palindrom* er et ord som staves ens, når man læser det forfra, og når man læser det bagfra.

Eksempler: **otto mellem kajak**

Længden på et palindrom antages at være minimum tre karakterer. Der skelnes mellem store og små bogstaver. **Otto** er således *ikke* et palindrom.

Opgaven går ud på at skrive en algoritme, som kan tjekke en tekst for indholdet af palindromer og returnere antallet. Hvis teksten indeholder et eller flere ulovlige tegn, returneres -1.

Lovlige tegn:

- Store og små bogstaver i det engelske alfabet: a-z og A-Z
- Følgende tegn . , ; ! ? (punktum, komma, semikolon, udråbstegn og spørgsmålstegn)

Hvis der skal opnås fuld points, må der kun anvendes metoder fra `string/String`-klassen til fremfinding af enkeltkarakterer i en tekststreng:

- Java: `charAt()`
- C++: `at()`
- C#: kantet parentes med position, fx `str[i]`

Forslag til signatur (C#): `public int AntalPalindromer(string tekst)`

Eksempler:

- "Min hat, den har tre buler. Tre buler har min hat!" returnerer 0 – ingen palindromer.
- "Anna betalte sine regninger" returnerer 1 (regninger); Anna er *ikke* et palindrom.
- "Olfert+ roede en tur i sin kajak" returnerer -1; + er et ulovligt tegn.

Endelig skal du analysere din algoritmes tidskompleksitet og angive hvilken Store-O (Big-Oh), den tilhører.

---

## Løsning

Se `PalindromTæller.cs`.

**Antagelse:** Mellemrum står ikke på listen over lovlige tegn, men alle eksemplerne indeholder mellemrum. Mellemrum behandles derfor som et lovligt tegn, der skiller ord ad (ligesom . , ; ! ?).

### Idé

Teksten gennemløbes én gang fra venstre mod højre:

1. Er tegnet et bogstav, og er vi ikke allerede i et ord, huskes `start = i`.
2. Er tegnet en adskiller, og er vi i et ord, er ordet `tekst[start..i-1]` slut. Er det mindst 3 langt, tjekkes det med to pegepinde (forfra og bagfra mod midten). Er det et palindrom, tælles `antal` op.
3. Er tegnet hverken bogstav eller adskiller, returneres -1 med det samme.

Løkken går én plads ud over enden (`i <= tekst.Length`) og lader som om, der står et mellemrum dér. Så bliver det sidste ord også afsluttet.

Kun `tekst[i]` bruges til at hente enkelttegn (ingen `Split`, `Substring`, `Reverse` osv.).

```csharp
public int AntalPalindromer(string tekst)
{
    int antal = 0;
    int start = -1;

    for (int i = 0; i <= tekst.Length; i++)
    {
        bool slut = i == tekst.Length;
        char c = slut ? ' ' : tekst[i];

        if (ErBogstav(c))
        {
            if (start < 0)
                start = i;
        }
        else if (ErAdskiller(c))
        {
            if (start >= 0)
            {
                if (i - start >= 3 && ErPalindrom(tekst, start, i - 1))
                    antal++;
                start = -1;
            }
        }
        else
        {
            return -1;
        }
    }
    return antal;
}

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
```

### Test

| Tekst | Forventet | Begrundelse |
|---|---:|---|
| "Min hat, den har tre buler. Tre buler har min hat!" | 0 | ingen af ordene er palindromer |
| "Anna betalte sine regninger" | 1 | regninger; Anna fejler på A ≠ a |
| "Olfert+ roede en tur i sin kajak" | −1 | + er ulovligt |
| "otto mellem kajak" | 3 | alle tre ord |
| "aa bob, wow!" | 2 | "aa" er for kort |

### Tidskompleksitet: O(n)

Lad n være tekstens længde.

- Hovedløkken kører n + 1 gange og laver konstant arbejde pr. tegn (sammenligninger, tildelinger).
- `ErPalindrom` kaldes én gang pr. ord og laver højst (ordlængde / 2) sammenligninger. Ordene overlapper ikke, så alle kaldene tilsammen laver højst n / 2 sammenligninger.

I alt: (n + 1) + n/2 = **O(n)**. Det er optimalt, da hvert tegn skal læses mindst én gang for at finde ulovlige tegn.
