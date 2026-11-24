# Opgave 4 (8 %) – Prioritetskø

**Opgave:** Nedenstående array skal forestille en prioritetskø.

I øverste række er prioriteterne angivet; i nederste række står de tilsvarende tabelindeks.

Der er imidlertid to fejl (hvor *heap order* ikke er overholdt). Find fejlene og indsæt andre tal, så tabellen bliver en prioritetskø (*heap order* overholdt).

| Prioritet | | 11 | 17 | 28 | 23 | 68 | 49 | 29 | 22 | 56 | 97 | 106 | 118 | 60 | 65 | 76 | 69 | 27 | 67 | 54 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Indeks** | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 |

---

## Løsning

Binær min-hob med roden på indeks 1: børnene til indeks *i* ligger på 2*i* og 2*i*+1, og en forælder må ikke være større end sine børn.

| Forælder (indeks: værdi) | Børn | OK? |
|---|---|---|
| 1: 11 | 2: 17, 3: 28 | ✓ |
| 2: 17 | 4: 23, 5: 68 | ✓ |
| 3: 28 | 6: 49, 7: 29 | ✓ |
| **4: 23** | **8: 22**, 9: 56 | **✗** 22 < 23 |
| 5: 68 | 10: 97, 11: 106 | ✓ |
| 6: 49 | 12: 118, 13: 60 | ✓ |
| 7: 29 | 14: 65, 15: 76 | ✓ |
| 8: 22 | 16: 69, 17: 27 | ✓ |
| **9: 56** | 18: 67, **19: 54** | **✗** 54 < 56 |

### Fejl 1: indeks 4 (23) og indeks 8 (22)

Ret enten

- **indeks 8** til et tal mellem 23 og 27 (≥ forælderen 23, ≤ børnene 69 og 27), fx **25**, eller
- **indeks 4** til et tal mellem 17 og 22 (≥ forælderen 17, ≤ børnene 22 og 56), fx **20**.

### Fejl 2: indeks 9 (56) og indeks 19 (54)

Ret enten

- **indeks 19** til et tal ≥ 56 (bladet har ingen børn), fx **58**, eller
- **indeks 9** til et tal mellem 17 og 54 (≥ forælderen 17, ≤ børnene 67 og 54), fx **50**.

### Rettet tabel (indeks 8 → 25, indeks 19 → 58)

| Prioritet | | 11 | 17 | 28 | 23 | 68 | 49 | 29 | **25** | 56 | 97 | 106 | 118 | 60 | 65 | 76 | 69 | 27 | 67 | **58** |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Indeks** | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 |
