# Øvelse 1 – traversering af binære træer (lektion 7)

## Opgaven

> Øvelser, som kodes på den udleverede BST-kode:
>
> 1. Skriv metoder til pre-order, in-order, post-order og level order traversering af binære (søge) træer.

## Løsning

Koden står i `BinarySearchTree.cs` (metoderne `PreOrder`, `InOrder`, `PostOrder` og `LevelOrder`). Hver metode returnerer en `List<int>` med elementerne i den rækkefølge, de besøges.

| Traversering | Rækkefølge | Implementering |
|---|---|---|
| Pre-order | node, venstre, højre | rekursiv |
| In-order | venstre, node, højre | rekursiv |
| Post-order | venstre, højre, node | rekursiv |
| Level order | niveau for niveau, venstre mod højre | iterativ med en **kø** (bredde-først) |

De tre første adskiller sig kun ved, *hvornår* noden selv lægges i listen i forhold til de to rekursive kald. Level order kan ikke laves med simpel rekursion, fordi man skal blive på samme niveau. Derfor lægges børnene i en kø: de noder, der kom først ind, besøges først.

Alle fire besøger hver node præcis én gang, så køretiden er **O(N)**.

## Testtræet

Underviserens eksempel fra den udleverede `Main`: indsæt 7, 4, 28, 3, 55, 2, 51, 60, 1, 48, 58, 69, 40, 57, 35, og slet derefter 55 og 3.

- `Remove(55)`: 55 har to børn, så den erstattes af det mindste element i højre subtræ (57), og 57 slettes nedenunder.
- `Remove(3)`: 3 har ét barn (2), som rykker op.

```
            7
          /   \
         4     28
        /        \
       2          57
      /          /  \
     1         51    60
              /     /  \
            48     58   69
           /
         40
        /
      35
```

## Resultat

| Traversering | Resultat |
|---|---|
| Pre-order | 7, 4, 2, 1, 28, 57, 51, 48, 40, 35, 60, 58, 69 |
| In-order | 1, 2, 4, 7, 28, 35, 40, 48, 51, 57, 58, 60, 69 |
| Post-order | 1, 2, 4, 35, 40, 48, 51, 58, 69, 60, 57, 28, 7 |
| Level order | 7, 4, 28, 2, 57, 1, 51, 60, 48, 58, 69, 40, 35 |

In-order giver elementerne i sorteret rækkefølge. Det gælder altid for et binært søgetræ, og det er derfor, den udleverede `PrintTree` bruger in-order.
