# Øvelse 4 – perfekt binært træ med højden h (lektion 7)

## Opgaven

> Øvelser, som kodes på den udleverede BST-kode:
>
> 4. Skriv en metode, som kan skabe et perfekt binært træ med højden h (>0).

Fra sliden: i et perfekt binært træ er alle niveauer fyldt, og antallet af noder er $N = 2^{h+1} - 1$.

## Løsning

Koden står i `BinarySearchTree.cs` (metoden `CreatePerfectTree(int h)`).

Opgaven siger ikke, hvilke værdier der skal i træet. Jeg bruger nøglerne $1, 2, \dots, 2^{h+1}-1$ og vælger altid **midten** som rod. Så er træet både perfekt og et gyldigt binært søgetræ:

```csharp
private static BinaryNode? BuildPerfect(int low, int high)
{
    if (low > high)
        return null;

    int mid = (low + high) / 2;
    return new BinaryNode(mid, BuildPerfect(low, mid - 1), BuildPerfect(mid + 1, high));
}
```

Fordi antallet af nøgler er $2^{h+1}-1$ (et ulige tal), bliver de to halvdele altid lige store ($2^h - 1$ hver). Træet bliver derfor perfekt hele vejen ned.

Man kunne også bygge træet ved at kalde `Insert` i level order (fx 4, 2, 6, 1, 3, 5, 7), men så skal man først beregne den rækkefølge. Den direkte konstruktion laver hver node én gang: **O(N)**.

Metoden kaster en `ArgumentOutOfRangeException`, hvis h ≤ 0, fordi opgaven kræver h > 0.

## Eksempel, h = 2

```
          4
        /   \
       2     6
      / \   / \
     1   3 5   7
```

## Test

Testen bygger træer for h = 1–4 og tjekker højde, antal noder, blade, fulde noder, internal path length, og at in-order giver 1 … N i rækkefølge.

| h | Noder $2^{h+1}-1$ | Blade $2^h$ | Fulde noder $2^h - 1$ | IPL |
|---|---|---|---|---|
| 1 | 3 | 2 | 1 | 2 |
| 2 | 7 | 4 | 3 | 10 |
| 3 | 15 | 8 | 7 | 34 |
| 4 | 31 | 16 | 15 | 98 |

IPL for et perfekt træ er $\sum_{d=0}^{h} d \cdot 2^d = (h-1) \cdot 2^{h+1} + 2$, fordi der er $2^d$ noder i dybde $d$.
