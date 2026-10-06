# Øvelse 4 – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

- **Insert:** placér elementet på første ledige plads, og lad det *percolate up*, så længe ordenskravet er brudt.
- **deleteMax / deleteMin:** fjern roden, flyt det sidste element op, og lad det *percolate down*. I en max-heap byttes med det **største** barn, i en min-heap med det **mindste**.

## Opgaven

> H₁ and H₂ are max heaps. A max heap is a priority queue where the biggest key is in the root, and the definition of the heap order is that any node must have a key that is bigger than all its descendants'.
>
> Draw H₁ after an *insert* operation with key 12.
>
> Draw H₂ after a *deleteMax* operation.

```
H₁:       32                H₂:       29
        /    \                      /    \
      10      8                   10      8
     /  \    / \                 /  \    / \
    1    9  7   6               1    9  3   6
```

## Løsning

### H₁ efter insert(12)

Start: `[32, 10, 8, 1, 9, 7, 6]`

1. 12 placeres på index 8 (barn af 1).
2. 12 > 1, så de byttes.
3. 12 > 10, så de byttes.
4. 12 < 32, og vi stopper.

```
          32
        /    \
      12      8
     /  \    / \
    10   9  7   6
   /
  1
```

Array: `[32, 12, 8, 10, 9, 7, 6, 1]`

### H₂ efter deleteMax

Start: `[29, 10, 8, 1, 9, 3, 6]`

1. 29 fjernes, og det sidste element (6) skal ned fra roden.
2. Børnene er 10 og 8. 10 > 6, så 10 rykker op.
3. Børnene er 1 og 9. 9 > 6, så 9 rykker op.
4. Der er ingen børn, så 6 placeres på index 5.

```
       10
      /  \
     9    8
    / \  /
   1  6 3
```

Array: `[10, 9, 8, 1, 6, 3]`
