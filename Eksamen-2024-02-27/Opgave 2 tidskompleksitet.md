# Opgave 2 (10 %) – Tidskompleksitet

**Opgave:** Hvad er Store O (Big-Oh) tidskompleksiteten for nedenstående metode? Begrund dit svar.

```java
public static long myM(int N) {
    long x = 0;
    for (int i = 0; i < 10000; i++) {
        for (int j = 0; j < N*10; j++) {
            for (int k = N; k > 0; k = k/10) {
                x++;
            }
        }
    }
    return x;
}
```

En brugbar strategi for at løse opgaven, udover at analysere koden, kan være at lave nogle eksperimenter med koden, hvor du tæller antal af operationer, dvs. ser på returværdien for forskellige værdier af parameteren (N) for at skitsere et estimat af Store O tidskompleksiteten.

---

## Svar: O(N log N)

### Løkke for løkke

**i-løkken** kører **10 000** gange, uanset N. Det er en konstant, så den forsvinder i store-O (men den gør programmet langsomt i praksis).

**j-løkken** kører **10 · N** gange. Konstanten 10 forsvinder → **N**.

**k-løkken** er den interessante. k starter i N og *divideres* med 10 hver gang (heltalsdivision), indtil k bliver 0:

| N | k-værdier | gennemløb |
|---:|---|---:|
| 9 | 9 | 1 |
| 10 | 10, 1 | 2 |
| 100 | 100, 10, 1 | 3 |
| 1 000 | 1000, 100, 10, 1 | 4 |
| 12 345 | 12345, 1234, 123, 12, 1 | 5 |

Antallet af gennemløb er antallet af cifre i N, dvs. **⌊log₁₀ N⌋ + 1**. Den kører altså **log N** gange (grundtallet er ligegyldigt i store-O, da log₁₀ N = log₂ N / log₂ 10).

### Samlet

Løkkerne er uafhængige af hinanden, så de ganges sammen:

$$
T(N) = 10\,000 \cdot 10N \cdot (\lfloor \log_{10} N \rfloor + 1) = 100\,000 \cdot N \cdot (\lfloor \log_{10} N \rfloor + 1)
$$

Konstanterne 10 000 og 10 forsvinder:

**T(N) = O(N log N)**

### Testresultater

Antallet af `x++` (returværdien) for forskellige N, se `TestOpgave2` i `Program.cs`:

| N | x | x / N | x / (N · (⌊log₁₀ N⌋+1)) |
|---:|---:|---:|---:|
| 9 | 900 000 | 100 000 | 100 000 |
| 10 | 2 000 000 | 200 000 | 100 000 |
| 99 | 19 800 000 | 200 000 | 100 000 |
| 100 | 30 000 000 | 300 000 | 100 000 |
| 1 000 | 400 000 000 | 400 000 | 100 000 |
| 10 000 | 5 000 000 000 | 500 000 | 100 000 |

- **x / N vokser** med 100 000 hver gang N får et ciffer mere (bliver 10 gange større). Det er log-faktoren, der viser sig. Algoritmen er altså *ikke* lineær.
- **x / (N · antal cifre i N) er præcis 100 000** for alle N, dvs. konstanten 10 000 · 10.

Bemærk springet fra N = 99 til N = 100: N er kun vokset med ca. 1 %, men x er vokset med 50 %, fordi k-løkken får et gennemløb mere (fra 2 til 3).

Konklusion: **O(N log N)**.
