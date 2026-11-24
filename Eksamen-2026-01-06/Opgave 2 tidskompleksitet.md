# Opgave 2 (20 %) – Tidskompleksitet

**Opgave:** Betragt følgende algoritme:

```java
public static long myMethod(long N) {
    long x = 0;
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < N; j += j+1) {
            for (int k = 0; k < N; k +=(i+j+5)) {
                x++; } } if (i > 1024) i++; } return x;
}
```

Giv dit bedste bud på algoritmens tidskompleksitet i Store-O notationen. Dit svar skal begrundes detaljeret, helst ved hjælp af testresultater.

---

## Svar: O(N log² N)

### Løkke for løkke

**i-løkken:** Op til i = 1024 tælles i op med 1. Derefter tælles i op med 2 (`i++` i kroppen + `i++` i løkken). For store N kører den altså ca. N/2 gange, hvilket stadig er **Θ(N)**. Konstanten ændrer ikke store-O.

**j-løkken:** `j += j+1` betyder j ← 2j + 1, så j løber 0, 1, 3, 7, 15, …, 2ᵐ − 1. Løkken stopper, når 2ᵐ − 1 ≥ N, dvs. efter ca. **log₂ N** gennemløb.

**k-løkken:** k tælles op med (i + j + 5), så den kører ⌈N / (i + j + 5)⌉ gange. Det afhænger af både i og j, så man kan *ikke* bare gange de tre løkker sammen.

### Summen

Antal gange `x++` udføres:

$$
T(N) \approx \sum_{m=0}^{\log N} \sum_{i=0}^{N} \frac{N}{i + 2^m + 4}
$$

For fast j = 2ᵐ − 1 er den inderste sum en harmonisk række:

$$
\sum_{i=0}^{N} \frac{1}{i + 2^m + 4} \approx \ln\frac{N + 2^m}{2^m} \approx (\log_2 N - m)\cdot \ln 2
$$

Summeres over m = 0 … log N:

$$
T(N) \approx N \cdot \ln 2 \cdot \sum_{m=0}^{\log N} (\log N - m) \approx N \cdot \ln 2 \cdot \frac{(\log N)^2}{2}
$$

Altså **T(N) = Θ(N log² N)**.

(Overhead fra j-løkken selv er N · log N, og den er domineret af ovenstående.)

### Testresultater

Antallet af `x++` er talt for N = 2⁸ … 2¹⁸ (se `TestOpgave2` i `Program.cs`):

| N | x | x / (N log N) | x / (N log² N) |
|---:|---:|---:|---:|
| 256 | 6 959 | 3,398 | 0,425 |
| 1 024 | 42 446 | 4,145 | 0,415 |
| 4 096 | 197 722 | 4,023 | 0,335 |
| 16 384 | 945 883 | 4,124 | 0,295 |
| 65 536 | 4 516 764 | 4,308 | 0,269 |
| 262 144 | 21 388 479 | 4,533 | 0,252 |

- x / (N log N) **bliver ved med at stige**. Væksten er altså hurtigere end N log N.
- x / (N log² N) **flader ud mod en konstant**. Den falder lidt, fordi halveringen af i-løkken efter i = 1024 fylder mere og mere, når N vokser (teoretisk grænse ≈ ln 2 / 4 ≈ 0,17).
- Hver gang N firedobles, ganges x med ca. 4,5–4,8, ikke 16 (som O(N²) ville give).

Konklusion: **O(N log² N)**.
