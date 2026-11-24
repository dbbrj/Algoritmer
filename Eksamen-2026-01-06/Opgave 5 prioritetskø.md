# Opgave 5 (5 %) – Prioritetskø

**Opgave:** Hvorfor kan nedenstående tabel *ikke* repræsentere en prioritetskø?

I øverste række er prioriteterne angivet; i nederste række står de tilsvarende tabelindeks.

| Prioritet | | 11 | 17 | 28 | 23 | 68 | 49 | 29 | 25 | 56 | 97 | 106 | 118 | 48 | 65 | 76 | 69 | 27 | 68 | 62 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Indeks** | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 |

---

## Løsning

En prioritetskø i en tabel er en binær hob (heap) med roden på indeks 1. Børnene til indeks *i* ligger på 2*i* og 2*i*+1, og **hob-ordenen** kræver, at en forælder aldrig er større end sine børn (min-hob).

Alle forælder–barn-par tjekkes:

| Forælder (indeks: værdi) | Børn | OK? |
|---|---|---|
| 1: 11 | 2: 17, 3: 28 | ✓ |
| 2: 17 | 4: 23, 5: 68 | ✓ |
| 3: 28 | 6: 49, 7: 29 | ✓ |
| 4: 23 | 8: 25, 9: 56 | ✓ |
| 5: 68 | 10: 97, 11: 106 | ✓ |
| **6: 49** | 12: 118, **13: 48** | **✗** |
| 7: 29 | 14: 65, 15: 76 | ✓ |
| 8: 25 | 16: 69, 17: 27 | ✓ |
| 9: 56 | 18: 68, 19: 62 | ✓ |

**Svar:** Indeks 6 (prioritet 49) har barnet på indeks 13 (= 2·6 + 1) med prioritet **48 < 49**. Hob-ordenen er brudt, så tabellen kan ikke være en binær hob/prioritetskø.

(Det kan heller ikke være en max-hob, da roden 11 er mindre end sine børn.)
