# Opgave 5 (15 %) – Prioritetskø

**Opgave:** En prioritetskø kan repræsenteres af et array, hvor indeks 0 altid har værdien 0 og som udgangspunkt ikke benyttes.

Betragt følgende array:

{0,9,23,106,10,36,38,98,84,12,14,50,55,35,68}

Dette array udgør ikke en prioritetskø.

Opgaven går ud på at finde de elementer, som forbryder sig imod/ødelægger heap-order, og at angive det interval af værdier, de skulle antage, for at (gen)skabe heap-order. Det forudsættes, at der ikke må være dubletter i en prioritetskø.

Hvis det opdaterede array, altså prioritetskøen, (minus indeks 0) blev implementeret/set som et binært træ, hvilke egenskaber ville dette træ så have?

---

## Løsning

Binær min-hob med roden på indeks 1: børnene til indeks *i* ligger på 2*i* og 2*i*+1. **Heap-order:** en forælder skal være mindre end sine børn (strengt mindre, da der ikke må være dubletter).

| Indeks | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Værdi** | 9 | 23 | 106 | 10 | 36 | 38 | 98 | 84 | 12 | 14 | 50 | 55 | 35 | 68 |

Som træ:

```
                    9
              /           \
           23               106
         /    \           /      \
       10      36       38        98
      /  \    /  \     /  \      /
    84   12  14  50   55   35   68
```

### Trin 1: tjek alle forælder–barn-par

| Forælder (indeks: værdi) | Børn | OK? |
|---|---|---|
| 1: 9 | 2: 23, 3: 106 | ✓ |
| **2: 23** | **4: 10**, 5: 36 | **✗** 10 < 23 |
| **3: 106** | **6: 38**, **7: 98** | **✗** begge børn < 106 |
| 4: 10 | 8: 84, 9: 12 | ✓ |
| **5: 36** | **10: 14**, 11: 50 | **✗** 14 < 36 |
| **6: 38** | 12: 55, **13: 35** | **✗** 35 < 38 |
| **7: 98** | **14: 68** | **✗** 68 < 98 |

Der er altså 6 brud på heap-order.

### Trin 2: hvilke elementer er det, der er forkerte?

Ved hvert brud kan man vælge at ændre forælderen eller barnet. De fleste brud kan rettes ved at ændre ét tal, men ét sted kan det ikke:

**Bruddet 23 → 10 (indeks 2 og 4).** Indeks 4 har barnet 12, og indeks 2 har forælderen 9.

- Hvis kun **23** ændres, skal den ligge mellem 9 og 10 – der findes intet heltal dér.
- Hvis kun **10** ændres, skal den være over 23 men under barnet 12 – umuligt.

Derfor skal der **to** ændringer til i det område. Det mest naturlige er at ændre **10** *og* **12**, så de begge bliver større end 23.

Resten rettes med én ændring pr. brud. Med 6 ændringer i alt (det er det mindst mulige) kan man fx vælge disse elementer som de forkerte:

| Indeks | Nuværende | Hvorfor forkert | Interval for ny værdi | Valgt |
|---:|---:|---|---|---:|
| 3 | 106 | større end begge børn 38 og 98 | 9 < x < 38 → **10–37**, ikke 23, 36 eller andre tal i køen | **20** |
| 4 | 10 | mindre end forælderen 23 | 23 < x < 84 og x < indeks 9 → **24–83** | **24** |
| 9 | 12 | mindre end (den nye) forælder på indeks 4 | x > indeks 4 (her 24) → **25 og op** | **25** |
| 10 | 14 | mindre end forælderen 36 | x > 36 → **37 og op** (blad) | **37** |
| 13 | 35 | mindre end forælderen 38 | x > 38 → **39 og op** (blad) | **39** |
| 14 | 68 | mindre end forælderen 98 | x > 98 → **99 og op** (blad) | **99** |

Bladene (indeks 8–14) har ingen børn, så de har kun en nedre grænse. Valgte værdier må ikke være dubletter af tal, der allerede står i køen (fx ikke 50, 55 eller 84).

> Andre løsninger er også gyldige, fx at ændre forælderen 38 (indeks 6) i stedet for barnet 35, eller 98 (indeks 7) i stedet for 68. Et brute-force-tjek af alle kombinationer viser, at **6 ændringer er det mindst mulige**, og at **106 skal ændres i alle løsninger**.

### Rettet array

{0, 9, 23, **20**, **24**, 36, 38, 98, 84, **25**, **37**, 50, 55, **39**, **99**}

```
                    9
              /           \
           23               20
         /    \           /      \
       24      36       38        98
      /  \    /  \     /  \      /
    84   25  37  50   55   39   99
```

Alle forældre er mindre end deres børn, og der er ingen dubletter (tjekket i `TestOpgave5` i `Program.cs`).

### Trin 3: træets egenskaber

Set som binært træ (uden indeks 0) har prioritetskøen disse egenskaber:

- **Komplet binært træ** (*structure property*): alle niveauer er fyldt helt ud, undtagen det nederste, som er fyldt fra venstre mod højre. Det er derfor, træet kan ligge i et array uden huller, og børnene til *i* altid ligger på 2*i* og 2*i*+1 (forælderen på ⌊*i*/2⌋).
- **Heap-order** (*heap-order property*): hver node er mindre end sine børn. Dermed ligger **minimum altid i roden** (indeks 1), og `findMin` er O(1).
- **14 noder, højde 3** (⌊log₂ 14⌋ = 3). Niveauerne har 1, 2, 4 og 7 noder. Højden er altid O(log N), så `insert` og `deleteMin` er O(log N).
- Det er **ikke** et binært søgetræ: der er ingen orden mellem venstre og højre undertræ (fx er 20 til højre for 23).
