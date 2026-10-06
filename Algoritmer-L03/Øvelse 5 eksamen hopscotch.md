# Øvelse 5 fra tidligere eksamen (15%) - Hopscotch hashing

**Opgave:** Nedenstående figur viser en tom hopscotch hashtabel med indeks 44–57 og 4-bit "hop"-info pr. plads 
(neighborhood-størrelse **H = 4**). Indsæt følgende elementer i tabellen og opdater hoppen i overensstemmelse hermed:

| Værdi | Hasher til indeks |
|---|---|
| A | 47 |
| B | 51 |
| C | 55 |
| D | 51 |
| E | 46 |
| F | 51 |
| G | 49 |
| H | 53 |
| I | 50 |
| J | 51 |

Beskriv derefter problemet, der opstår, hvis næste indsættelse hasher til indeks 50.

---

## Hvordan hopscotch-indsættelse virker

Hop-info for en beholder *b* er en bitvektor med H bit (her 4), hvor bit *i* er sat, 
hvis beholder *b+i* indeholder et element, hvis **hjemme-position (home)** er *b*.

Ved indsættelse af nøgle x med home *j*:
1. Find nærmeste ledige plads via lineær probing fra *j* og frem - kald den `free`.
2. Så længe `free - j ≥ H`: find en beholder *i* i intervallet `[free-H+1, free-1]` 
(start fra den, der ligger længst fra `free`), som har et element i sin egen neighborhood, 
der ligger *før* `free`, og som kan flyttes til `free` uden at overskride H fra sit eget home. 
Flyt det element til `free`, og sæt `free` til dets gamle plads. Gentag.
3. Når `free - j < H`: placer x på `free`, og sæt bit `(free - j)` i home *j*'s hop-info.

Hvis der ikke findes noget flytbart element i trin 2, **fejler indsættelsen** - det er præcis det, 
der sker i sidste del af opgaven.

---

## Indsættelse trin for trin

| Indsat | Home | Free fundet ved lineær probing | Swap nødvendig? | Placeret på |
|---|---|---|---|---|
| A | 47 | 47 (ledig) | nej | 47 |
| B | 51 | 51 (ledig) | nej | 51 |
| C | 55 | 55 (ledig) | nej | 55 |
| D | 51 | 52 (ledig) | nej (offset 1 < 4) | 52 |
| E | 46 | 46 (ledig) | nej | 46 |
| F | 51 | 53 (ledig) | nej (offset 2 < 4) | 53 |
| G | 49 | 49 (ledig) | nej | 49 |
| H | 53 | 54 (ledig) | nej (offset 1 < 4) | 54 |
| I | 50 | 50 (ledig) | nej | 50 |
| J | 51 | 56 (ledig, men offset 5 ≥ 4!) | **ja** | se nedenfor |

**Detalje for J (home = 51):** lineær probing finder først ledig plads på 56 - 
men det er 5 pladser fra home 51, som er ≥ H(4). Der kræves derfor et hopscotch-swap:

- Kig i intervallet [53, 55] (dvs. `free-H+1` til `free-1`), startende længst fra `free`.
- Beholder 53 har hop-bit for offset 1 sat (H sidder på plads 54, med home 53). Position 54 < free(56), 
- så H kan flyttes til 56 (offset 56−53 = 3 < 4 ✓).
- H flyttes fra 54 til 56. Home 53's hop opdateres: bit for offset 1 ryddes, bit for offset 3 sættes.
- `free` bliver nu 54, og 54 − 51 = 3 < 4 → indsættelsen kan nu fuldføres: **J placeres på 54**, 
- og home 51's hop-bit for offset 3 sættes.

---

## Tabellen efter alle 10 indsættelser

| Indeks | Værdi | Hop (home = dette indeks) |
|---|---|---|
| 44 | - | - |
| 45 | - | - |
| 46 | E | 1000 |
| 47 | A | 1000 |
| 48 | - | - |
| 49 | G | 1000 |
| 50 | I | 1000 |
| 51 | B | **1111** (B@51, D@52, F@53, J@54) |
| 52 | D | 0000 |
| 53 | F | **0001** (H@56, home 53, offset 3) |
| 54 | J | 0000 |
| 55 | C | 1000 |
| 56 | H | 0000 |
| 57 | - | - |

Beholder 51's hop-bitmap (1111) fortæller, at alle fire elementer med home 51 (B, D, F, J) 
ligger inden for tabellens fire beholdere 51–54 - selvom de fysisk er spredt ud over 51, 52, 53 og 54.

---

## Problemet ved næste indsættelse med home = 50

Antag den næste nøgle K hasher til indeks 50 (som allerede indeholder I).

**Trin 1 - lineær probing efter ledig plads:** 50, 51, 52, 53, 54, 55, 56 er alle optaget. 
Første ledige plads er **57**. Afstand: 57 − 50 = 7, hvilket er langt over H = 4.

**Trin 2 - forsøg på hopscotch-swap (intervallet [54, 56]):**
- i = 54: hop[54] = 0000 → intet at flytte.
- i = 55: hop[55] = 1000 → C (home 55) sidder på plads 55, hvilket er < free(57). 
C kan flyttes til 57 (offset 57−55 = 2 < 4 ✓). 
Swap udføres: C flyttes til 57, hop[55] opdateres til 0010, og `free` bliver nu 55.

`free − home` = 55 − 50 = 5, stadig ≥ 4 → endnu et swap-forsøg er nødvendigt.

**Trin 3 - nyt swap-forsøg (intervallet [52, 54]):**
- i = 52: hop[52] = 0000 → intet at flytte.
- i = 53: hop[53] = 0001 → H (home 53) sidder på plads 56 (offset 3). 
Men 56 er **ikke** mindre end det nye `free` (55) - H ligger længere ude end den ledige plads, 
vi prøver at fylde, så det element kan ikke bruges.
- i = 54: hop[54] = 0000 → intet at flytte.

**Der findes ingen flytbar kandidat i intervallet.** Swap-processen går i stå, 
og indsættelsen af K (home 50) **kan ikke gennemføres** - 
selvom tabellen slet ikke er fuld (44, 45, 48 og nu 55 står stadig ledige).

### Konklusion

Dette er den klassiske svaghed ved hopscotch hashing: en indsættelse kan fejle, 
ikke fordi tabellen er fuld, men fordi der ikke findes nogen kæde af lovlige flytninger, 
der kan bringe en ledig plads inden for H beholdere fra nøglens home - 
selv når der er rigeligt med ledig plads *andre steder* i tabellen (fx 44, 45, 48). 
I praksis håndteres dette ved at **forstørre tabellen (resize/rehash)**, ligesom man gør ved cuckoo hashing, 
når en cyklus opstår. Forskellen er, at hopscotch fejler pga. manglende flytbare naboer inden for neighborhood-vinduet, 
mens cuckoo hashing fejler pga. en egentlig cyklus i "smid ud"-kæden.
