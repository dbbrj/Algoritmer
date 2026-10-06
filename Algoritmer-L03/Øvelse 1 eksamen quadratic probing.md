# Øvelse 1 fra tidligere eksamen (15%) - quadratic probing

**Opgave:** Nedenstående tabel er en hashtabel, hvor quadratic probing anvendes til håndtering af kollisioner. 
Hashfunktionen er $h(x) = x \bmod 11$ (tabelstørrelse 11). 
Følgende værdier er indsat i tabellen: 22, 5, 16 og 27 (i den rækkefølge).

| Indeks | Værdi |
|---|---|
| 0 | 22 |
| 1 |  |
| 2 |  |
| 3 |  |
| 4 |  |
| 5 | 5 |
| 6 | 16 |
| 7 |  |
| 8 |  |
| 9 | 27 |
| 10 |  |

Bekræft at tabellen er korrekt, og ret eventuelle fejl. 
Vis derefter hvordan tabellen ser ud, efter at elementerne 1, 12, 23, din alder og dit eksamensnummer er blevet indsat, 
og forklar hvorledes det er gået til.

Probe-sekvensen ved quadratic probing er:

$$h_i(x) = (h(x) + i^2) \bmod 11, \quad i = 0, 1, 2, 3, \dots$$

---

## Trin 1: Bekræft den opgivne tabel

| Indsat | h(x) | Probing | Resultat |
|---|---|---|---|
| 22 | 0 | i=0 → 0 (ledig) | placeret på **0** ✓ |
| 5  | 5 | i=0 → 5 (ledig) | placeret på **5** ✓ |
| 16 | 5 | i=0 → 5 (optaget af 5), i=1 → 6 (ledig) | placeret på **6** ✓ |
| 27 | 5 | i=0 → 5 (optaget), i=1 → 6 (optaget af 16), i=2 → 9 (ledig) | placeret på **9** ✓ |

Alt stemmer med den opgivne tabel - **tabellen er korrekt, der er ingen fejl at rette.**

---

## Trin 2: Indsæt 1, 12, 23

| Indsat | h(x) | Probing | Resultat |
|---|---|---|---|
| 1  | 1 | i=0 → 1 (ledig) | placeret på **1** |
| 12 | 1 | i=0 → 1 (optaget af 1), i=1 → 2 (ledig) | placeret på **2** |
| 23 | 1 | i=0 → 1 (optaget), i=1 → 2 (optaget af 12), i=2 → 5 (optaget af 5), i=3 → 10 (ledig) | placeret på **10** |

---

## Trin 3: Indsæt alder (39) og eksamensnummer (215662605)

**Alder: 39**

$h(39) = 39 \bmod 11 = 6$

| i | Prøvet indeks | Status |
|---|---|---|
| 0 | 6 | optaget (16) |
| 1 | 7 | ledig ✓ |

→ **39 placeres på indeks 7**

**Eksamensnummer: 215662605**

$11 \times 19\,605\,691 = 215\,662\,601 \Rightarrow 215\,662\,605 - 215\,662\,601 = 4$

$h(215662605) = 4$

| i | Prøvet indeks | Status |
|---|---|---|
| 0 | 4 | ledig ✓ |

→ **215662605 placeres på indeks 4** (direkte hit, ingen kollision)

---

## Slutresultat

| Indeks | Værdi |
|---|---|
| 0 | 22 |
| 1 | 1 |
| 2 | 12 |
| 3 |  |
| 4 | 215662605 |
| 5 | 5 |
| 6 | 16 |
| 7 | 39 |
| 8 |  |
| 9 | 27 |
| 10 | 23 |

Kun indeks 3 og 8 er stadig ledige - tabellen er nu 8 ud af 11 pladser fyldt ($\lambda \approx 0{,}73$).
