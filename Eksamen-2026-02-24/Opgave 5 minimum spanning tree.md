# Opgave 5 (15 %) – Minimum spanning tree

**Opgave:** Nedenfor ses en graf med 10 vertices/noder og 18 edges/kanter.

Kanterne (aflæst fra figuren):

| Kant | Vægt | | Kant | Vægt | | Kant | Vægt |
|---|---:|---|---|---:|---|---|---:|
| a–b | 6 | | b–g | 2 | | g–i | 14 |
| a–c | 1 | | b–d | 3 | | j–i | 5 |
| a–d | 2 | | c–d | 2 | | h–i | 9 |
| a–e | 8 | | d–h | 15 | | h–f | 4 |
| e–h | 8 | | g–j | 19 | | f–i | 9 |
| e–f | 11 | | g–h | 8 | | e–i | 2 |

Opgaven går ud på at etablere et *minimum spanning tree* (MST).

Løsningen skal være en liste af edges i den rækkefølge, som den anvendte algoritme foreskriver (der kan være flere løsninger, eftersom flere edges har samme vægt).

Du skal desuden angive, hvilken algoritme du har anvendt samt træets samlede vægt.

---

## Løsning: Kruskals algoritme

Kruskal sorterer alle kanter efter vægt og tager dem én ad gangen. En kant tages med, hvis den forbinder to *forskellige* komponenter (dvs. ikke laver en kreds). Komponenterne holdes styr på med union-find. Algoritmen stopper, når der er valgt V − 1 = 9 kanter.

| # | Kant | Vægt | Valgt? | Komponenter efter |
|---:|---|---:|---|---|
| 1 | a–c | 1 | ✓ | {a,c} |
| 2 | a–d | 2 | ✓ | {a,c,d} |
| 3 | b–g | 2 | ✓ | {a,c,d} {b,g} |
| 4 | c–d | 2 | ✗ kreds (c og d er begge i {a,c,d}) | |
| 5 | e–i | 2 | ✓ | {a,c,d} {b,g} {e,i} |
| 6 | b–d | 3 | ✓ | {a,b,c,d,g} {e,i} |
| 7 | h–f | 4 | ✓ | {a,b,c,d,g} {e,i} {f,h} |
| 8 | j–i | 5 | ✓ | {a,b,c,d,g} {e,i,j} {f,h} |
| 9 | a–b | 6 | ✗ kreds | |
| 10 | a–e | 8 | ✓ | {a,b,c,d,e,g,i,j} {f,h} |
| 11 | g–h | 8 | ✓ | alle 10 noder – færdig |

**MST (i Kruskal-rækkefølge):**
a–c (1), a–d (2), b–g (2), e–i (2), b–d (3), h–f (4), j–i (5), a–e (8), g–h (8)

**Samlet vægt:** 1 + 2 + 2 + 2 + 3 + 4 + 5 + 8 + 8 = **35**

### Alternative løsninger med samme vægt

- **c–d** kunne vælges i stedet for **a–d** (begge vægt 2), afhængigt af rækkefølgen blandt kanterne med vægt 2.
- **e–h** (8) kunne vælges i stedet for **g–h** (8). Begge forbinder {f,h} med resten.

Vægten er altid **35**.

### Kontrol med Prim (start i a)

Prim giver samme træ (samme vægt) i rækkefølgen:
a–c (1), a–d (2), b–d (3), b–g (2), a–e (8), e–i (2), j–i (5), g–h (8), h–f (4) → **35**.
