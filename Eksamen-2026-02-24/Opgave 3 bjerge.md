# Opgave 3 (30 %) – Bjerge i et array

**Opgave:** I en tabel/array af heltal definerer vi et *bjerg*, som en række af tal, hvor minimum to stigende tal er efterfulgt af minimum to faldende, fx {12,17,14,11}.

Opgaven går ud på at skrive tre algoritmer med følgende egenskaber:

1. Indeholder arrayet et bjerg (true/false)? (5 %)
2. Hvor mange bjerge indeholder arrayet? (10 %)
3. Hvad er længden på det længste bjerg i arrayet? (15 %)

**Eksempler** (bjergene er markeret med fed)

- Eksempel 1: {12,34,56,1,3,19,12,55,78,34,48,6,53,19,20,7,11} – ingen bjerge
- Eksempel 2: {6,**4,11,17,19,9,5,2**,99,11,66,4,14,51,6,7,88,76} – ét bjerg; længde: 7
- Eksempel 3: {13,**6,67,55,44**,55,89,95,**69,74,36,19,6**,71,44,61,16} – to bjerge; det længste: 5

Et tal kan godt indgå i flere bjerge: I denne tabel {17,29,12,6,7,12,10,8,25,23,38,17,19} indgår tallet 6 både i {17,29,12,6} og i {6,7,12,10,8}.

Hvis du løser opgaven i C++, er det tilladt at have størrelsen på arrayet med som parameter til metoden.

---

## Løsning

Se `BjergFinder.cs`.

### Fortolkning

Ud fra eksemplet {12,17,14,11}:

- **"To stigende tal"** = 12, 17, dvs. **mindst ét skridt op**.
- **"To faldende tal"** = 14, 11 efter toppen, dvs. **mindst to skridt ned**.
- Et bjerg tages så langt som muligt: fra dalen før opstigningen til bunden af nedstigningen (jf. eksempel 2, hvor længden er 7: 4 → 19 → 2).
- To ens nabotal bryder et bjerg.
- **Længde** = antal tal i bjerget = skridt op + skridt ned + 1.

Tjek af eksemplerne med denne fortolkning:

- Eks. 1: Alle nedstigninger er kun ét skridt (56→1, 19→12, 78→34, 48→6, 53→19, 20→7) → 0 bjerge ✓
- Eks. 2: 4→11→17→19 (3 op), 19→9→5→2 (3 ned) → længde 7. Resten har kun ét skridt ned → 1 bjerg ✓
- Eks. 3: {6,67,55,44} (1 op, 2 ned, længde 4) og {69,74,36,19,6} (1 op, 3 ned, længde 5) → 2 bjerge, længste 5 ✓

### Idé

Arrayet gennemløbes én gang i "op-ned-faser":

1. Gå frem, så længe tallene stiger, og tæl skridt op.
2. Gå derefter frem, så længe tallene falder, og tæl skridt ned.
3. Er der ≥ 1 op og ≥ 2 ned, er det et bjerg.
4. Fortsæt fra bunden. Bunden er ikke "brugt", så den kan være dal for næste bjerg. Derfor kan et tal indgå i to bjerge (6 i det sidste eksempel).
5. Er der hverken op eller ned (to ens tal), gå ét skridt frem.

```csharp
private static int Stig(int[] a, ref int i)
{
    int skridt = 0;
    while (i < a.Length && a[i] > a[i - 1]) { skridt++; i++; }
    return skridt;
}

private static int Fald(int[] a, ref int i)
{
    int skridt = 0;
    while (i < a.Length && a[i] < a[i - 1]) { skridt++; i++; }
    return skridt;
}
```

De tre metoder bruger samme skelet og adskiller sig kun i, hvad de gør, når de finder et bjerg:

```csharp
// 1. Findes der et bjerg?
public bool HarBjerg(int[] a)
{
    int i = 1;
    while (i < a.Length)
    {
        int op = Stig(a, ref i);
        int ned = Fald(a, ref i);
        if (op >= 1 && ned >= 2) return true;
        if (op == 0 && ned == 0) i++;
    }
    return false;
}

// 2. Hvor mange?
public int AntalBjerge(int[] a)
{
    int antal = 0, i = 1;
    while (i < a.Length)
    {
        int op = Stig(a, ref i);
        int ned = Fald(a, ref i);
        if (op >= 1 && ned >= 2) antal++;
        if (op == 0 && ned == 0) i++;
    }
    return antal;
}

// 3. Længste?
public int LængsteBjerg(int[] a)
{
    int længste = 0, i = 1;
    while (i < a.Length)
    {
        int op = Stig(a, ref i);
        int ned = Fald(a, ref i);
        if (op >= 1 && ned >= 2) længste = Math.Max(længste, op + ned + 1);
        if (op == 0 && ned == 0) i++;
    }
    return længste;
}
```

### Test

| Array | Har bjerg | Antal | Længste |
|---|---|---:|---:|
| Eksempel 1 | false | 0 | 0 |
| Eksempel 2 | true | 1 | 7 |
| Eksempel 3 | true | 2 | 5 |
| {17,29,12,6,7,12,10,8,25,23,38,17,19} | true | 2 | 5 |
| {12,17,14,11} | true | 1 | 4 |
| {1,2,2,1,0} | false | 0 | 0 |

### Tidskompleksitet: O(n)

`i` starter på 1 og bliver kun større, enten i `Stig`, i `Fald` eller med `i++` ved ens tal. Hver gennemgang af while-løkken flytter `i` mindst ét skridt. Der er derfor højst n − 1 skridt i alt, og hvert skridt koster konstant tid → **O(n)** for alle tre metoder. Det er optimalt, da alle tal skal ses mindst én gang.
