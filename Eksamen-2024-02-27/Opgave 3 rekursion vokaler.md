# Opgave 3 (18 %) – Rekursion: antal vokaler

**Opgave:** Skriv en rekursiv metode/algoritme med følgende signatur:

```java
int antalVokaler(String str, int l)
```

Parameteren str indeholder små bogstaver i det engelske alfabet:

{a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z}.

Og metoden skal returnere antallet af vokaler i strengen str, og vi antager, at vokalerne er:

{a,e,i,o,u,y}

Kaldt med strengen "stationsbygninger" returneres 6, eftersom vokalerne i strengen er "a,i,o,y,i,e".

Den anden parameter i metoden angiver længden af strengen minus 1, idet det antages, at strengen opfattes som et array af karakterer (chars).

Det kan anbefales at skrive en hjælpemetode med returværdi true ellers false, som kan afgøre om den enlige parameter af datatypen char, er en vokal eller en konsonant.

---

## Løsning

Se `VokalTæller.cs`. Kaldes med `AntalVokaler(str, str.Length - 1)`.

```csharp
public int AntalVokaler(string str, int l)
{
    if (l < 0)
        return 0;
    int her = ErVokal(str[l]) ? 1 : 0;
    return her + AntalVokaler(str, l - 1);
}

public bool ErVokal(char c)
{
    switch (c)
    {
        case 'a': case 'e': case 'i':
        case 'o': case 'u': case 'y':
            return true;
        default:
            return false;
    }
}
```

- **Basistilfælde:** `l < 0` – der er ingen tegn tilbage, så der er 0 vokaler. (Det dækker også den tomme streng, hvor `l = −1` fra start.)
- **Rekursivt tilfælde:** Se på tegnet på plads `l` (1, hvis det er en vokal, ellers 0) og læg antallet af vokaler i resten `str[0..l−1]` til. Hvert kald gør `l` én mindre, så basistilfældet nås altid.
- **Hjælpemetoden** `ErVokal` returnerer true for a, e, i, o, u, y og false for alle andre bogstaver (konsonanter).
- **Tidskompleksitet:** N + 1 kald med konstant arbejde → **O(N)**.

### Gennemløb for "stationsbygninger" (l = 16)

Rekursionen går bagfra: r, e, g, n, i, n, g, y, b, s, n, o, i, t, a, t, s. Vokalerne e, i, y, o, i, a giver tilsammen **6**.

### Testresultater

Se `TestOpgave3` i `Program.cs`:

| str | Forventet | Resultat |
|---|---:|---|
| stationsbygninger | 6 | OK |
| (tom streng) | 0 | OK |
| bcd | 0 | OK |
| aeiouy | 6 | OK |
