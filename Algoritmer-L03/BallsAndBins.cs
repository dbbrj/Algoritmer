/*
 * 3. Balls'n Bins
 *
 * Denne øvelse handler om de hashing-relaterede emner: ensartet distribution og the power of two
 * choices.
 *
 * Balls'n Bins-problemet handler om tilfældig fordeling af et antal bolde i et tilsvarende antal
 * beholdere (fx 97 bolde i 97 beholdere), og hvorledes man kan regne ud, hvor mange bolde der skal
 * være plads til i hver beholder for med meget stor sandsynlighed at kunne undgå, at beholderen
 * 'løber over' (overflow).
 * Implementér og test Balls & Bins problemet og besvar løs følgende opgaver.
 *
 *   1. Det essentielle problem er at finde ud af, hvor mange bolde der skal være plads til i hver
 *      beholder for med stor sandsynlighed at undgå overløb. Hvad vil være et godt estimat være for
 *      antal bolde pr beholder, hvis 10.007 bolde skal fordeles i 10.007 beholdere? Opgaven kan
 *      løses ved at udføre en række eksperimenter, hvor gennemsnitsværdier og maksimumsværdier for
 *      antallet af bolde beregnes.
 *   2. Gentag forsøget for 32,749 bolde og beholdere og sammenlign med resultaterne for opgave 1.
 *   3. Løs opgave 1 og 2 med the power of two choices princip, dvs.: vælg to beholdere tilfældigt
 *      og put bolden i den beholder med det færreste antal bolde.
 *   4. Weiss anfører i sektion 5.7, at med 'almindeligt' B&B eksperiment er den forventede værdi af
 *      det maksimale antal bolde pr beholder Θ(logN / log log N) og Θ(log log N) for power of two
 *      choices. Kan dine eksperimenter bekræfte disse estimater?
 *   5. Theorem 5.2 anfører, at hvis N bolde placeres i M = N^2 beholdere, så er sandsynligheden for,
 *      at ingen beholder indeholder mere end én bold mindre end 0,5. Udfør eksperimenter som be-
 *      eller afkræfter denne teori.
 */
public class BallsAndBins
{
    private readonly Random rng = new Random();

    // Single choice: hver bold vælger én tilfældig beholder
    public int SingleChoiceMax(int balls, int bins)
    {
        int[] load = new int[bins];
        for (int i = 0; i < balls; i++)
        {
            int b = rng.Next(bins);
            load[b]++;
        }
        return Max(load);
    }

    // Power of two choices: vælg to tilfældige beholdere, læg bolden i den mindst fyldte
    public int PowerOfTwoChoicesMax(int balls, int bins)
    {
        int[] load = new int[bins];
        for (int i = 0; i < balls; i++)
        {
            int b1 = rng.Next(bins);
            int b2 = rng.Next(bins);
            int chosen = (load[b1] <= load[b2]) ? b1 : b2;
            load[chosen]++;
        }
        return Max(load);
    }

    private static int Max(int[] arr)
    {
        int max = 0;
        foreach (int v in arr) if (v > max) max = v;
        return max;
    }

    // Teoretiske estimater fra Weiss sektion 5.7
    public static double TheorySingle(double n)
    {
        double lnN = Math.Log(n);
        double lnlnN = Math.Log(lnN);
        return lnN / lnlnN;
    }

    public static double TheoryPow2(double n)
    {
        double lnN = Math.Log(n);
        double lnlnN = Math.Log(lnN);
        return lnlnN / Math.Log(2);
    }

    // Opgave 5: Theorem 5.2 - N bolde i M = N^2 beholdere.
    // Sandsynlighed for at ingen beholder får mere end én bold.
    public double ProbNoCollision(int n, int trials)
    {
        long m = (long)n * n;
        int successes = 0;

        for (int t = 0; t < trials; t++)
        {
            var seen = new HashSet<long>();
            bool collision = false;
            for (int i = 0; i < n; i++)
            {
                long b = (long)(rng.NextDouble() * m);
                if (!seen.Add(b))
                {
                    collision = true;
                    break;
                }
            }
            if (!collision) successes++;
        }
        return (double)successes / trials;
    }
}