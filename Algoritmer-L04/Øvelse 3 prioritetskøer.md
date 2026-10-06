# Øvelse 3 – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

- **Insert:** placér elementet på første ledige plads, og lad det *percolate up*, så længe ordenskravet er brudt.
- **deleteMax / deleteMin:** fjern roden, flyt det sidste element op, og lad det *percolate down*. I en max-heap byttes med det **største** barn, i en min-heap med det **mindste**.

## Opgaven

> The figure below represents a priority queue implemented in a simple array.
>
> Show the contents of the priority queue (draw it or show the array) after the following three operations have been performed: first you must add an element with the value of 7 (`insert(7)`), then an element with the value of 15 is added (`insert(15)`), and finally the smallest element is removed (`deleteMin()`).

```
              5
          /       \
         9         11
       /   \      /  \
     14     18   19   21
    /  \   /
   33  17 27
```

| Indeks | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| Værdi | 0 | 5 | 9 | 11 | 14 | 18 | 19 | 21 | 33 | 17 | 27 | |

## Løsning

Start: `[5, 9, 11, 14, 18, 19, 21, 33, 17, 27]`

### insert(7)

7 placeres på index 11 (forælder 18). Det byttes med 18 og derefter med 9, og det stopper ved 5.

`[5, 7, 11, 14, 9, 19, 21, 33, 17, 27, 18]`

### insert(15)

15 placeres på index 12 (forælder 19). Det byttes med 19 og stopper ved 11.

`[5, 7, 11, 14, 9, 15, 21, 33, 17, 27, 18, 19]`

### deleteMin()

5 fjernes, og det sidste element (19) skal ind i hullet i roden. Hullet bevæger sig ned mod det mindste barn:

1. Børnene er 7 og 11. 7 < 19, så 7 rykker op.
2. Børnene er 14 og 9. 9 < 19, så 9 rykker op.
3. Børnene er 27 og 18. 18 < 19, så 18 rykker op.
4. Der er ingen børn, så 19 placeres på index 11.

```
            7
         /     \
        9       11
      /   \    /  \
    14    18  15   21
   / \   / \
  33 17 27 19
```

Array: `[7, 9, 11, 14, 18, 15, 21, 33, 17, 27, 19]`
