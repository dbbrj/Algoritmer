using System.Globalization;

/*
 * Opgave i grafer/aktivitetsplanlægning - kritisk vej (lektion 9 og 10).
 * Hele opgaveteksten står i "Øvelse 1 aktivitetsplanlægning.md".
 *
 * Kort:
 *   - Indlæs data.txt (event;task;duration), lav et Aktivitet-objekt pr. linje
 *     og læg dem i en liste ved navn tabel.
 *   - Udskriv antal aktiviteter og den gennemsnitlige varighed (3,7 dage).
 *   - Alle tasks i event k skal være færdige, før event k+1 kan starte.
 *     Udskriv varigheden af den kritiske vej (26 dage) og dens tasks
 *     (B, D, E, H, J). Det skal gøres ved at gennemløbe tabel, ikke filen.
 *   - Precondition: events kommer i stigende orden fra 1 uden huller.
 *
 * Opgaven siger vector/ArrayList. I C# svarer det til List<T>
 * (System.Collections.ArrayList findes også, men er ikke typesikker).
 */
public class Aktivitetsplan
{
    private static readonly CultureInfo Dansk = new CultureInfo("da-DK");

    private readonly List<Aktivitet> tabel = new List<Aktivitet>();

    // Indlæs aktiviteterne fra filen, isolér attributterne og læg
    // Aktivitet-objekterne i tabel.
    public Aktivitetsplan(string filnavn)
    {
        foreach (string linje in File.ReadAllLines(filnavn))
        {
            if (string.IsNullOrWhiteSpace(linje))
                continue;

            string[] dele = linje.Split(';');
            int @event = int.Parse(dele[0].Trim());
            string task = dele[1].Trim();
            int duration = int.Parse(dele[2].Trim());

            tabel.Add(new Aktivitet(@event, task, duration));
        }
    }

    public int AntalAktiviteter()
    {
        return tabel.Count;
    }

    public double GennemsnitligVarighed()
    {
        if (tabel.Count == 0)
            return 0;

        int sum = 0;
        foreach (Aktivitet a in tabel)
            sum += a.GetDuration();

        return (double)sum / tabel.Count;
    }

    // Kritisk vej: for hvert event er det den længste task, der bestemmer,
    // hvornår næste event kan starte. Den kritiske vej er derfor den længste
    // task i hvert event, og varigheden er summen af dem.
    //
    // Ét gennemløb af tabel: O(N).
    // Hvis flere tasks i samme event har samme (maksimale) varighed, er de alle
    // kritiske. De skrives så sammen med "/", fx "A/K".
    public int KritiskVej(out List<string> tasks)
    {
        tasks = new List<string>();
        int total = 0;

        int aktueltEvent = 0;  // events starter ved 1
        int max = -1;
        List<string> kritiske = new List<string>();

        foreach (Aktivitet a in tabel)
        {
            if (a.GetEvent() != aktueltEvent)
            {
                // Nyt event: afslut det forrige
                if (aktueltEvent != 0)
                {
                    total += max;
                    tasks.Add(string.Join("/", kritiske));
                }

                aktueltEvent = a.GetEvent();
                max = -1;
                kritiske = new List<string>();
            }

            if (a.GetDuration() > max)
            {
                max = a.GetDuration();
                kritiske.Clear();
                kritiske.Add(a.GetTask());
            }
            else if (a.GetDuration() == max)
            {
                kritiske.Add(a.GetTask());
            }
        }

        // Afslut det sidste event
        if (aktueltEvent != 0)
        {
            total += max;
            tasks.Add(string.Join("/", kritiske));
        }

        return total;
    }

    public void Udskriv()
    {
        Console.WriteLine($"Antal aktiviteter: {AntalAktiviteter()}");
        Console.WriteLine($"Gennemsnitlig varighed: {GennemsnitligVarighed().ToString("F1", Dansk)} dage");

        int varighed = KritiskVej(out List<string> tasks);
        Console.WriteLine($"Kritisk vej: {varighed} dage");
        Console.WriteLine($"Rækkefølge: {string.Join(", ", tasks)}");
    }
}
