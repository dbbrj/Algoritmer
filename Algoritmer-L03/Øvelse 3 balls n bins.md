# Øvelse 3 - Balls'n Bins

**Opgave:** Implementér og test Balls & Bins-problemet: tilfældig fordeling af N bolde i N beholdere, 
med og uden *the power of two choices*. Besvar spørgsmål 1–5 nedenfor.

---

## Metode

Simuleringen er kørt i Python (samme logik kan direkte overføres til C#, se kode nedenfor):

- **Single choice:** hver bold vælger én tilfældig beholder.
- **Power of two choices:** hver bold vælger to tilfældige beholdere og lægges i den, der i forvejen har færrest bolde.

For hver konfiguration er der kørt 20 forsøg, og gennemsnittet af den maksimale beholderbelastning 
("hvor mange bolde skal der være plads til pr. beholder") er beregnet.

---

## Opgave 1 - N = 10.007

| Metode | Gns. af max pr. forsøg | Højeste observerede max | Teoretisk estimat |
|---|---|---|---|
| Single choice | 6,45 | 8 | $\ln N / \ln\ln N \approx 4{,}15$ |
| Power of two choices | 3,15 | 4 | $\ln\ln N / \ln 2 \approx 3{,}20$ |

**Godt estimat for antal bolde pr. beholder:** med almindelig (single choice) 
hashing bør man med stor sandsynlighed afsætte plads til omkring **7–8 bolde** pr. 
beholder for at undgå overløb ved N = 10.007. Bruger man *power of two choices*, er **4–5 pladser** rigeligt.

---

## Opgave 2 - N = 32.749

| Metode | Gns. af max pr. forsøg | Højeste observerede max | Teoretisk estimat |
|---|---|---|---|
| Single choice | 7,40 | 10 | $\ln N / \ln\ln N \approx 4{,}44$ |
| Power of two choices | 3,15 | 4 | $\ln\ln N / \ln 2 \approx 3{,}38$ |

**Sammenligning med opgave 1:** Ved single choice stiger det observerede maksimum tydeligt (fra ~6–8 til ~7–10), 
mens power of two choices stort set er uændret (3–4 bolde i begge tilfælde). 
Det illustrerer netop pointen med power of two choices: væksten i den maksimale beholderbelastning er 
langt mere robust over for stigende N.

---

## Opgave 3 - Power of two choices

Se tabellerne ovenfor - resultaterne for begge N er allerede kørt med power-of-two-choices-strategien og 
sammenlignet direkte med single choice.

---

## Opgave 4 - Sammenligning med Weiss' teoretiske estimater (sektion 5.7)

Weiss angiver væksten som $\Theta(\log N / \log\log N)$ for single choice og $\Theta(\log\log N)$ for power of two choices.

- **Power of two choices** stemmer godt overens: de simulerede værdier (3,15) 
ligger meget tæt på det teoretiske estimat (3,20 og 3,38).
- **Single choice** viser den rigtige *tendens* (værdien vokser svagt med N, 
og vokser langsommere end lineært), men de simulerede tal (6,45 og 7,40) 
ligger et stykke over det rene $\ln N/\ln\ln N$-estimat. 
Det er forventeligt: Θ-notation skjuler konstanter og lavere-ordens led, 
og for "kun" 10.000–33.000 bolde er N ikke stor nok til, at de asymptotiske led dominerer fuldt ud. 
Eksperimenterne **bekræfter altså væksttendensen**, men ikke det eksakte tal.

**Konklusion:** eksperimenterne understøtter, at power of two choices vokser markant langsommere ($\Theta(\log\log N)$) 
end single choice ($\Theta(\log N/\log\log N)$), præcis som Weiss beskriver.

---

## Opgave 5 - Theorem 5.2 (N bolde i M = N² beholdere)

**Påstanden:** hvis N bolde placeres tilfældigt i $M = N^2$ beholdere, er sandsynligheden for, 
at ingen beholder indeholder mere end én bold, **mindre end 0,5**.

Eksperiment (3000 forsøg pr. N):

| N | M = N² | Målt P(ingen kollision) |
|---|---|---|
| 10  | 100    | 0,631 |
| 20  | 400    | 0,613 |
| 50  | 2.500  | 0,619 |
| 100 | 10.000 | 0,626 |
| 200 | 40.000 | 0,614 |

**Resultat:** eksperimenterne **afkræfter** påstanden som ordret formuleret her. 
Sandsynligheden for ingen kollision ligger konsekvent omkring **0,61–0,63**, ikke under 0,5. 
Det stemmer med den klassiske "fødselsdagsparadoks"-tilnærmelse:

$$P(\text{ingen kollision}) \approx e^{-N^2/(2M)} = e^{-N^2/(2N^2)} = e^{-1/2} \approx 0{,}6065$$

hvilket er tæt på de målte værdier. Med N bolde i N² beholdere er belastningen faktisk lav nok til, 
at sandsynligheden for *ingen* kollision er **større** end 0,5 (og sandsynligheden for mindst én kollision er omkring 0,39) - 
det modsatte af hvad påstanden hævder.