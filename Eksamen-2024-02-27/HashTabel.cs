/*
 * Reeksamen 27. februar 2024 – Opgave 6 (15 %)
 *
 * Opgaven:
 *   Denne opgave går ud på at etablere to hashtabeller, begge med plads til
 *   16 elementer.
 *
 *   I den første (7 %) skal elementerne med nøglerne D,E,M,O,C,R,A,T indsættes.
 *   Hashfunktionen er
 *
 *     (11*k) % tableSize
 *
 *   hvor k er tallets placering/nummer i det engelske alfabet (se opgave 3).
 *   Eksempel: hash('C') = hash(3) = 11*3%16=33%16=1
 *   Ved kollisioner (et element hasher til et optaget indeks) anvendes
 *   linear probing.
 *
 *   I den anden tabel (8 %) skal elementerne med nøglerne R,E,P,U,B,L,I,C,A,N
 *   indsættes. Der anvendes samme hashfunktion som i den første og quadratic
 *   probing ved kollisioner.
 *
 * Klassen simulerer indsættelserne og skriver hvert forsøg ud, så tabellerne
 * i "Opgave 6 hashtabeller.md" kan efterprøves.
 */
public class HashTabel
{
    private readonly char?[] tabel;
    private readonly bool kvadratisk;

    public HashTabel(int størrelse, bool kvadratisk)
    {
        tabel = new char?[størrelse];
        this.kvadratisk = kvadratisk;
    }

    // k = bogstavets nummer i alfabetet (A = 1, B = 2, ...).
    public int Hash(char bogstav)
    {
        int k = bogstav - 'A' + 1;
        return (11 * k) % tabel.Length;
    }

    // Forsøg i = 0, 1, 2, ...:
    //   linear probing:    (hash + i)   % size
    //   quadratic probing: (hash + i^2) % size
    // Returnerer en tekst, der beskriver de prøvede pladser.
    public string Indsæt(char bogstav)
    {
        int h = Hash(bogstav);
        var forsøg = new List<int>();
        for (int i = 0; i < tabel.Length; i++)
        {
            int offset = kvadratisk ? i * i : i;
            int pos = (h + offset) % tabel.Length;
            forsøg.Add(pos);
            if (tabel[pos] == null)
            {
                tabel[pos] = bogstav;
                return $"{bogstav}: k={bogstav - 'A' + 1}, hash={h}, prøvet {string.Join(" -> ", forsøg)} => indsat på {pos}";
            }
        }
        return $"{bogstav}: kunne ikke indsættes (prøvet {string.Join(" -> ", forsøg)})";
    }

    public char? this[int indeks] => tabel[indeks];

    public int Størrelse => tabel.Length;

    public override string ToString()
    {
        var linjer = new List<string>();
        for (int i = 0; i < tabel.Length; i++)
            linjer.Add($"{i,2}: {(tabel[i]?.ToString() ?? "-")}");
        return string.Join(Environment.NewLine, linjer);
    }
}
