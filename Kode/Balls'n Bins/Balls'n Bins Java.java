import java.util.Random;

public class BallsBins
{
    
static int moreThanOneBallPerBin(int bins[], int noOfBins)
{
    int balls;
    balls = noOfBins;
    Random random = new Random();
     
    for (int i = 0; i < noOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < balls; i++)
    {
        int ix = random.nextInt(noOfBins);
        bins[ix]++;
    }

    int moreThanOne = 0;
    for (int i = 0; i < noOfBins; i++)
        if (bins[i] > 1)
            moreThanOne++;
    return moreThanOne;
}

static int maxBallsPerBin(int bins[], int noOfBins)
{
    int balls;
    balls = noOfBins;
    Random random = new Random();
     
    for (int i = 0; i < noOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < balls; i++)
    {
        int ix = random.nextInt(noOfBins);
        bins[ix]++;
    }

    int max = 0;
    for (int i = 0; i < noOfBins; i++)
        if (bins[i] > max)
            max = bins[i];
    return max;
}

static int ballsBinsPOTC(int bins[], int noOfBins)
{
    int balls;
    balls = noOfBins;
    Random random = new Random();
    
    for (int i = 0; i < noOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < balls; i++)
    {
        int ix1 = random.nextInt(noOfBins);
        int ix2 = random.nextInt(noOfBins);
        if (bins[ix1] > bins[ix2])
            bins[ix2]++;
        else
            bins[ix1]++;
    }

    int moreThanOne = 0;
    for (int i = 0; i < noOfBins; i++)
        if (bins[i] > 1)
        moreThanOne++;
    return moreThanOne;
}

static int maxBallsPerBinsPOTC(int bins[], int noOfBins)
{
    int balls;
    balls = noOfBins;
    Random random = new Random();
    
    for (int i = 0; i < noOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < balls; i++)
    {
        int ix1 = random.nextInt(noOfBins);
        int ix2 = random.nextInt(noOfBins);
        if (bins[ix1] > bins[ix2])
            bins[ix2]++;
        else
            bins[ix1]++;
    }

    int max = 0;
    for (int i = 0; i < noOfBins; i++)
        if (bins[i] > max)
        max = bins[i];
    return max;
}
static String mSquareNLessThanHalf(int size)
{
    int[] bins = new int[size*size]; 
    int yes = 0; int no = 0;
    Random random = new Random();
    
    for (int j = 0; j < 100; j++)
    {
        boolean found = false;
        for (int m = 0; m < size*size; m++)
            bins[m] = 0;
        for (int i = 0; i < size; i++)
        {
            int ix = random.nextInt(size*size);
            bins[ix]++;
        }
        for (int i = 0; i < size*size; i++)
        {
            if (bins[i] > 1)
            {
                yes++;
                found = true;
                break;
            }
        }
        if (!found) no++;
    }
    return "Yes: "+yes+" No: "+no;

}


public static void main(String[] args)
{
    System.out.println("This is balls and bins.");

    int noOfBins = 5000000;
   
    int[] bins;
    bins = new int[noOfBins];
    
    System.out.println( noOfBins + " Normal     " + moreThanOneBallPerBin(bins, noOfBins));
    System.out.println( noOfBins + " POTC       " + ballsBinsPOTC(bins, noOfBins));
    System.out.println( noOfBins + " Normal max " + maxBallsPerBin(bins, noOfBins));
    System.out.println( noOfBins + " POTC max   " + maxBallsPerBinsPOTC(bins, noOfBins));
    System.out.println(mSquareNLessThanHalf(11));
    
    
}
}
