# Opgave 1 (12 %) – Sum af to tal

**Opgave:** Skriv en metode/algoritme med følgende signatur:

```java
bool sumAfToTalLigParameter(int[] arr, int l, int X)
```

Den første parameter er et array af positive, sorterede heltal uden dubletter; anden parameter er længden af arrayet; og tredje parameter er den værdi, der ledes efter.

Metoden skal finde ud af, om der findes to tal i arrayet, hvis sum er lig med parameteren X. Hvis svaret er ja, returneres true. Hvis svaret er nej, returneres false. Det samme tal må ikke bruges to gange.

Opgaven skal løses i to versioner således at:

- Version 1 har kvadratisk tidskompleksitet – O(N²).
- Version 2 har lineær tidskompleksitet O(N), det vil sige, at hvert element i tabellen kun må læses én gang.

De to løsninger tæller lige meget, altså 6 %.

---

## Version 1 – O(N²)

Se `ToTalSum.cs`.

```csharp
public bool SumAfToTalLigParameterV1(int[] arr, int l, int x)
{
    for (int i = 0; i < l - 1; i++)
    {
        for (int j = i + 1; j < l; j++)
        {
            if (arr[i] + arr[j] == x)
                return true;
        }
    }
    return false;
}
```

- Alle par (i, j) med i < j prøves. Fordi j starter ved i + 1, bruges det samme tal aldrig to gange.
- Antal par i værste fald (intet par findes): N(N−1)/2 → **O(N²)**.
- Version 1 bruger ikke, at arrayet er sorteret.

## Version 2 – O(N)

```csharp
public bool SumAfToTalLigParameterV2(int[] arr, int l, int x)
{
    int venstre = 0;
    int højre = l - 1;
    while (venstre < højre)
    {
        int sum = arr[venstre] + arr[højre];
        if (sum == x)
            return true;
        if (sum < x)
            venstre++;
        else
            højre--;
    }
    return false;
}
```

**Idé: to pegepinde** – én i hver ende af det sorterede array.

- **sum = X:** fundet.
- **sum < X:** `arr[venstre]` er for lille til at indgå i *noget* par: dens største mulige partner er `arr[højre]`, og summen er stadig for lille. Den kan altså smides væk → `venstre++`.
- **sum > X:** tilsvarende er `arr[højre]` for stor til at indgå i noget par → `højre--`.

Hvert skridt fjerner ét element fra søgningen, og hvert element læses kun, når en pegepind står på det. Der er højst N − 1 skridt → **O(N)**. Løkken stopper ved `venstre == højre`, så samme tal bruges aldrig to gange.

### Eksempel: arr = {1, 3, 5, 8, 12, 15}, X = 20

| venstre | højre | sum | handling |
|---|---|---:|---|
| 1 | 15 | 16 | < 20 → venstre frem |
| 3 | 15 | 18 | < 20 → venstre frem |
| 5 | 15 | 20 | fundet → **true** |

### Testresultater

Se `TestOpgave1` i `Program.cs`. Begge versioner giver samme svar på alle testtilfælde, inkl. X = 2 · et tal i arrayet (fx X = 10 med 5 i arrayet), hvor svaret er false, fordi 5 ikke må bruges to gange.
