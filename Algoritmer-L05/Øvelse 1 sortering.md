# Øvelse 1 – sortering (lektion 5/6)

## Opgaven

> En tabel indeholder N sorterede heltal. Opgaven går ud på at afgøre om tabellen indeholder to tal, hvis sum er lig med parameteren X. Metoden skal skrives i to udgaver: én med kvadratisk tidskompleksitet og en med lineær.

Koden ligger i `TwoSum.cs`, og testene i `Program.cs`.

## Løsning

### Kvadratisk udgave – O(N²)

Prøv alle par `(i, j)` med `i < j`, og se om `a[i] + a[j] == X`.

```csharp
for (int i = 0; i < a.Length - 1; i++)
    for (int j = i + 1; j < a.Length; j++)
        if (a[i] + a[j] == x)
            return true;
return false;
```

Antallet af par er N(N−1)/2, så i værste fald (intet par findes) er køretiden O(N²). Denne udgave bruger slet ikke, at tabellen er sorteret.

### Lineær udgave – O(N)

Her udnytter vi, at tabellen er sorteret. Vi har to pegere: `left` starter i begyndelsen og `right` i slutningen.

- `a[left] + a[right] == X`: vi har fundet parret.
- Summen er **for lille**: det eneste, der kan gøre den større, er at flytte `left` til højre.
- Summen er **for stor**: det eneste, der kan gøre den mindre, er at flytte `right` til venstre.
- Mødes pegerne, findes der intet par.

```csharp
int left = 0, right = a.Length - 1;
while (left < right)
{
    long sum = (long)a[left] + a[right];
    if (sum == x) return true;
    else if (sum < x) left++;
    else right--;
}
return false;
```

Hver iteration flytter én peger ét skridt mod den anden, så der er højst N−1 iterationer: **O(N)**.

### Hvorfor går vi ikke glip af et par?

Antag, at summen er for lille. Så er `a[left]` plus det **største** tal, den kan parres med (`a[right]`), stadig mindre end X. Derfor kan `a[left]` ikke indgå i noget par, og vi kan trygt kassere den. Tilsvarende for `a[right]`, når summen er for stor. Vi smider altså kun tal væk, som beviseligt ikke kan bruges.

### Eksempel

Tabel `[1, 3, 4, 8, 11, 15, 20]`, X = 12:

| left | right | a[left] + a[right] | handling |
|---|---|---|---|
| 0 | 6 | 1 + 20 = 21 | for stor → right-- |
| 0 | 5 | 1 + 15 = 16 | for stor → right-- |
| 0 | 4 | 1 + 11 = 12 | fundet |

## Bemærkninger

- Summen beregnes som `long`, så to store `int`'er ikke giver overflow.
- "To tal" er fortolket som to forskellige pladser i tabellen, så `[1, ...]` med X = 2 giver `false` (1 må ikke bruges to gange). `left < right` sikrer det.
- Er tabellen **ikke** sorteret, kan man sortere den først (O(N log N)) og så bruge den lineære metode. Det er stadig bedre end O(N²).
