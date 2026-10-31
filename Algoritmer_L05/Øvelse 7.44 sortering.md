# Opgave 7.44 – sortering (lektion 5/6)

## Opgaven

> Suppose you have an array of N elements containing only two distinct keys, `true` and `false`. Give an O(N) algorithm to rearrange the list so that all `false` elements precede the `true` elements. You may use only constant extra space.

Koden ligger i `BooleanPartition.cs`, og testene i `Program.cs`.

## Løsning

Det er præcis partitioneringstrinnet fra quicksort, hvor "pivot" er grænsen mellem `false` og `true`.

To pegere:

- `left` starter til venstre og springer over alle `false` (de står rigtigt). Den stopper ved et `true`, som står forkert.
- `right` starter til højre og springer over alle `true`. Den stopper ved et `false`, som står forkert.
- Står `left` stadig før `right`, byttes de to elementer, og vi fortsætter.
- Mødes pegerne, er vi færdige.

```csharp
int left = 0, right = a.Length - 1;
while (left < right)
{
    while (left < right && !a[left]) left++;
    while (left < right && a[right]) right--;
    if (left < right)
    {
        a[left] = false;
        a[right] = true;
        left++;
        right--;
    }
}
```

### Eksempel

```
[T F T F F T]     left = 0 (T), right = 4 (F)  → byt
[F F T F T T]     left = 2 (T), right = 3 (F)  → byt
[F F F T T T]     pegerne mødes → færdig
```

## Analyse

- **Tid: O(N).** `left` bevæger sig kun til højre og `right` kun til venstre, og de stopper, når de mødes. Tilsammen tager de højst N skridt.
- **Plads: O(1).** Der bruges kun to heltal, `left` og `right`.

## Bemærkninger

- Når der kun er to værdier, behøver man faktisk ikke bytte. Man kan i stedet tælle antallet af `false` (k) og så skrive k gange `false` efterfulgt af `true`. Det er også O(N) og O(1) plads. Byttemetoden er dog den, der virker, når elementerne har andre data med (fx objekter med en `bool`-nøgle), fordi den flytter de rigtige elementer i stedet for at overskrive dem.
- Byttemetoden er **ikke stabil**: elementer med samme nøgle kan skifte indbyrdes rækkefølge.
