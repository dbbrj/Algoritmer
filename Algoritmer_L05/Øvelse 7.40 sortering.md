# Opgave 7.40* – sortering (lektion 5/6)

## Opgaven

> The following divide-and-conquer algorithm is proposed for finding the simultaneous maximum and minimum: If there is one item, it is the maximum and minimum, and if there are two items, then compare them and in one comparison you can find the maximum and minimum. Otherwise, split the input into two halves, divided as evenly as possibly (if N is odd, one of the two halves will have one more element than the other). Recursively find the maximum and minimum of each half, and then in two additional comparisons produce the maximum and minimum for the entire problem.
>
> a. Suppose N is a power of 2. What is the exact number of comparisons used by this algorithm?
>
> b. Suppose N is of the form 3 · 2ᵏ. What is the exact number of comparisons used by this algorithm?
>
> c. Modify the algorithm as follows: When N is even, but not divisible by four, split the input into sizes of N/2 − 1 and N/2 + 1. What is the exact number of comparisons used by this algorithm?

Koden ligger i `MaxMinFinder.cs`. Den implementerer algoritmen og tæller sammenligninger, og `Program.cs` tjekker svarene nedenfor.

## Rekurrensen

Lad T(N) være antallet af sammenligninger:

```
T(1) = 0
T(2) = 1
T(N) = T(⌈N/2⌉) + T(⌊N/2⌋) + 2      for N > 2
```

## a. N = 2ᵏ

Begge halvdele har størrelsen N/2:

```
T(N) = 2T(N/2) + 2,   T(2) = 1
```

Vi folder den ud (teleskopering, som med mergesort):

```
T(N) = 2T(N/2) + 2
     = 2(2T(N/4) + 2) + 2         = 4T(N/4) + 4 + 2
     = 8T(N/8) + 8 + 4 + 2
     ...
     = (N/2)·T(2) + (N/2 + ... + 8 + 4 + 2)
```

Efter k − 1 halveringer er vi nede på T(2), og der er N/2 af dem. Den geometriske sum er 2 + 4 + ... + N/2 = N − 2.

```
T(N) = N/2 · 1 + (N − 2)
```

**Svar: T(N) = 3N/2 − 2**

Tjek: T(2) = 3 − 2 = 1 ✓, T(4) = 2·1 + 2 = 4 = 6 − 2 ✓, T(8) = 2·4 + 2 = 10 = 12 − 2 ✓.

## b. N = 3 · 2ᵏ

Igen deles der hele vejen ned i lige store halvdele, men nu ender vi i T(3) i stedet for T(2):

```
T(3) = T(2) + T(1) + 2 = 1 + 0 + 2 = 3
```

Udfoldning som i a, men med 2ᵏ kopier af T(3) i bunden og summen 2 + 4 + ... + 2ᵏ = 2ᵏ⁺¹ − 2:

```
T(3·2ᵏ) = 2ᵏ · T(3) + (2ᵏ⁺¹ − 2)
        = 3·2ᵏ + 2·2ᵏ − 2
        = 5·2ᵏ − 2
```

Da N = 3·2ᵏ er 2ᵏ = N/3:

**Svar: T(N) = 5N/3 − 2**

Tjek: T(3) = 5 − 2 = 3 ✓, T(6) = 2·3 + 2 = 8 = 10 − 2 ✓, T(12) = 2·8 + 2 = 18 = 20 − 2 ✓.

Bemærk, at 5N/3 er **flere** sammenligninger end 3N/2. Problemet er bunden: et delproblem på 3 elementer koster 3 sammenligninger, altså 1 pr. element, mens delproblemer på 2 kun koster ½ pr. element. Det er det, c løser.

## c. Den modificerede algoritme

Når N er lige men ikke delelig med 4, er N/2 ulige. Ved at dele i N/2 − 1 og N/2 + 1 bliver **begge halvdele lige**. Så er alle delproblemer lige hele vejen ned, og bunden er altid T(2), aldrig T(3) eller T(1).

**Påstand:** for alle lige N er T(N) = 3N/2 − 2.

**Bevis (induktion):**

- **Basis:** T(2) = 1 = 3·2/2 − 2 ✓
- **N delelig med 4:** halvdelene er N/2 og N/2, begge lige.

  T(N) = 2(3(N/2)/2 − 2) + 2 = 3N/2 − 4 + 2 = 3N/2 − 2 ✓
- **N lige, ikke delelig med 4:** halvdelene er N/2 − 1 og N/2 + 1, begge lige.

  T(N) = (3(N/2 − 1)/2 − 2) + (3(N/2 + 1)/2 − 2) + 2 = 3N/2 − 4 + 2 = 3N/2 − 2 ✓

**Svar:** for N = 3·2ᵏ med k ≥ 1 (dvs. N lige) bruger den modificerede algoritme **3N/2 − 2** sammenligninger, altså 9·2ᵏ⁻¹ − 2. For N = 3 (k = 0) er N ulige, modifikationen gør ikke noget, og svaret er stadig 3.

Tjek: N = 6 deles i 2 + 4: T = 1 + 4 + 2 = 7 = 9 − 2 ✓. N = 12 deles i 6 + 6: T = 7 + 7 + 2 = 16 = 18 − 2 ✓.

## Hvorfor er det interessant?

Den naive metode (find max med N − 1 sammenligninger og derefter min med N − 1) bruger 2N − 2. Med den modificerede algoritme bruges kun 3N/2 − 2, og man kan bevise, at **⌈3N/2⌉ − 2 er den nedre grænse** for at finde både max og min med sammenligninger. Algoritmen i c er altså optimal for alle lige N.

## Sammenfatning

| | Antal sammenligninger |
|---|---|
| Naiv (max og min hver for sig) | 2N − 2 |
| a. N = 2ᵏ | 3N/2 − 2 |
| b. N = 3·2ᵏ | 5N/3 − 2 |
| c. Modificeret, N lige | 3N/2 − 2 (optimal) |

Formlerne er tjekket ved at tælle sammenligninger i koden for alle lige N op til 3000.
