# Øvelse 5 – prioritetskøer (lektion 4)

Heaps er skrevet som arrays med index 0 ubrugt. Index *i* har børn på `2i` og `2i+1` og forælder på `i/2`.

- **Insert:** placér elementet på første ledige plads, og lad det *percolate up*, så længe ordenskravet er brudt.
- **deleteMax / deleteMin:** fjern roden, flyt det sidste element op, og lad det *percolate down*. I en max-heap byttes med det **største** barn, i en min-heap med det **mindste**.

## Opgaven

> Consider the following array:
>
> `{0,4,17,12,20,25,15,38,22,30,24,45,67,18,40,42,36,56}`
>
> Can this array represent a *heap* (a priority queue)? Explain your answer.

## Løsning

**Nej, arrayet kan ikke repræsentere en heap.**

Strukturen er i orden, fordi arrayet er sammenhængende. Ordenskravet er dog brudt:

- Index 10 indeholder **24**, og dens forælder (index 5) indeholder **25**.
- I en min-heap må et barn ikke være mindre end sin forælder, og 24 < 25.

Alle andre forælder-barn-par overholder ordenskravet, men ét brud er nok. Det er heller ikke en max-heap, da roden (4) er den mindste værdi.

---

## Tip

Den hurtigste måde at tjekke, om et array er en heap, er at gå fra `i = 2` til `n` og sammenligne `a[i]` med `a[i/2]`. Det kører i O(n), og man undgår fejl fra at tegne træet.
