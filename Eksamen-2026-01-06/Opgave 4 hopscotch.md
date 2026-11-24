# Opgave 4 (15 %) – Hopscotch hashing

**Opgave:** Nedenfor til venstre er hopscotch tabel. I kolonnerne til højre er angivet de indsatte elementers værdi og deres placering.

| Indeks | Værdi | Hop |
|---:|:---:|:---:|
| 22 | | 0000 |
| 23 | F | 1000 |
| 24 | | 0000 |
| 25 | B | 1000 |
| 26 | | 0000 |
| 27 | D | 1100 |
| 28 | L | 1000 |
| 29 | H | 1000 |
| 30 | | 0000 |
| 31 | A | 1010 |
| 32 | I | 1000 |
| 33 | J | 0000 |
| 34 | C | 1000 |
| 35 | | 0000 |
| 36 | K | 1000 |
| 37 | | 0000 |
| 38 | | 0000 |
| 39 | E | 1000 |
| 40 | | 0000 |
| 41 | G | 1000 |
| 42 | | 0000 |

| Værdi | Indeks (hash) |
|:---:|---:|
| A | 31 |
| B | 25 |
| C | 34 |
| D | 27 |
| E | 39 |
| F | 23 |
| G | 41 |
| H | 29 |
| I | 32 |
| J | 31 |
| K | 36 |
| L | 27 |

Der er én fejl i tabellen. Den skal du finde og rette.

Dernæst bedes du indsætte nedenstående elementer i tabellen og gøre rede for dine overvejelser.

| Værdi | Indeks |
|:---:|---:|
| M | 31 |
| N | 35 |
| O | 26 |
| P | 39 |
| Q | 27 |

Du behøver kun at aflevere slutkonfigurationen af tabellen.

---

## Løsning

Hop-felterne har 4 bit, så neighborhood-størrelsen er **H = 4**. Bit *i* (talt fra venstre, i = 0..3) i hop[b] er sat, hvis plads b+i indeholder et element, der hasher til b.

### Fejlen

Alle hop-felter tjekkes:

- hop[27] = 1100: D på 27 og L på 28 hasher begge til 27. ✓
- hop[31] = 1010: A på 31 og J på 33 hasher begge til 31. ✓
- **hop[28] = 1000** siger, at elementet på plads 28 hasher til 28. Men L hasher til 27, og det er allerede registreret i hop[27]. Intet element hasher til 28.

**Rettelse: hop[28] = 0000.**

### Indsættelser

**M (hash 31):** Vinduet 31–34 er fuldt (A, I, J, C). Første ledige plads er 35, og 35 − 31 = 4 ≥ H, så den ligger uden for M's vindue. Der skal "hoppes":
- Vi leder efter et element på pladserne 32–34, som må flyttes til 35, dvs. hvis hjem h opfylder 35 − h ≤ 3. Vi starter med den fjerneste kandidat, 32.
- I på plads 32 hasher til 32. Afstand 35 − 32 = 3 er OK. I flyttes til 35: hop[32] = 0000 → **0001**.
- Plads 32 er nu fri og ligger i M's vindue (offset 1). M indsættes på 32: hop[31] = 1010 → **1110**.

**N (hash 35):** 35 er optaget (I), 36 er optaget (K), 37 er ledig (offset 2). N indsættes på 37: hop[35] = **0010**.

**O (hash 26):** 26 er ledig. O indsættes på 26: hop[26] = **1000**.

**P (hash 39):** 39 er optaget (E), 40 er ledig (offset 1). P indsættes på 40: hop[39] = **1100**.

**Q (hash 27):** 27 (D), 28 (L) og 29 (H) er optaget, 30 er ledig (offset 3). Q indsættes på 30: hop[27] = 1100 → **1101**.

### Slutkonfiguration

| Indeks | Værdi | Hop |
|---:|:---:|:---:|
| 22 | | 0000 |
| 23 | F | 1000 |
| 24 | | 0000 |
| 25 | B | 1000 |
| 26 | O | 1000 |
| 27 | D | 1101 |
| 28 | L | 0000 |
| 29 | H | 1000 |
| 30 | Q | 0000 |
| 31 | A | 1110 |
| 32 | M | 0001 |
| 33 | J | 0000 |
| 34 | C | 1000 |
| 35 | I | 0010 |
| 36 | K | 1000 |
| 37 | N | 0000 |
| 38 | | 0000 |
| 39 | E | 1100 |
| 40 | P | 0000 |
| 41 | G | 1000 |
| 42 | | 0000 |
