# Øvelser i hashing - quadratic probing (kan løses i hånden)

Fra hashing-slidesene (slide 34):

1. En hashtabel har plads til 16 elementer. 
Indsæt fem elementer, som alle hasher til samme position i tabellen med quadratic probing.
2. En hashtabel har plads til 11 elementer. 
Indsæt seks elementer, som alle hasher til samme position i tabellen med quadratic probing.
3. Hvordan skal man håndtere sletninger, hvis der anvendes probing/open addressing?

Ved quadratic probing er probe-sekvensen:

$$h_i(x) = (h(x) + i^2) \bmod M$$

hvor $M$ er tabelstørrelsen og $i = 0, 1, 2, 3, \dots$

---

## Øvelse 1 - tabelstørrelse M = 16

Antag alle 5 elementer hasher til position 0 (positionen er ligegyldig, princippet er det samme uanset hvilken position).

| i | i² | i² mod 16 |
|---|----|-----------|
| 0 | 0  | **0** |
| 1 | 1  | **1** |
| 2 | 4  | **4** |
| 3 | 9  | **9** |
| 4 | 16 | **0** ← genbesøger! |
| 5 | 25 | **9** ← genbesøger! |
| 6 | 36 | **4** ← genbesøger! |

Fra i = 4 begynder sekvensen at gentage sig selv (0, 1, 4, 9, 0, 9, 4, 1, 0, …). 
Der findes kun **4 distinkte offsets** (0, 1, 4, 9), selvom tabellen har 16 pladser og kun er 25 % fyldt.

**Konklusion:** De første 4 elementer kan indsættes fint (på positionerne 0, 1, 4, 9). 
Det **5. element kan ikke indsættes**, quadratic probing "løber i ring" og finder aldrig en ledig plads, 
selvom der er masser af ledig plads i tabellen. Dette sker, fordi 16 ikke er et primtal.

---

## Øvelse 2 - tabelstørrelse M = 11

Samme opgave, men nu skal der indsættes 6 elementer, og tabelstørrelsen er 11 (et primtal).

| i | i² | i² mod 11 |
|---|----|-----------|
| 0 | 0  | **0** |
| 1 | 1  | **1** |
| 2 | 4  | **4** |
| 3 | 9  | **9** |
| 4 | 16 | **5** |
| 5 | 25 | **3** |

Alle 6 offsets (0, 1, 4, 9, 5, 3) er **forskellige**. Alle 6 elementer kan derfor indsættes uden problemer.

**Hvorfor virker det her, men ikke i øvelse 1?**
Der findes et teorem, som siger: hvis tabelstørrelsen $M$ er et **primtal**, 
og tabellen er mindre end halvt fyldt ($\lambda < 0{,}5$), er quadratic probing garanteret at finde en ledig plads. 
Her er 6 ud af 11 pladser lige akkurat $\lceil 11/2 \rceil = 6$ - grænsen for garantien.

---

## Øvelse 3 - sletninger ved probing/open addressing

Man kan **ikke** bare fjerne et element direkte og sætte pladsen til "tom" (empty). 
Grunden er, at senere søgninger (find/search) stopper, når de møder en tom plads under probing, 
hvis et andet element blev indsat *efter* det slettede element og "sprang over" dets plads via probing, 
vil søgningen efter det element fejle, selvom elementet stadig er i tabellen.

**Løsningen** er *lazy deletion* (også kaldt tombstones): i stedet for at markere pladsen som "tom", 
markeres den som **"slettet"** (deleted/tombstone).

- Ved **søgning**: probing fortsætter forbi en "slettet"-markeret plads, ligesom hvis den var optaget.
- Ved **indsættelse**: en "slettet"-markeret plads kan genbruges som ledig plads.
- Ulempen er, at tabellen med tiden kan blive fyldt med tombstones, hvilket øger søgetiden, 
- derfor kan det være nødvendigt med rehashing for at rydde op.