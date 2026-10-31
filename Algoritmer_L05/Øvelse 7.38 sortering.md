# Opgave 7.38 – sortering (lektion 5/6)

## Opgaven

> Write a program that reads N points in a plane and outputs any group of four or more colinear points (i.e., points on the same line). The obvious brute-force algorithm requires O(N⁴) time. However, there is a better algorithm that makes use of sorting and runs in O(N² log N) time.

Koden ligger i `Point.cs` og `ColinearFinder.cs`, og testene i `Program.cs`.

## Brute force – O(N⁴)

Prøv alle grupper af fire punkter og tjek, om de ligger på samme linje. Der er "N vælg 4" ≈ N⁴/24 grupper, så O(N⁴).

## Løsning med sortering – O(N² log N)

**Nøgleidé:** tre punkter p, q og r ligger på samme linje, præcis når hældningen fra p til q er den samme som hældningen fra p til r.

For hvert punkt `p`:

1. Beregn hældningen fra `p` til hvert af de andre N − 1 punkter.
2. **Sortér** punkterne efter hældning. Så ligger punkter med samme hældning ved siden af hinanden.
3. Gå igennem den sorterede liste og find løb af ens hældninger. Er et løb 3 eller længere, ligger de punkter og `p` på samme linje, altså en gruppe på mindst 4.

### Eksempel

Set fra p = (0, 0) med de andre punkter (1, 1), (2, 2), (3, 3), (5, 5), (4, 0) og (4, 2) bliver hældningerne:

| punkt | (1,1) | (2,2) | (3,3) | (5,5) | (4,0) | (4,2) |
|---|---|---|---|---|---|---|
| hældning | 1 | 1 | 1 | 1 | 0 | 1/2 |

Efter sortering: `0, 1/2, 1, 1, 1, 1`. Løbet af fire 1-taller giver gruppen (0,0), (1,1), (2,2), (3,3), (5,5).

### Hældning uden afrundingsfejl

Med `double` kan to hældninger, der burde være ens, blive en smule forskellige. Derfor gemmes retningen som et forkortet heltalspar `(dx, dy)`:

- Del dx og dy med deres største fælles divisor: (2, 4) → (1, 2).
- Gør fortegnet entydigt (dx > 0, eller dx = 0 og dy > 0): (−1, −2) → (1, 2).

Så har to punkter samme hældning, præcis når deres par er ens. Lodrette linjer (dx = 0) kræver ikke noget særtilfælde, sådan som en division med 0 ville.

Parrene sorteres efter dx og derefter dy. Rækkefølgen svarer ikke til hældningernes størrelse, men det gør ikke noget, for vi har kun brug for, at **ens** retninger ender ved siden af hinanden.

### Undgå dubletter

En linje med fx 5 punkter bliver fundet 5 gange, én gang fra hvert af sine punkter. Gruppen gemmes kun, når `p` er linjens mindste punkt (sorteret efter x og derefter y). Så udskrives hver linje én gang.

## Analyse

For hvert af de N punkter:

- beregn N − 1 retninger: O(N)
- sortér dem: O(N log N)
- find løb: O(N)

I alt **N · O(N log N) = O(N² log N)**.

Sorteringen er det, der giver forbedringen: den samler punkter med samme hældning, så vi ikke behøver at prøve alle kombinationer af dem.

## Bemærkninger

- Punkterne antages at være forskellige. To ens punkter ville give retningen (0, 0).
- Opgaven siger "reads N points". `FindGroups` tager et array, så punkterne kan komme fra en fil, konsollen eller (som i testen) være skrevet direkte i koden.
- I testen ligger (0, 10) og (4, 2) tilfældigvis også på linjen y = −2x + 10, så den gruppe har 6 punkter. Det er et godt eksempel på, at metoden finder hele linjen og ikke kun grupper på præcis 4.
