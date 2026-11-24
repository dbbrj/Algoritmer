# Øvelse 2 – egen graf og opgave 9.13 a og b (lektion 9)

## Opgaven

> **Supplerende øvelse:** Implementer din egen graf med klasserne Edge, Vertex og Graph, og lav øvelse 9.13 a og b i lærebogen.

> **9.13** A *bipartite graph*, G = (V, E), is a graph such that V can be partitioned into two subsets V₁ and V₂ and no edge has both its vertices in the same subset.
>
> a. Give a linear algorithm to determine whether a graph is bipartite.
>
> b. The *bipartite matching* problem is to find the largest subset E′ of E such that no vertex is included in more than one edge. A matching of four edges (indicated by dashed edges) is shown in Figure 9.83. There is a matching of five edges, which is maximum.
>
> Show how the bipartite matching problem can be used to solve the following problem: We have a set of instructors, a set of courses, and a list of courses that each instructor is qualified to teach. If no instructor is required to teach more than one course, and only one instructor may teach a given course, what is the maximum number of courses that can be offered?

## Grafen

| Klasse | Indhold |
|---|---|
| `Vertex` | `Name` og `Adjacent`: adjacency list med de kanter, der går ud fra knuden |
| `Edge` | `From`, `To` og `Weight` (vægt 1 i uvægtede grafer) |
| `Graph` | Knuderne i en `Dictionary<string, Vertex>`, `AddVertex`, `AddEdge`, `Print`, samt `IsBipartite` (9.13 a) og `MaximumBipartiteMatching` (9.13 b) |

Grafen bruger **adjacency list**, som er god til sparse grafer (plads O(|E| + |V|)). Den kan være orienteret eller uorienteret (`new Graph(directed: false)`). I en uorienteret graf lægges hver kant i begge knuders liste. 9.13 handler om uorienterede grafer.

## 9.13 a – er grafen bipartit?

**Idé:** Farv knuderne med to farver (0 = V₁, 1 = V₂) ved bredde-først søgning. Startknuden får farve 0. Hver nabo får den modsatte farve af knuden, man kommer fra. Møder man en nabo, der allerede har **samme** farve som knuden selv, går der en kant inden for samme delmængde, og grafen er ikke bipartit.

```
for hver knude s, der ikke er farvet:        // én gang pr. komponent
    farve[s] = 0; kø.enqueue(s)
    while kø ikke er tom:
        v = kø.dequeue()
        for hver nabo w til v:
            if w ikke er farvet:
                farve[w] = 1 - farve[v]; kø.enqueue(w)
            else if farve[w] == farve[v]:
                return false
return true
```

Den ydre løkke er nødvendig, fordi grafen kan bestå af flere komponenter. Hver skal tjekkes for sig.

**Hvorfor er det korrekt?** Inden for en komponent er farvningen tvunget, når startknuden har fået sin farve. Der er altså kun én mulig opdeling (op til at bytte V₁ og V₂). Fejler den, findes der ingen. Det svarer til den kendte sætning: *en graf er bipartit, hvis og kun hvis den ikke har nogen kreds af ulige længde.*

**Køretid:** Hver knude sættes i køen én gang, og hver kant kigges på højst to gange (én gang fra hver ende). Det giver **O(|V| + |E|)**, altså lineært, som opgaven kræver. Det er den samme analyse som for unweighted shortest path fra slides.

**Test:**

| Graf | Resultat |
|---|---|
| Kreds med 4 knuder | bipartit, V₁ = {1, 3}, V₂ = {2, 4} |
| Trekant (kreds med 3 knuder) | ikke bipartit |
| To komponenter, hvor den ene er en trekant | ikke bipartit (viser, at alle komponenter tjekkes) |
| Instruktører og kurser fra 9.13 b | bipartit |

## 9.13 b – instruktører og kurser

**Modellering som bipartit matching:**

- V₁ = instruktørerne, V₂ = kurserne.
- Der er en kant mellem instruktør *i* og kursus *c*, hvis *i* er kvalificeret til at undervise i *c*.
- Grafen er bipartit pr. konstruktion: der går aldrig en kant mellem to instruktører eller to kurser.

Et valg af hvem der underviser i hvad, svarer til en delmængde E′ af kanterne:

- "Ingen instruktør underviser i mere end ét kursus" = ingen knude i V₁ ligger i mere end én kant i E′.
- "Kun én instruktør pr. kursus" = ingen knude i V₂ ligger i mere end én kant i E′.

Det er præcis definitionen på en **matching**. Hver kant i matchingen er ét kursus, der bliver udbudt. Derfor:

> **Det maksimale antal kurser, der kan udbydes, er størrelsen af en maksimal matching i grafen.**

### Hvordan finder man den maksimale matching?

Det er ikke nok at være grådig. Tager første instruktør det første kursus, hun kan, kan det blokere for en anden, der kun kan det samme kursus. Løsningen er **augmenting paths** (forøgende stier), den samme idé som i network flow fra lektion 10:

For hver instruktør *v* prøver vi at finde et ledigt kursus. Er kurset optaget, prøver vi at flytte den instruktør, der har det, over på et andet kursus, rekursivt. Lykkes det, bliver matchingen én kant større.

Det er implementeret i `Graph.MaximumBipartiteMatching` (Kuhns algoritme). For hver af de |V₁| instruktører laves én dybde-først søgning, der højst kigger på hver kant én gang: **O(|V| · |E|)**.

### Eksempel

| Instruktør | Kan undervise i |
|---|---|
| Ole | ADS, OOP |
| Anne | ADS |
| Peter | OOP, DB |
| Mette | DB, Net |
| Lars | Net |

Ingen kan undervise i Math.

Forløbet:

1. Ole får ADS.
2. Anne kan kun ADS, som er optaget. Augmenting path: Ole flyttes til OOP, Anne får ADS.
3. Peter får DB (OOP er optaget, og Ole kan ikke flyttes igen, fordi Anne har ADS).
4. Mette får Net.
5. Lars kan kun Net. Mette kan flyttes til DB, men så skal Peter til OOP, Ole til ADS og Anne har intet alternativ. Ingen augmenting path, så Lars får intet.

**Resultat: 4 kurser** (Ole – OOP, Anne – ADS, Peter – DB, Mette – Net).

Det er også det bedste muligt: ingen er kvalificeret til Math, så højst 4 af de 5 kurser kan udbydes. Der er 5 instruktører, men én af dem må gå uden kursus. 

Hvorfor ikke bare være grådig? Se på delen med kun Ole (ADS, OOP) og Anne (ADS). Grådigt, hvor hver tager det første ledige kursus: Ole tager ADS, og Anne får ingenting, altså 1 kursus. Med augmenting path flyttes Ole til OOP, og Anne får ADS, altså 2 kurser. Det er præcis trin 2 ovenfor.

**Test:** Programmet sammenligner resultatet med en brute-force-søgning på eksemplet og på 300 tilfældige små bipartite grafer. Alle giver samme størrelse, og alle matchinger er gyldige (ingen knude bruges to gange).

### Bemærkning om 9.13 c og d

Det er ikke en del af øvelsen, men c og d bygger videre på b: Tilføj en kilde *s* med kant (kapacitet 1) til alle instruktører og en dræn *t* med kant fra alle kurser. Giv kanterne instruktør → kursus kapacitet 1. Det maksimale flow fra *s* til *t* er så størrelsen af den maksimale matching. Augmenting paths i flow-netværket er præcis det, `TryAugment` gør.
