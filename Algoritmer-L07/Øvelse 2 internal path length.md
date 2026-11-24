# Øvelse 2 – internal path length (lektion 7)

## Opgaven

> Øvelser, som kodes på den udleverede BST-kode:
>
> 2. Skriv en metode, som kan beregne internal path length af et binært træ.

Fra sliden: *"The sum of all the depths of all nodes in a tree is known as the internal path length."* (Weiss). Vi går med Weiss og tæller bladene med.

## Løsning

Koden står i `BinarySearchTree.cs` (metoden `InternalPathLength`).

Idéen er at sende dybden med ned i rekursionen. Roden har dybde 0, og hvert barn har dybden af forælderen + 1. Hver node lægger sin egen dybde til summen:

```csharp
private int InternalPathLength(BinaryNode? t, int depth)
{
    if (t == null)
        return 0;

    return depth
         + InternalPathLength(t.left, depth + 1)
         + InternalPathLength(t.right, depth + 1);
}
```

Hver node besøges én gang, så køretiden er **O(N)**.

## Test

**Slide-eksemplet** (træet A–J): 2 noder i dybde 1, 4 i dybde 2 og 3 i dybde 3.

$$IPL = 2 \cdot 1 + 4 \cdot 2 + 3 \cdot 3 = 19$$

Testen bygger et træ med samme form (indsæt 8, 4, 12, 2, 6, 10, 14, 1, 3, 5) og får **19**.

**Underviserens træ** (se øvelse 1):

| Dybde | Noder | Bidrag |
|---|---|---|
| 0 | 7 | 0 |
| 1 | 4, 28 | 2 |
| 2 | 2, 57 | 4 |
| 3 | 1, 51, 60 | 9 |
| 4 | 48, 58, 69 | 12 |
| 5 | 40 | 5 |
| 6 | 35 | 6 |
| | **I alt** | **38** |

Til sammenligning har et komplet træ med 13 noder en IPL på 28 (1·0 + 2·1 + 4·2 + 6·3). Træet er altså lidt skævt, fordi grenen 51 → 48 → 40 → 35 er lang. Det er præcis det problem, AVL-træer løser i lektion 8.
