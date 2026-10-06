# Øvelse 2 fra tidligere eksamen (15%) - to hashtabeller

**Opgave:** Etabler to hashtabeller, begge med plads til 16 elementer.

- Tabel 1 (7%): indsæt nøglerne **D, E, M, O, C, R, A, T** med hashfunktionen $(11 \cdot k) \bmod 16$, 
hvor $k$ er bogstavets placering i det engelske alfabet (A=1, …, Z=26). Ved kollision anvendes **linear probing**.
- Tabel 2 (8%): indsæt nøglerne **R, E, P, U, B, L, I, C, A, N** med samme hashfunktion. 
Ved kollision anvendes **quadratic probing**.

Eksempel fra opgaven: hash('C') = hash(3) = 11·3 % 16 = 33 % 16 = 1. Ved kollisioner (et element hasher til et optaget indeks) anvendes probing som angivet.

Alfabetets numre: A1 B2 C3 D4 E5 F6 G7 H8 I9 J10 K11 L12 M13 N14 O15 P16 Q17 R18 S19 T20 U21 V22 W23 X24 Y25 Z26.

---

## Tabel 1 - linear probing

**Hashværdier:** $h(k) = (11k) \bmod 16$

| Bogstav | k | 11k | 11k mod 16 |
|---|---|---|---|
| D | 4  | 44  | 12 |
| E | 5  | 55  | 7 |
| M | 13 | 143 | 15 |
| O | 15 | 165 | 5 |
| C | 3  | 33  | 1 |
| R | 18 | 198 | 6 |
| A | 1  | 11  | 11 |
| T | 20 | 220 | 12 |

**Indsættelse (rækkefølge D, E, M, O, C, R, A, T), linear probing $h_i = (h+i) \bmod 16$:**

| Bogstav | h | Probing | Placeret på |
|---|---|---|---|
| D | 12 | ledig | **12** |
| E | 7  | ledig | **7** |
| M | 15 | ledig | **15** |
| O | 5  | ledig | **5** |
| C | 1  | ledig | **1** |
| R | 6  | ledig | **6** |
| A | 11 | ledig | **11** |
| T | 12 | optaget (D) → 13 ledig | **13** |

**Resultat, Tabel 1:**

| Indeks | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Værdi |  | C |  |  |  | O | R | E |  |  |  | A | D | T |  | M |

---

## Tabel 2 - quadratic probing

**Hashværdier:** $h(k) = (11k) \bmod 16$

| Bogstav | k | 11k | 11k mod 16 |
|---|---|---|---|
| R | 18 | 198 | 6 |
| E | 5  | 55  | 7 |
| P | 16 | 176 | 0 |
| U | 21 | 231 | 7 |
| B | 2  | 22  | 6 |
| L | 12 | 132 | 4 |
| I | 9  | 99  | 3 |
| C | 3  | 33  | 1 |
| A | 1  | 11  | 11 |
| N | 14 | 154 | 10 |

**Indsættelse (rækkefølge R, E, P, U, B, L, I, C, A, N), quadratic probing $h_i = (h+i^2) \bmod 16$:**

| Bogstav | h | Probing | Placeret på |
|---|---|---|---|
| R | 6  | ledig | **6** |
| E | 7  | ledig | **7** |
| P | 0  | ledig | **0** |
| U | 7  | optaget (E) → i=1: 8 ledig | **8** |
| B | 6  | optaget (R) → i=1: 7 optaget (E) → i=2: 10 ledig | **10** |
| L | 4  | ledig | **4** |
| I | 3  | ledig | **3** |
| C | 1  | ledig | **1** |
| A | 11 | ledig | **11** |
| N | 10 | optaget (B) → i=1: 11 optaget (A) → i=2: 14 ledig | **14** |

**Resultat, Tabel 2:**

| Indeks | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Værdi | P | C |  | I | L |  | R | E | U |  | B | A |  |  | N |  |

---

## Bemærkning om tabelstørrelse 16

Da 16 ikke er et primtal, er quadratic probing i teorien ikke garanteret at finde en ledig plads 
(jf. øvelserne fra slidesene, hvor dette demonstreres med et worst-case eksempel). 
I denne opgave går det dog fint, fordi ingen af nøglerne kolliderer så mange gange, 
at man rammer den begrænsede cyklus af offsets ($i^2 \bmod 16 \in \{0,1,4,9\}$ for $i=0,1,2,3$, 
hvorefter mønsteret gentager sig).
