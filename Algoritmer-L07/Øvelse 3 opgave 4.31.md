# Øvelse 3 – opgave 4.31 (lektion 7)

## Opgaven

> **4.31** Write efficient methods that take only a reference to the root of a binary tree, *T*, and compute
>
> a. The number of nodes in *T*.
> b. The number of leaves in *T*.
> c. The number of full nodes in *T*.
>
> What is the running time of your routines?

Et **blad** er en node uden børn. En **fuld node** er en node med præcis to børn.

## Løsning

Koden står i `BinarySearchTree.cs` (metoderne `CountNodes`, `CountLeaves` og `CountFullNodes`). De er `static` og tager kun en `BinaryNode` (roden) som parameter, som opgaven kræver. Derfor virker de på ethvert binært træ og ikke kun på søgetræet.

Alle tre følger samme rekursive mønster: tomt træ giver 0, ellers tæller noden selv (hvis den opfylder betingelsen) plus resultatet fra venstre og højre subtræ.

```csharp
// a. Antal noder
if (t == null) return 0;
return 1 + CountNodes(t.left) + CountNodes(t.right);

// b. Antal blade
if (t == null) return 0;
if (t.left == null && t.right == null) return 1;
return CountLeaves(t.left) + CountLeaves(t.right);

// c. Antal fulde noder
if (t == null) return 0;
int self = (t.left != null && t.right != null) ? 1 : 0;
return self + CountFullNodes(t.left) + CountFullNodes(t.right);
```

## Køretid

Hver metode besøger hver node præcis én gang og laver et konstant stykke arbejde pr. node. Køretiden er derfor **O(N)** for alle tre. Det er også det bedste, man kan gøre, fordi man er nødt til at kigge på hver node for at tælle den.

Som rekurrens: $T(N) = T(i) + T(N - i - 1) + c$, som løser til $T(N) = O(N)$ uanset hvordan træet er delt op.

## Test (underviserens træ fra øvelse 1)

| | Resultat | Noder |
|---|---|---|
| a. Noder | **13** | |
| b. Blade | **4** | 1, 35, 58, 69 |
| c. Fulde noder | **3** | 7, 57, 60 |

**Kontrol:** I ethvert ikke-tomt binært træ gælder *antal blade = antal fulde noder + 1* (Weiss, opgave 4.4). Her er 4 = 3 + 1 ✓
