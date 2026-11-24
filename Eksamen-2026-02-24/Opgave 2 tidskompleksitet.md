# Opgave 2 (15 %) – Tidskompleksitet

**Opgave:** Betragt følgende algoritme:

```java
public static int myMethod(int N)
{
    int x = 0;
    for (int i = 0; i < N * Math.sqrt(N); i++)
    {
        for(int j = 0; j < N; j++)
        {
            for (int k = 0; k < N * 10; k++)
            {
                x++;
            }
            j *= 2;
        }
    }

    return x;
}
```

Giv dit bedste bud på algoritmens tidskompleksitet i Store-O notationen. Dit svar skal begrundes detaljeret, gerne ved hjælp af testresultater.

---

## Svar: O(N^2,5 · log N)

### Løkke for løkke

**i-løkken** kører N · √N = **N^1,5** gange. i ændres ikke inde i løkken.

**j-løkken** er fælden. Efter den inderste løkke udføres `j *= 2`, og bagefter `j++` i løkkehovedet. Pr. gennemløb sker altså j ← 2j + 1:

| gennemløb | j ved start |
|---:|---:|
| 1 | 0 |
| 2 | 1 |
| 3 | 3 |
| 4 | 7 |
| 5 | 15 |
| m | 2^(m−1) − 1 |

Løkken stopper, når j ≥ N, dvs. efter ca. **log₂ N + 1** gennemløb. Den kører altså **ikke** N gange.

**k-løkken** kører **10 · N** gange, uafhængigt af i og j. Konstanten 10 forsvinder i store-O.

### Samlet

De tre løkker er uafhængige af hinanden, så de kan ganges sammen:

$$
T(N) = N^{1,5} \cdot (\log_2 N + 1) \cdot 10N \approx 10 \cdot N^{2,5} \log_2 N
$$

**T(N) = O(N^2,5 · log N)**

### Testresultater

Antallet af `x++` for N = 16 … 1024 (se `TestOpgave2` i `Program.cs`; x er gjort til `long`, da `int` løber over ved N = 1024):

| N | x | x / N^2,5 | x / (N^2,5 · log N) |
|---:|---:|---:|---:|
| 16 | 51 200 | 50,0 | 12,50 |
| 64 | 2 293 760 | 70,0 | 11,67 |
| 256 | 94 371 840 | 90,0 | 11,25 |
| 1 024 | 3 690 987 520 | 110,0 | 11,00 |

- x / N^2,5 **vokser** med præcis 20 hver gang N firedobles (dvs. log₂ N stiger med 2). Det er log-faktoren × 10, der viser sig.
- x / (N^2,5 · log N) **går mod konstanten 10**. Den er lidt over 10 pga. "+1"-gennemløbet af j-løkken (10 · (log N + 1) / log N).

Konklusion: **O(N^2,5 · log N)**.
