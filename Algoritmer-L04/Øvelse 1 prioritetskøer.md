# Øvelse 1 – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

- **Insert:** placér elementet på første ledige plads, og lad det *percolate up*, så længe ordenskravet er brudt.
- **deleteMax / deleteMin:** fjern roden, flyt det sidste element op, og lad det *percolate down*. I en max-heap byttes med det **største** barn, i en min-heap med det **mindste**.

## Opgaven

> H₁ and H₂ are max heaps. A max heap is a priority queue where the biggest key is in the root, and the definition of the heap order is that any node must have a key that is bigger than all its descendants'.
>
> Draw H₁ after a *deleteMax* operation.
>
> Draw H₂ after an *insert* operation with key 35.

```
H₁:       42                H₂:       42
        /    \                      /    \
      41      23                  33      37
     /  \    /  \                /  \    /  \
    3    9  17   6              3   18  17   4
```

## Løsning

### H₁ efter deleteMax

Start: `[42, 41, 23, 3, 9, 17, 6]`

1. 42 fjernes, og det sidste element (6) flyttes til roden.
2. Børnene er 41 og 23. Det største er 41 > 6, så de byttes.
3. Børnene er 3 og 9. Det største er 9 > 6, så de byttes.
4. 6 er nu et blad, og vi stopper.

```
        41
      /    \
     9      23
    / \    /
   3   6  17
```

Array: `[41, 9, 23, 3, 6, 17]`

### H₂ efter insert(35)

Start: `[42, 33, 37, 3, 18, 17, 4]`

1. 35 placeres på index 8 (barn af 3).
2. 35 > 3, så de byttes.
3. 35 > 33, så de byttes.
4. 35 < 42, og vi stopper.

```
          42
        /    \
      35      37
     /  \    /  \
    33  18  17   4
   /
  3
```

Array: `[42, 35, 37, 33, 18, 17, 4, 3]`