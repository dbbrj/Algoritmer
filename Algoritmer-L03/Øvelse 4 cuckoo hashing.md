# Øvelse 4 - Cuckoo hashing (supplement)

**Opgave:** Skriv din egen implementering af en Cuckoo hashtabel. Implementeringen skal kunne indsætte og hente elementer, 
og helst også kunne håndtere den situation, at loadfaktoren overskrider 0,5.

C#-koden ligger i to filer i projektet:
- **`CuckooHashTable.cs`** - selve klassen med logikken
- **`Program.cs`** - `Main`, som opretter en `BallsAndBins`, en `CuckooHashTable` og kører demoen

---

## Princippet i Cuckoo hashing

- Der bruges **to tabeller** og **to hashfunktioner**, $h_1$ og $h_2$.
- Et element kan altid kun ligge ét af to steder: `table1[h1(key)]` eller `table2[h2(key)]`.
- Ved indsættelse: hvis pladsen i tabel 1 er optaget, **smides den nuværende beboer ud** ("cuckoo") og forsøges i stedet 
placeret i tabel 2 på sin $h_2$-position. 
Er den plads også optaget, smides *den* beboer ud og skal placeres i tabel 1 igen - og så videre.
- Denne "smid ud"-kæde kan i princippet blive uendelig (en **cyklus**).
Derfor har implementeringen et loft (`maxKicks`) - nås loftet, **forstørres tabellen** (rehash), 
og alle elementer indsættes igen i den større tabel.
- Fordelen ved cuckoo hashing er, at et opslag (`Contains`) altid er **O(1) i værste tilfælde**, 
der er kun to steder at kigge.

I implementeringen bruges:

```csharp
private int Hash1(int key) => ((key % capacity) + capacity) % capacity;
private int Hash2(int key) => (((key * 31 + 7) % capacity) + capacity) % capacity;
```

Loadfaktoren håndteres på to måder:
1. **Proaktivt:** inden en indsættelse tjekkes det, om loadfaktoren (elementer / (2 × kapacitet)) 
1. allerede vil overstige 0,5 - i så fald forstørres tabellen først.
2. **Reaktivt:** hvis "smid ud"-kæden rammer `maxKicks` uden at finde en ledig plads (tegn på en cyklus), 
1. forstørres tabellen, og elementet forsøges indsat igen.

---

## Demonstration

Nøglerne `4, 15, 26, 37, 48, 8, 19, 30, 5, 16, 27, 9` indsættes i en tabel, der starter med kapacitet 11 pr. tabel 
(22 pladser i alt):

| Indsat | Resultat | Loadfaktor efter | Kapacitet efter |
|---|---|---|---|
| 4  | OK | 0,05 | 11 |
| 15 | OK | 0,09 | 11 |
| 26 | OK | 0,07 | **22** ← tabellen blev forstørret |
| 37 | OK | 0,09 | 22 |
| 48 | OK | 0,06 | **44** ← forstørret igen |
| 8  | OK | 0,07 | 44 |
| 19 | OK | 0,08 | 44 |
| 30 | OK | 0,09 | 44 |
| 5  | OK | 0,10 | 44 |
| 16 | OK | 0,11 | 44 |
| 27 | OK | 0,12 | 44 |
| 9  | OK | 0,14 | 44 |

**Bemærk:** tabellen bliver forstørret to gange, selvom loadfaktoren er lav (7–9 %).
det er ikke fordi tabellen er ved at være fuld, men fordi "smid ud"-kæden løber ind i en **cyklus** 
med den lille kapacitet på 11. 
Det er netop cuckoo hashings kendte svaghed: med for få pladser kan selv få elementer skabe cykler, 
som kun kan løses ved at forstørre tabellen. Det illustrerer godt, hvorfor kravet om at 
"kunne håndtere at loadfaktoren overskrider 0,5" i praksis betyder, at man skal kunne resize dynamisk.

**Slutresultat (kapacitet 44 pr. tabel):**

Tabel 1 (uddrag, øvrige pladser tomme):
```
[4]=4  [5]=5  [8]=8  [9]=9  [15]=15  [16]=16  [19]=19
[26]=26  [27]=27  [30]=30  [37]=37
```

Tabel 2 (kun én plads i brug):
```
[43]=48
```

**Opslag:**
- `Contains(26)` → **true** (fundet i tabel 1, position 26)
- `Contains(99)` → **false** (findes ikke i nogen af tabellerne)

---

## Konklusion

Implementeringen demonstrerer de centrale egenskaber ved cuckoo hashing:
- Indsættelse kan udløse en kæde af "udsmidninger" mellem de to tabeller.
- Cykler i denne kæde håndteres ved at forstørre tabellen og geninds sætte alle elementer.
- Opslag er altid hurtigt, fordi et element kun kan ligge ét af to bestemte steder.
