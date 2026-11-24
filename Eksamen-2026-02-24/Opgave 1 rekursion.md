# Opgave 1 (20 %) – Rekursion

## 1.1 (9 %)

**Opgave:** Skriv en rekursiv algoritme, som kan returnere summen af elementerne i et array bestående af heltal.

Kaldt med arrayet {15,17,6,21,8} returneres 67.

### Løsning

Se `ArraySum.cs`. Kaldes med `Sum(arr, 0)`.

```csharp
public int Sum(int[] arr, int index)
{
    if (index >= arr.Length)
        return 0;
    return arr[index] + Sum(arr, index + 1);
}
```

- **Basistilfælde:** `index` er forbi sidste element. Summen af ingenting er 0.
- **Rekursivt tilfælde:** Det aktuelle element plus summen af resten. Hvert kald rykker `index` én frem, så basistilfældet nås altid.
- **Tidskompleksitet:** N + 1 kald med konstant arbejde → **O(N)**.

## 1.2 (11 %)

**Opgave:** Skriv en rekursiv algoritme, der kaldes med et array af heltal og et bestemt heltal. Antallet af forekomster af dette heltal i arrayet skal returneres.

Eksempel: i arrayet {12,4,88,19,6,28,4,39,22} forekommer 4 to gange og 37 nul gange.

### Løsning

Se `ForekomstTæller.cs`. Kaldes med `Antal(arr, tal, 0)`.

```csharp
public int Antal(int[] arr, int tal, int index)
{
    if (index >= arr.Length)
        return 0;
    int her = arr[index] == tal ? 1 : 0;
    return her + Antal(arr, tal, index + 1);
}
```

- **Basistilfælde:** `index` er forbi sidste element → 0 forekomster.
- **Rekursivt tilfælde:** 1, hvis det aktuelle element er tallet (ellers 0), plus antallet i resten.
- **Tidskompleksitet:** **O(N)**.
