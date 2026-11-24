# Opgave 7 (15 %) – Dijkstra og minimum spanning tree

**Opgave:** Betragt nedenstående graf.

Bemærk: den lille plet på billedet mellem noderne f og h er ikke et minus. Vægten mellem f og h er 2.

Kanterne (orienterede, aflæst fra figuren):

| Kant | Vægt | | Kant | Vægt | | Kant | Vægt |
|---|---:|---|---|---:|---|---|---:|
| s → a | 5 | | f → b | 2 | | b → e | 5 |
| s → h | 2 | | f → h | 2 | | b → g | 7 |
| h → a | 2 | | f → e | 6 | | e → g | 2 |
| a → b | 7 | | h → e | 9 | | g → c | 3 |
| a → f | 3 | | b → c | 8 | | c → d | 4 |

I nedenstående tabel er angivet en fejlbehæftet slutkonfiguration for traversering af grafen ved anvendelse af *Dijkstras algoritme* med start i node s. Første del af opgaven går ud på at rette konfigurationen, så den bliver korrekt.

| v | known | d<sub>v</sub> | p<sub>v</sub> |
|---|---|---:|---|
| S | false | 0 | 0 |
| A | false | 5 | s |
| B | false | 12 | a |
| C | false | 20 | b |
| D | false | 20 | c |
| E | false | 11 | h |
| F | false | 7 | a |
| G | false | 13 | e |
| H | false | 2 | a |

Anden del af opgaven er at udarbejde et *minimum spanning tree* for grafen, hvis den var undirected. Angiv hvilken algoritme, der er anvendt og træets vægt.

Du behøver ikke at tegne grafen; men du skal, under alle omstændigheder, skrive, i hvilken rækkefølge kanterne (edges) tilføjes. En god løsning kunne derfor være noget lignende nedenstående (som vedrører en anden graf end ovenstående):

| Kant nr. | Fra | Til | Vægt |
|---:|---|---|---:|
| 1 | C | F | 2 |
| 2 | H | J | 3 |
| 3 | B | D | 3 |

---

## Del 1: Dijkstra fra s

Dijkstra gør hver gang den ukendte node med mindst d<sub>v</sub> *known* og opdaterer dens naboer, hvis vejen gennem den er kortere. Se `Dijkstra.cs` og `TestOpgave7` i `Program.cs`.

| Skridt | Gøres known | d | Opdaterer |
|---:|---|---:|---|
| 1 | s | 0 | a = 5 (s), h = 2 (s) |
| 2 | h | 2 | a = 2+2 = **4** (h), e = 2+9 = 11 (h) |
| 3 | a | 4 | b = 4+7 = 11 (a), f = 4+3 = 7 (a) |
| 4 | f | 7 | b = 7+2 = **9** (f); e: 7+6 = 13 > 11, ingen ændring |
| 5 | b | 9 | c = 9+8 = 17 (b), g = 9+7 = 16 (b); e: 14 > 11 |
| 6 | e | 11 | g = 11+2 = **13** (e) |
| 7 | g | 13 | c = 13+3 = **16** (g) |
| 8 | c | 16 | d = 16+4 = 20 (c) |
| 9 | d | 20 | – |

### Rettet slutkonfiguration

| v | known | d<sub>v</sub> | p<sub>v</sub> | Rettelse |
|---|---|---:|---|---|
| s | **true** | 0 | – | known |
| a | **true** | **4** | **h** | var 5 / s: s → h → a = 2 + 2 = 4 er kortere end s → a = 5 |
| b | **true** | **9** | **f** | var 12 / a: s → h → a → f → b = 2 + 2 + 3 + 2 = 9 |
| c | **true** | **16** | **g** | var 20 / b: s → h → e → g → c = 2 + 9 + 2 + 3 = 16 (via b: 9 + 8 = 17) |
| d | **true** | 20 | c | known (dv og pv var rigtige) |
| e | **true** | 11 | h | known (dv og pv var rigtige) |
| f | **true** | 7 | a | known (dv og pv var rigtige) |
| g | **true** | 13 | e | known (dv og pv var rigtige) |
| h | **true** | 2 | **s** | pv var a: h nås direkte fra s med vægt 2 |

Fejlene var altså: alle `known` skal være **true** i slutkonfigurationen, og A, B, C og H havde forkerte værdier (H kun i p<sub>v</sub>).

Korteste veje (følg p<sub>v</sub> baglæns):

- a: s → h → a (4)
- b: s → h → a → f → b (9)
- c: s → h → e → g → c (16)
- d: s → h → e → g → c → d (20)
- e: s → h → e (11)
- f: s → h → a → f (7)
- g: s → h → e → g (13)
- h: s → h (2)

## Del 2: Minimum spanning tree (uorienteret graf) – Kruskal

Kruskal sorterer alle kanter efter vægt og tager dem én ad gangen. En kant tages med, hvis den forbinder to *forskellige* komponenter (dvs. ikke laver en kreds). Grafen har 9 noder, så træet skal have V − 1 = **8 kanter**. Se `Kruskal.cs`.

| Skridt | Kant | Vægt | Valgt? | Komponenter efter |
|---:|---|---:|---|---|
| 1 | s–h | 2 | ✓ | {s,h} |
| 2 | h–a | 2 | ✓ | {s,h,a} |
| 3 | f–b | 2 | ✓ | {s,h,a} {f,b} |
| 4 | f–h | 2 | ✓ | {s,h,a,f,b} |
| 5 | e–g | 2 | ✓ | {s,h,a,f,b} {e,g} |
| 6 | a–f | 3 | ✗ kreds (a og f er begge i {s,h,a,f,b}) | |
| 7 | g–c | 3 | ✓ | {s,h,a,f,b} {e,g,c} |
| 8 | c–d | 4 | ✓ | {s,h,a,f,b} {e,g,c,d} |
| 9 | s–a | 5 | ✗ kreds | |
| 10 | b–e | 5 | ✓ | alle 9 noder – færdig |

### Svar

| Kant nr. | Fra | Til | Vægt |
|---:|---|---|---:|
| 1 | s | h | 2 |
| 2 | h | a | 2 |
| 3 | f | b | 2 |
| 4 | f | h | 2 |
| 5 | e | g | 2 |
| 6 | g | c | 3 |
| 7 | c | d | 4 |
| 8 | b | e | 5 |

**Algoritme:** Kruskal. **Træets vægt:** 2 + 2 + 2 + 2 + 2 + 3 + 4 + 5 = **22**.

Rækkefølgen blandt de fem kanter med vægt 2 kan byttes rundt, men træet er det samme, fordi alle fem kommer med. MST'et er entydigt: a–f (3) og s–a (5) laver kreds med kanter af vægt 2, og b–e (5) er den eneste letteste kant mellem de to sidste komponenter (de andre er f–e 6, b–g 7, b–c 8 og h–e 9).

### Kontrol med Prim (start i s)

s–h (2), h–a (2), h–f (2), f–b (2), b–e (5), e–g (2), g–c (3), c–d (4) → samme 8 kanter, vægt **22**.
