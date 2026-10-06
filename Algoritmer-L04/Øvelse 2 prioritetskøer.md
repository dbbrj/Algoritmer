# Øvelse 2 – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

- **Insert:** placér elementet på første ledige plads, og lad det *percolate up*, så længe ordenskravet er brudt.
- **deleteMax / deleteMin:** fjern roden, flyt det sidste element op, og lad det *percolate down*. I en max-heap byttes med det **største** barn, i en min-heap med det **mindste**.

## Opgaven

> A priority queue can be implemented as a simple array of integers in which the first element is left unused for addressing purposes. Can the array below represent a priority queue? Please explain your answer.
>
> `{0,17,21,23,44,32,65,38,56,46,69,33,77,67,56,39,61,60,62,50,71}`

## Løsning

**Ja, arrayet kan repræsentere en prioritetskø (en min-heap).**

- **Strukturkrav:** træet skal være komplet. Det er opfyldt, fordi arrayet er fyldt sammenhængende fra index 1.
- **Ordenskrav:** for alle `i ≥ 2` skal `a[i] ≥ a[i/2]`. Det holder overalt, fx 44 ≥ 21 (i=4), 33 ≥ 32 (i=11) og 71 ≥ 69 (i=20).

Det er ikke en max-heap.

---

## Tip

Den hurtigste måde at tjekke, om et array er en heap, er at gå fra `i = 2` til `n` og sammenligne `a[i]` med `a[i/2]`. Det kører i O(n), og man undgår fejl fra at tegne træet.
