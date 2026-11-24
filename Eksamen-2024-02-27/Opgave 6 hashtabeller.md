# Opgave 6 (15 %) – Hashtabeller

**Opgave:** Denne opgave går ud på at etablere to hashtabeller, begge med plads til 16 elementer.

I den første (7 %) skal elementerne med nøglerne D,E,M,O,C,R,A,T indsættes.

Hashfunktionen er

(11\*k) % tableSize

hvor k er tallets placering/nummer i det engelske alfabet (se opgave 3).

Eksempel: hash('C') = hash(3) = 11\*3%16=33%16=1

Ved kollisioner (et element hasher til et optaget indeks) anvendes *linear probing*.

I den anden tabel (8 %) skal elementerne med nøglerne R,E,P,U,B,L,I,C,A,N indsættes.

Der anvendes samme hashfunktion som i den første og *quadratic probing* ved kollisioner.

---

## Fremgangsmåde

- **Linear probing:** ved kollision prøves hash + 1, hash + 2, hash + 3, … (mod 16).
- **Quadratic probing:** ved kollision prøves hash + 1², hash + 2², hash + 3², … = hash + 1, hash + 4, hash + 9, … (mod 16).

Simuleringen ligger i `HashTabel.cs` og køres i `TestOpgave6` i `Program.cs`.

## Tabel 1: D, E, M, O, C, R, A, T – linear probing

| Nøgle | k | 11·k | hash = 11·k % 16 | Prøvet | Indsat på |
|---|---:|---:|---:|---|---:|
| D | 4 | 44 | 12 | 12 | **12** |
| E | 5 | 55 | 7 | 7 | **7** |
| M | 13 | 143 | 15 | 15 | **15** |
| O | 15 | 165 | 5 | 5 | **5** |
| C | 3 | 33 | 1 | 1 | **1** |
| R | 18 | 198 | 6 | 6 | **6** |
| A | 1 | 11 | 11 | 11 | **11** |
| T | 20 | 220 | 12 | 12 (D) → 13 | **13** |

Kun én kollision: T hasher til 12, hvor D står, og lander på næste plads, 13.

| Indeks | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Nøgle** | | C | | | | O | R | E | | | | A | D | T | | M |

## Tabel 2: R, E, P, U, B, L, I, C, A, N – quadratic probing

| Nøgle | k | 11·k | hash = 11·k % 16 | Prøvet | Indsat på |
|---|---:|---:|---:|---|---:|
| R | 18 | 198 | 6 | 6 | **6** |
| E | 5 | 55 | 7 | 7 | **7** |
| P | 16 | 176 | 0 | 0 | **0** |
| U | 21 | 231 | 7 | 7 (E) → 7+1 = 8 | **8** |
| B | 2 | 22 | 6 | 6 (R) → 6+1 = 7 (E) → 6+4 = 10 | **10** |
| L | 12 | 132 | 4 | 4 | **4** |
| I | 9 | 99 | 3 | 3 | **3** |
| C | 3 | 33 | 1 | 1 | **1** |
| A | 1 | 11 | 11 | 11 | **11** |
| N | 14 | 154 | 10 | 10 (B) → 10+1 = 11 (A) → 10+4 = 14 | **14** |

Tre kollisioner: U, B og N. Bemærk, at quadratic probing altid regner ud fra den **oprindelige** hashværdi (hash + i²), ikke fra den sidst prøvede plads.

| Indeks | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| **Nøgle** | P | C | | I | L | | R | E | U | | B | A | | | N | |
