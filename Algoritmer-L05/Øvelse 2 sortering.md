# Øvelse 2 – sortering (lektion 5/6)

## Opgaven

> Ordene stale og least er anagrammer, dvs. de indeholder nøjagtig de samme karakterer. Skriv en metode, som kan afgøre, om to ord (strings) er anagrammer.

Koden ligger i `AnagramChecker.cs`, og testene i `Program.cs`.

## Løsning

Først et hurtigt tjek: har ordene forskellig længde, kan de ikke være anagrammer. Derefter er der to gode måder at gøre det på.

### Udgave 1 – sortér bogstaverne, O(N log N)

To ord er anagrammer, præcis når de er ens efter at have fået sorteret deres bogstaver:

```
stale  →  a e l s t
least  →  a e l s t   → ens, altså anagrammer
```

```csharp
char[] ca = a.ToCharArray();
char[] cb = b.ToCharArray();
Array.Sort(ca);
Array.Sort(cb);
// sammenlign ca og cb tegn for tegn
```

Sorteringen koster O(N log N), sammenligningen O(N), så i alt **O(N log N)**. Det er den løsning, der passer til kapitlets emne.

### Udgave 2 – tæl bogstaverne, O(N)

Samme idé som `bucketSort()` fra slides: i stedet for at sammenligne tæller vi forekomster.

1. Gå gennem `a` og læg 1 til tælleren for hvert tegn.
2. Gå gennem `b` og træk 1 fra for hvert tegn.
3. Bliver en tæller negativ, har `b` flere af det tegn end `a`, og det er ikke et anagram.

```csharp
var counts = new Dictionary<char, int>();
foreach (char c in a)
    counts[c] = counts.GetValueOrDefault(c) + 1;

foreach (char c in b)
{
    int n = counts.GetValueOrDefault(c) - 1;
    if (n < 0) return false;
    counts[c] = n;
}
return true;
```

Hvert tegn behandles én gang, og hvert opslag i en `Dictionary` er O(1) i gennemsnit, så køretiden er **O(N)**.

Hvorfor er det nok at tjekke for negative tællere? Ordene har samme længde, så der er lagt lige så meget til som trukket fra. Hvis ingen tæller blev negativ, må alle derfor ende på 0.

## Sammenligning

| | Sortering | Optælling |
|---|---|---|
| Tid | O(N log N) | O(N) |
| Ekstra plads | O(N) (kopierede tegn-arrays) | O(k), k = antal forskellige tegn |
| Idé fra kapitlet | Sammenligningsbaseret sortering | bucketSort |

## Bemærkninger

- `Dictionary<char, int>` bruges i stedet for et `int[26]`, så æ, ø, å og andre tegn også virker.
- Store og små bogstaver tælles som forskellige tegn (`"Stale"` og `"least"` giver `false`). Skal det ignoreres, kan man kalde `ToLowerInvariant()` på begge strenge først.
