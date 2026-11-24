# Øvelse 1 – opgave i aktivitetsplanlægning, kritisk vej (lektion 9 og 10)

Øvelsen står på slides i både lektion 9 og lektion 10 ("Opgave i aktivitetsplanlægning – Kritisk vej, på itslearning").

## Opgaven

> **Algoritmer og Datastrukturer – opgave i grafer/aktivitetsplanlægning**
>
> I denne opgave skal der arbejdes med filer, klasser, objekter og vektorer/ArrayLists.
>
> Lav en tekstfil ved navn data.txt med følgende indhold:
>
> ```
> 1;A;3
> 1;B;6
> 1;C;4
> 2;D;5
> 3;E;4
> 3;F;1
> 4;G;2
> 4;H;7
> 4;I;1
> 5;J;4
> ```
>
> Den type af filer kaldes kommaseparerede filer og bruges ofte til logning af måledata og andre strukturerede data.
>
> Her er der tale om data til en aktivitetsplan for et udviklingsprojekt med tre informationer pr aktivitet.
>
> Den første information kalder vi event, og den angiver rækkefølgen af aktiviteterne; den anden kalder vi task, og den angiver navnet på aktiviteten; den sidste hedder duration og indeholder aktivitetens varighed i dage.
>
> Opgaven går ud på følgende:
>
> - Lav en klasse ved navn Aktivitet, som indeholder attributterne event (int), task (string) og duration (int) med de sædvanlige metoder (constructors, get-metoder).
> - Indlæs aktiviteterne fra filen og isoler attributterne.
> - Hver aktivitet transformeres til et Aktivitet-objekt, og objekterne placeres i en vector/ArrayList ved navn tabel.
> - Herefter udskrives antallet af aktiviteter (10) og den gennemsnitlige varighed som decimaltal (3,7 dage).
> - Udførelsen af de enkelte aktiviteter er afhængig af attributten event. Fx kan task D (event 2) ikke udføres, før A, B og C (event 1) alle er udført, og task J (event 5) kan ikke udføres før G, H og I (event 4) er udført. Aktiviteter med samme event-nummer kan altså udføres parallelt/samtidigt.
> - Der er således en række aktiviteter, der kan kaldes den kritiske vej, hvorom det gælder, at ingen af de indgående aktiviteter kan blive forsinkede, uden at det forsinker hele projektet. Fx kan task A godt blive forsinket, fordi task D alligevel ikke kan starte, før task B er færdig. Men en forsinkelse af B, forsinker hele projektet.
> - Du skal udskrive varigheden af den kritiske vej, dvs. den tid det minimum tager at udføre projektet (26 dage) samt rækkefølgen af tasks (B, D, E, H, J). Opgaven skal løses ved at gennemløbe vector/ArrayList tabel, og ikke ved at læse inputfilen igen.
> - Precondition: du kan antage, at events er i stigende orden startende med 1 og uden 'huller'. Tasks, derimod, kommer ikke altid nødvendigvis i stigende alfabetisk orden som i dette tilfælde.
> - Endelig skal du lave en helt ny tekstfil (eller flere) med andre data, som du kan teste dit program yderligere med.
>
> I forhold til hvordan programmeringsopgaver løses i industrien, er det af afgørende betydning, at du følger ovenstående specifikation til punkt og prikke. Fx ikke noget med at bruge andre navne på attributterne end de angivne eller at bruge et array i stedet for en vector/ArrayList.
>
> *Ole Dolriis, november 2024*

## Løsning

Filer: `Aktivitet.cs`, `Aktivitetsplan.cs`, `Program.cs` og testdata `data.txt`–`data4.txt`.

### Specifikationen, punkt for punkt

| Krav | Sådan er det løst |
|---|---|
| Klassen `Aktivitet` med `event` (int), `task` (string), `duration` (int) | `Aktivitet.cs`. `event` er et reserveret ord i C#, så det skrives `@event`. Navnet er stadig `event`. |
| Constructors og get-metoder | `Aktivitet(int @event, string task, int duration)`, `GetEvent()`, `GetTask()`, `GetDuration()` |
| Indlæs filen og isolér attributterne | Konstruktøren i `Aktivitetsplan` læser hver linje og splitter på `;` |
| Objekterne i en vector/ArrayList ved navn `tabel` | `List<Aktivitet> tabel`. `List<T>` er C#'s svar på Javas `ArrayList` (den ikke-generiske `System.Collections.ArrayList` findes, men er ikke typesikker). |
| Udskriv antal (10) og gennemsnit som decimaltal (3,7 dage) | `AntalAktiviteter()` og `GennemsnitligVarighed()`. Udskrives med dansk komma via `CultureInfo("da-DK")`. |
| Kritisk vej (26 dage, B, D, E, H, J) ved at gennemløbe `tabel` | `KritiskVej(out List<string> tasks)` |
| Nye testfiler | `data2.txt`, `data3.txt`, `data4.txt` |

### Idéen bag den kritiske vej

Alle tasks i event *k* skal være færdige, før event *k+1* kan starte. Tasks i samme event kører parallelt, så et event varer lige så længe som sin **længste** task. Den længste task i hvert event er altså den, der ikke kan forsinkes, og den kritiske vej er den længste task fra hvert event:

| Event | Tasks | Længste | Varighed |
|---|---|---|---|
| 1 | A 3, **B 6**, C 4 | B | 6 |
| 2 | **D 5** | D | 5 |
| 3 | **E 4**, F 1 | E | 4 |
| 4 | G 2, **H 7**, I 1 | H | 7 |
| 5 | **J 4** | J | 4 |
| | | **B, D, E, H, J** | **26 dage** |

Som graf er hvert event en knude, og hver task er en kant fra event *k* til *k+1* med varigheden som vægt. Det er en DAG, og den kritiske vej er den **længste** vej fra start til slut. Fordi alle kanter går fra *k* til *k+1*, er den længste vej bare summen af den største kant mellem hvert par af nabo-events.

Algoritmen gennemløber `tabel` én gang. Den holder styr på det aktuelle event og den største varighed i det. Når event-nummeret skifter, lægges det forrige events maksimum til totalen. Køretid **O(N)**.

Gennemsnittet er $(3+6+4+5+4+1+2+7+1+4)/10 = 37/10 = 3{,}7$ dage.

### Uafgjort

Opgaven siger ikke, hvad der skal ske, hvis to tasks i samme event har samme maksimale varighed. Så er de begge kritiske, fordi en forsinkelse af den ene vil forsinke hele projektet. Programmet skriver dem derfor sammen med `/`, fx `A/K`.

## Testfiler

| Fil | Hvad testes | Antal | Gennemsnit | Kritisk vej |
|---|---|---|---|---|
| `data.txt` | Opgavens data | 10 | 3,7 | 26 dage: B, D, E, H, J |
| `data2.txt` | Tasks ikke i alfabetisk orden og uafgjort i event 2 | 6 | 4,5 | 14 dage: M, A/K, C |
| `data3.txt` | Kun én aktivitet | 1 | 7,0 | 7 dage: X |
| `data4.txt` | Ren kæde (ét task pr. event), så alt er kritisk | 4 | 2,5 | 10 dage: Design, Kode, Test, Release |

Kør med en anden fil: `dotnet run -- minfil.txt`

## Output

```
--- data.txt ---
Antal aktiviteter: 10
Gennemsnitlig varighed: 3,7 dage
Kritisk vej: 26 dage
Rækkefølge: B, D, E, H, J
```
