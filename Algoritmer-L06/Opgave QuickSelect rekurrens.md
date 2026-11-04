# Opgave i rekurrens: QuickSelect (lektion 6)

## Opgaven

> I starten af kurset blev I introduceret til selektionsproblemet, dvs. opgaven med at finde det k.-største eller mindste element i en tabel af usorterede heltal.
>
> En løsning med kvadratisk tidskompleksitet er indlysende: sortér tabellen med en af de simple sorteringsalgoritmer og returner det k. element. Anvendelse af en O(N log N) sortering vil naturligvis forbedre performance.
>
> Det kan imidlertid lade sig gøre at udføre opgaven i lineær tid ved at bruge principperne i QuickSort. Nedenfor er angivet koden for QuickSelect skrevet i C#. public-metoden returnerer det k.-mindste element.
>
> ```csharp
> class Program
> {
>     public static int QuickSelect(int[] a, int position)
>     {
>         if (position < 1 || position > a.Length)
>             throw new ArgumentOutOfRangeException(nameof(position));
>         return QuickSelect(a, 0, a.Length - 1, position - 1);
>     }
>
>     private static int QuickSelect(int[] a, int left, int right, int k)
>     {
>         // Only one element left
>         if (left == right)
>             return a[left];
>
>         // Choose rightmost element as pivot
>         int pivot = a[right];
>         int i = left;
>
>         // Partition
>         for (int j = left; j < right; j++)
>         {
>             if (a[j] < pivot)
>             {
>                 int temp = a[i]; a[i] = a[j]; a[j] = temp;
>                 i++;
>             }
>         }
>
>         // Put pivot in its correct position
>         int temp2 = a[i]; a[i] = a[right]; a[right] = temp2;
>
>         // i is now the position of the pivot
>         if (k == i)
>             return a[i];
>         if (k < i)
>             return QuickSelect(a, left, i - 1, k);
>         else
>             return QuickSelect(a, i + 1, right, k);
>     }
>
>     static void Main()
>     {
>         int[] a = { 8, 12, 6, 33, 9, 4, 7, 2, 5, 17, 23, 11 };
>         int result = QuickSelect(a, 6);
>         Console.WriteLine(result); // 8
>     }
> }
> ```
>
> Som I kan se, anvender koden ikke insertionSort, når tabelstørrelsen kommer under et vist niveau.
>
> Opgaven går ud på at opstille rekurrens-ligningen for QuickSelect, løse den og dermed bevise, at tidskompleksiteten er lineær, altså O(N).
>
> Det giver naturligvis god mening at lade sig inspirere af analysen af average case QuickSort.

Koden ligger i `QuickSelect.cs` (underviserens kode plus en tæller for sammenligninger), og testene og målingerne i `Program.cs`.

## Hvad koster ét kald?

Et kald på en deltabel med N elementer gør to ting:

1. **Partitionerer.** Løkken `for (int j = left; j < right; j++)` kører N − 1 gange med én sammenligning hver gang. Det er lineært arbejde, cN.
2. **Kalder sig selv én gang.** Pivot havner på plads i. Står k på pladsen, er vi færdige. Ellers fortsætter vi **kun** i den del, hvor k ligger, enten venstre del (i elementer) eller højre del (N − i − 1 elementer).

Det er den afgørende forskel fra QuickSort:

| | QuickSort | QuickSelect |
|---|---|---|
| Rekursive kald | to, begge dele | ét, kun den del med k |
| Rekurrens | T(N) = T(i) + T(N − i − 1) + cN | T(N) = T(i) **eller** T(N − i − 1) + cN |

Den generelle rekurrensligning for QuickSelect er altså

  **T(N) = T(størrelsen af den del, vi går videre i) + cN,  T(1) = 0** (base case: `left == right`, ingen sammenligninger)

## Average case

Som i noten om QuickSort antager vi, at pivot med lige stor sandsynlighed 1/N havner på hver af pladserne 0, 1, …, N − 1. Så er delen, vi går videre i, også lige sandsynligt af størrelse 0, 1, …, N − 1, og gennemsnittet af T(delstørrelse) er

  (1/N) · Σⱼ₌₀ᴺ⁻¹ T(j)

Rekurrensligningen bliver

  **T(N) = (1/N) · Σⱼ₌₀ᴺ⁻¹ T(j) + cN**

Bemærk: hos QuickSort står der **2**/N foran summen, fordi begge dele sorteres. Her er der kun 1/N, fordi kun én del behandles. Den faktor er hele grunden til, at vi ender i O(N) i stedet for O(N log N).

### Fjern summationen (samme trick som i QuickSort-noten)

Gang med N på begge sider:

  N·T(N) = Σⱼ₌₀ᴺ⁻¹ T(j) + cN²     (1)

Erstat N med N − 1:

  (N − 1)·T(N − 1) = Σⱼ₌₀ᴺ⁻² T(j) + c(N − 1)²     (2)

Træk (2) fra (1). Summerne går ud med hinanden, bortset fra det sidste led T(N − 1):

  N·T(N) − (N − 1)·T(N − 1) = T(N − 1) + c(N² − (N − 1)²)
  N·T(N) − (N − 1)·T(N − 1) = T(N − 1) + c(2N − 1)

Flyt (N − 1)·T(N − 1) over, og smid det ubetydelige −c væk:

  N·T(N) = N·T(N − 1) + 2cN

Divider med N:

  **T(N) = T(N − 1) + 2c**

Her behøver vi ikke at dividere med N(N + 1) som i QuickSort. Der står N foran både T(N) og T(N − 1), så de to led har allerede samme form.

### Teleskopering

Erstat N med N − 1, N − 2, … indtil base case:

  T(N)   = T(N − 1) + 2c
  T(N − 1) = T(N − 2) + 2c
  T(N − 2) = T(N − 3) + 2c
  …
  T(2)   = T(1) + 2c

Læg alle venstresider sammen og alle højresider sammen. T(N − 1), T(N − 2), …, T(2) står på begge sider og går ud. Tilbage er N − 1 led af 2c:

  T(N) = T(1) + 2c(N − 1)

Da T(1) = 0:

  **T(N) = 2c(N − 1) = O(N)**

Hvert niveau koster i gennemsnit kun et konstant ekstra stykke arbejde, og der er N niveauer i ligningskæden. Det giver lineær tid.

## Et skarpere bevis: altid den største del

Antagelsen ovenfor ("delen, vi går videre i, er lige sandsynligt af enhver størrelse") er en forenkling. I virkeligheden er det mest sandsynligt, at k ligger i den **største** del, for den indeholder flest pladser. Vil man være helt sikker, kan man antage det værste: vi går altid videre i den største del, max(i, N − 1 − i).

Når i løber fra 0 til N − 1, antager max(i, N − 1 − i) hver værdi fra ⌊N/2⌋ til N − 1 højst to gange. Derfor er

  T(N) ≤ (2/N) · Σⱼ₌⌊N/2⌋ᴺ⁻¹ T(j) + cN

Denne er ikke egnet til teleskopering (summen starter ikke i 0), så vi bruger **substitutionsmetoden**: gæt T(N) ≤ aN, og vis det med induktion.

- **Basis:** T(1) = 0 ≤ a · 1.
- **Antagelse:** T(j) ≤ aj for alle j < N.
- **Skridt:**

  T(N) ≤ (2/N) · Σⱼ₌⌊N/2⌋ᴺ⁻¹ a·j + cN

  Summen af j fra ⌊N/2⌋ til N − 1 er højst 3N²/8 (for N lige er den præcis 3N²/8 − N/4, og for N ulige (3N² − 3)/8). Altså

  T(N) ≤ (2/N) · a · 3N²/8 + cN = (3/4)·aN + cN

  Det er ≤ aN, præcis når cN ≤ (1/4)·aN, altså når **a ≥ 4c**.

Vælger vi a = 4c, holder induktionen, og T(N) ≤ 4cN = **O(N)**, også i denne pessimistiske udgave.

## Best case og worst case

**Best case:** pivot rammer midten hver gang, så delen halveres.

  T(N) = T(N/2) + cN

Teleskopering giver T(N) = cN + cN/2 + cN/4 + … + T(1) ≤ 2cN, altså O(N). Bemærk at summen er en geometrisk række, ikke log N led af samme størrelse som i MergeSort, fordi der kun er ét kald pr. niveau.

**Worst case:** pivot er hver gang det største (eller mindste) element, så delen kun bliver ét element mindre.

  T(N) = T(N − 1) + c(N − 1)

Det er samme rekurrens som worst case QuickSort på slides, og teleskopering giver

  T(N) = c · Σᵢ₌₁ᴺ⁻¹ i = c · N(N − 1)/2 = **Θ(N²)**

Med underviserens kode, der vælger elementet længst til højre som pivot, sker det for eksempel på en **allerede sorteret tabel**, når vi spørger efter det mindste element. Pivot er så altid det største, og hver partitionering fjerner kun pivot selv.

## Målinger

`Program.cs` tæller sammenligningerne `a[j] < pivot`. Med c = 1 forudsiger analysen mellem 2N (den forenklede model) og 4N (den pessimistiske), og worst case præcis N(N − 1)/2:

```
--- Average case: sammenligninger / N (tilfældigt input) ---
N =     1,000:          2,749 sammenligninger, pr. element 2.75
N =    10,000:         30,746 sammenligninger, pr. element 3.07
N =   100,000:        307,427 sammenligninger, pr. element 3.07
N = 1,000,000:      2,790,320 sammenligninger, pr. element 2.79

--- Worst case: sorteret input, position 1 ---
N = 1,000:    499,500 sammenligninger (N(N-1)/2 = 499,500), pr. N^2 0.50
N = 2,000:  1,999,000 sammenligninger (N(N-1)/2 = 1,999,000), pr. N^2 0.50
N = 4,000:  7,998,000 sammenligninger (N(N-1)/2 = 7,998,000), pr. N^2 0.50
```

Antal sammenligninger pr. element ligger fast omkring 3, selvom N vokser med en faktor 1000. Det er lineær tid. Den eksakte gennemsnitlige værdi (Knuth), taget over alle k, er netop omkring 3N, altså mellem vores to grænser. I worst case er forholdet til N² konstant 0,5, præcis som rekurrensen siger.

## Bemærkninger

- **Én rekursion i stedet for to** er hele forklaringen. Hos QuickSort fører 2/N til en harmonisk række Σ 1/j og dermed log N. Hos QuickSelect fører 1/N direkte til T(N) = T(N − 1) + 2c, og der er intet log N.
- **Lineær i gennemsnit, ikke garanteret.** Worst case er stadig Θ(N²). Det kan afbødes med median-of-three eller en tilfældig pivot, og med "median of medians" (Weiss 10.2.3) kan man få O(N) selv i worst case.
- **Tabellen ændres.** QuickSelect partitionerer in-place, så tabellen er omarrangeret bagefter. Derfor laver testene en kopi før hvert kald.
- **Lille trykfejl i QuickSort-noten:** når N erstattes med N − 1, skal det sidste led være c(N − 1)², ikke c(N + 1)². Det er (N − 1)², der giver 2cN − c, når man trækker fra.

## Sammenfatning

| Tilfælde | Rekurrens | Løsning |
|---|---|---|
| Average | T(N) = (1/N) Σ T(j) + cN → T(N) = T(N − 1) + 2c | **O(N)** |
| Average, pessimistisk | T(N) ≤ (2/N) Σⱼ₌⌊N/2⌋ᴺ⁻¹ T(j) + cN | T(N) ≤ 4cN = **O(N)** |
| Best | T(N) = T(N/2) + cN | ≤ 2cN = O(N) |
| Worst | T(N) = T(N − 1) + c(N − 1) | Θ(N²) |
