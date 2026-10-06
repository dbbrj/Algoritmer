# Eksamensopgave – 5 % (9 min.) – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

## Opgaven

Tabellen kan *ikke* repræsentere en prioritetskø, hvor enhver node skal være mindre end alle sine efterkommere (en min-heap). Heap-order fejler ét sted. I hvilket indeks?

| Indeks | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Værdi | 14 | 17 | 16 | 28 | 22 | 65 | 29 | 31 | 30 | 26 | 23 | 89 | 64 | 35 | 32 | 48 | 47 | 46 | 45 |

## Løsning

Gå fra `i = 2` til `19`, og tjek at `a[i] ≥ a[i/2]`:

| i | a[i] | i/2 | a[i/2] | OK? |
|---|---|---|---|---|
| 2 | 17 | 1 | 14 | ✓ |
| 3 | 16 | 1 | 14 | ✓ |
| 4 | 28 | 2 | 17 | ✓ |
| 5 | 22 | 2 | 17 | ✓ |
| 6 | 65 | 3 | 16 | ✓ |
| 7 | 29 | 3 | 16 | ✓ |
| 8 | 31 | 4 | 28 | ✓ |
| 9 | 30 | 4 | 28 | ✓ |
| 10 | 26 | 5 | 22 | ✓ |
| 11 | 23 | 5 | 22 | ✓ |
| 12 | 89 | 6 | 65 | ✓ |
| **13** | **64** | **6** | **65** | **✗** |
| 14 | 35 | 7 | 29 | ✓ |
| 15 | 32 | 7 | 29 | ✓ |
| 16 | 48 | 8 | 31 | ✓ |
| 17 | 47 | 8 | 31 | ✓ |
| 18 | 46 | 9 | 30 | ✓ |
| 19 | 45 | 9 | 30 | ✓ |

Det relevante deltræ:

```
        16          (index 3)
       /  \
     65    29       (index 6, 7)
    /  \
  89    64          (index 12, 13)
```

**Svar:** Heap-order fejler mellem **index 6 (65)** og dets barn **index 13 (64)**, fordi 64 < 65.

Med opgavens definition ("enhver node skal være mindre end alle sine efterkommere") er det noden på **index 6**, der bryder reglen, da 65 ikke er mindre end sin efterkommer 64. Fejlen opdages ved **index 13**, hvor barnet er mindre end sin forælder.

Til eksamen er det sikrest at skrive begge dele: *"Heap-order fejler ved index 6/13: værdien 65 på index 6 er større end sit barn 64 på index 13."*
