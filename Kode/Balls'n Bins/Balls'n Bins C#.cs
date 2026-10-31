using System;

public class BallsBins
{
    // Use one Random object for the whole program.
    // This is simpler and avoids creating a new random-number generator
    // every time a method is called.
    static Random random = new Random();

    static int MoreThanOneBallPerBin(int[] bins)
    {
        Array.Clear(bins, 0, bins.Length);

        // We use the same number of balls as bins.
        for (int i = 0; i < bins.Length; i++)
        {
            int bin = random.Next(bins.Length);
            bins[bin]++;
        }

        int count = 0;

        foreach (int balls in bins)
        {
            if (balls > 1)
                count++;
        }

        return count;
    }

    static int MaxBallsPerBin(int[] bins)
    {
        Array.Clear(bins, 0, bins.Length);

        for (int i = 0; i < bins.Length; i++)
        {
            int bin = random.Next(bins.Length);
            bins[bin]++;
        }

        int max = 0;

        foreach (int balls in bins)
        {
            if (balls > max)
                max = balls;
        }

        return max;
    }

    static int BallsBinsPOTC(int[] bins)
    {
        Array.Clear(bins, 0, bins.Length);

        for (int i = 0; i < bins.Length; i++)
        {
            // Power of Two Choices:
            // choose two bins and put the ball in the less loaded one.
            int bin1 = random.Next(bins.Length);
            int bin2 = random.Next(bins.Length);

            if (bins[bin1] > bins[bin2])
                bins[bin2]++;
            else
                bins[bin1]++;
        }

        int count = 0;

        foreach (int balls in bins)
        {
            if (balls > 1)
                count++;
        }

        return count;
    }

    static int MaxBallsPerBinPOTC(int[] bins)
    {
        Array.Clear(bins, 0, bins.Length);

        for (int i = 0; i < bins.Length; i++)
        {
            int bin1 = random.Next(bins.Length);
            int bin2 = random.Next(bins.Length);

            if (bins[bin1] > bins[bin2])
                bins[bin2]++;
            else
                bins[bin1]++;
        }

        int max = 0;

        foreach (int balls in bins)
        {
            if (balls > max)
                max = balls;
        }

        return max;
    }

    static string MSquareNLessThanHalf(int size)
    {
        int[] bins = new int[size * size];

        int yes = 0;
        int no = 0;

        for (int experiment = 0; experiment < 100; experiment++)
        {
            Array.Clear(bins, 0, bins.Length);

            // Throw 'size' balls into 'size squared' bins.
            for (int i = 0; i < size; i++)
            {
                int bin = random.Next(bins.Length);
                bins[bin]++;
            }

            bool collisionFound = false;

            foreach (int balls in bins)
            {
                if (balls > 1)
                {
                    collisionFound = true;
                    break;
                }
            }

            if (collisionFound)
                yes++;
            else
                no++;
        }

        return $"Yes: {yes} No: {no}";
    }

    public static void Main()
    {
        Console.WriteLine("This is balls and bins.");

        int numberOfBins = 5_000_000;
        int[] bins = new int[numberOfBins];

        Console.WriteLine($"{numberOfBins} Normal     {MoreThanOneBallPerBin(bins)}");
        Console.WriteLine($"{numberOfBins} POTC       {BallsBinsPOTC(bins)}");
        Console.WriteLine($"{numberOfBins} Normal max {MaxBallsPerBin(bins)}");
        Console.WriteLine($"{numberOfBins} POTC max   {MaxBallsPerBinPOTC(bins)}");

        Console.WriteLine(MSquareNLessThanHalf(11));
    }
}
