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