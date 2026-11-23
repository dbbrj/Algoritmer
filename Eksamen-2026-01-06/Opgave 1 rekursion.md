# Opgave 1 (15 %) – Rekursion

## 1.1 (6 %)

**Opgave:** Skriv en rekursiv algoritme, som kan returnere værdien af N! (N fakultet).
Kaldt med N = 5, returneres 120 (5\*4\*3\*2\*1).

### Løsning

Se `Fakultet.cs`.

```csharp
public long Beregn(int n)
{
    if (n <= 1)
        return 1;
    return n * Beregn(n - 1);
}
```

- **Basistilfælde:** 0! = 1! = 1.
- **Rekursivt tilfælde:** N! = N · (N−1)!. Hvert kald gør problemet én mindre, så rekursionen når altid basistilfældet.
- **Tidskompleksitet:** N kald med konstant arbejde → **O(N)**.

`long` bruges, fordi `int` løber over allerede ved 13!.

## 1.2 (9 %)

**Opgave:** Skriv en rekursiv algoritme, som kan afgøre, om et array er sorteret eller ej.

Signaturforslag Java og C#: `boolean isSorted(int[] arr, int index)`

- `{23,44,61,90,112}` returnerer true.
- `{23,44,31,90,112}` returnerer false.

### Løsning

Se `SorteringsTjek.cs`.

```csharp
public bool IsSorted(int[] arr, int index)
{
    if (index >= arr.Length - 1)
        return true;
    if (arr[index] > arr[index + 1])
        return false;
    return IsSorted(arr, index + 1);
}
```

- **Basistilfælde:** Står vi ved sidste element (eller er arrayet tomt), er resten trivielt sorteret.
- **Rekursivt tilfælde:** Arrayet er sorteret fra `index`, hvis `arr[index] <= arr[index+1]` og resten fra `index+1` er sorteret.
- Kaldes med `IsSorted(arr, 0)`.
- **Tidskompleksitet:** Højst N−1 kald → **O(N)**.
