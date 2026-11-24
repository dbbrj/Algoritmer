# Opgave 4 (15 %) – Sortering i lineær tid

**Opgave:** Opgaven går ud på at skrive en metode, der kan sortere et array, som altid består af 100 positive heltal, hvorom det gælder, at alle tal i tabellen er større end eller lig med en (1) og mindre end eller lig med 200.

Metoden har ingen returværdi, men skal blot udskrive tallene fra tabellen i stigende rækkefølge, og det er ikke tilladt at benytte sorteringsmetoder fra standardbibliotekerne.

For at få fuldt points skal din metode have lineær - O(N) - tidskompleksitet.

Metoden kunne have følgende signatur:

```java
void minSortering(int[] arr);
```

---

## Løsning: Counting sort (tællesortering)

Se `TælleSortering.cs`.

```csharp
public void MinSortering(int[] arr)
{
    int[] antal = new int[201]; // indeks 0 bruges ikke

    foreach (int tal in arr)
        antal[tal]++;

    for (int værdi = 1; værdi <= 200; værdi++)
    {
        for (int i = 0; i < antal[værdi]; i++)
            Console.Write(værdi + " ");
    }
    Console.WriteLine();
}
```

### Idé

Fordi alle tal ligger i det kendte, lille interval 1–200, behøver vi ikke sammenligne tallene med hinanden. I stedet **tæller** vi, hvor mange gange hver værdi forekommer:

1. **Tælling:** Gå én gang gennem `arr` og læg 1 til `antal[tal]`. Fx betyder `antal[17] = 3`, at 17 forekommer tre gange.
2. **Udskrivning:** Gå tællerne igennem fra 1 til 200 og udskriv hver værdi det antal gange, den er talt. Det giver automatisk stigende rækkefølge, og dubletter kommer med.

### Tidskompleksitet

Lad N være antal tal (her 100) og K antallet af mulige værdier (her 200).

- Tælling: **N** skridt.
- Udskrivning: den ydre løkke kører **K** gange, og den indre løkke udskriver i alt **N** tal → K + N.

I alt **O(N + K)**. K = 200 er en konstant, der ikke vokser med N, så tidskompleksiteten er **O(N)**.

Det er ikke i strid med, at sammenligningsbaseret sortering kræver Ω(N log N): counting sort sammenligner aldrig to elementer, men udnytter, at værdierne er små heltal.

### Testresultater

Se `TestOpgave4` i `Program.cs`: 100 tilfældige tal i 1–200 (med dubletter) sorteres og sammenlignes med en kontrolsortering. Derudover udskrives et mindre eksempel.
