# Opgave 7.43* – sortering (lektion 5/6)

## Opgaven

> Suppose arrays A and B are both sorted and both contain N elements. Give an O(log N) algorithm to find the median of A ∪ B.

Koden ligger i `MedianFinder.cs`, og testene i `Program.cs`.

## Hvad er medianen her?

A ∪ B har 2N elementer, altså et lige antal. Medianen er derfor gennemsnittet af det **N'te** og det **(N+1)'te** mindste element. (Nogle bøger bruger bare det N'te, også kaldet den nedre median. Det får man også ud af løsningen.)

Den simple løsning er at flette A og B (som i mergesort) og tage de to midterste elementer. Det er O(N). Opgaven vil have O(log N), og så kan vi ikke engang kigge på alle elementerne. Det kalder på binær søgning.

## Løsning – binær søgning på en deling

Tænk på den sorterede A ∪ B som delt i to halvdele med N elementer hver:

```
venstre halvdel (de N mindste)  |  højre halvdel (de N største)
```

Venstre halvdel består af de første `i` elementer fra A og de første `j = N − i` fra B:

```
A:  a[0] ... a[i-1]   |   a[i] ... a[N-1]
B:  b[0] ... b[j-1]   |   b[j] ... b[N-1]
```

Inden for hvert array er venstre side allerede ≤ højre side, fordi arrays er sorterede. Delingen er derfor korrekt, hvis også **krydsene** holder:

```
a[i-1] <= b[j]    og    b[j-1] <= a[i]
```

Så kan vi binærsøge efter `i` i intervallet [0, N]:

- `a[i-1] > b[j]`: vi har taget et for stort element fra A. **Tag færre fra A** (søg til venstre).
- `b[j-1] > a[i]`: vi har taget for få fra A. **Tag flere fra A** (søg til højre).
- Ellers er delingen korrekt:
  - N'te mindste = `max(a[i-1], b[j-1])` (det største i venstre halvdel)
  - (N+1)'te mindste = `min(a[i], b[j])` (det mindste i højre halvdel)
  - medianen er gennemsnittet af de to.

Tager vi 0 eller N elementer fra et array, findes `a[i-1]` eller `a[i]` ikke. Så bruges −∞ eller +∞ (`int.MinValue` og `int.MaxValue`), og sammenligningen bliver automatisk sand.

### Eksempel

A = [1, 3, 5, 7], B = [2, 4, 6, 8], N = 4.

| low–high | i | j | a[i-1] / a[i] | b[j-1] / b[j] | handling |
|---|---|---|---|---|---|
| 0–4 | 2 | 2 | 3 / 5 | 4 / 6 | 3 ≤ 6 og 4 ≤ 5 → korrekt |

Venstre halvdel er {1, 3, 2, 4}, og højre er {5, 7, 6, 8}. N'te mindste = max(3, 4) = 4, (N+1)'te = min(5, 6) = 5. **Medianen er 4,5.**

Et eksempel, der kræver flere trin: A = [5, 6, 7, 8], B = [1, 2, 3, 4].

| low–high | i | j | a[i-1] / a[i] | b[j-1] / b[j] | handling |
|---|---|---|---|---|---|
| 0–4 | 2 | 2 | 6 / 7 | 2 / 3 | 6 > 3 → færre fra A |
| 0–1 | 0 | 4 | −∞ / 5 | 4 / +∞ | korrekt |

Medianen er (max(−∞, 4) + min(5, +∞)) / 2 = (4 + 5) / 2 = **4,5**.

## Analyse

Søgeintervallet for `i` starter med N + 1 muligheder og halveres i hvert trin, og hvert trin er O(1) (fire opslag og to sammenligninger). Derfor er tiden **O(log N)**, og den ekstra plads er O(1).

## Bemærkninger

- Løsningen virker også med dubletter, også på tværs af A og B.
- Summen beregnes som `long` før divisionen, så to store `int`'er ikke giver overflow.
- Samme teknik virker, hvis A og B har forskellig længde, men så skal man binærsøge i det korteste array.
