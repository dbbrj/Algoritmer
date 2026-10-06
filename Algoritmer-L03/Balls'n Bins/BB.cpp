#include <iostream>
#include <random>
#include <string>

using namespace std;

// One random-number generator is used throughout the program.
// random_device provides the initial seed.
mt19937 generator(random_device{}());

// Returns a uniformly distributed random integer
// in the range 0 to numberOfBins - 1.
int randomBin(int numberOfBins)
{
    uniform_int_distribution<int> distribution(0, numberOfBins - 1);
    return distribution(generator);
}

int moreThanOneBallPerBin(int bins[], int numberOfBins)
{
    // Empty all bins.
    for (int i = 0; i < numberOfBins; i++)
        bins[i] = 0;

    // Throw one ball for each bin.
    for (int i = 0; i < numberOfBins; i++)
    {
        int ix = randomBin(numberOfBins);
        bins[ix]++;
    }

    // Count bins containing more than one ball.
    int moreThanOne = 0;

    for (int i = 0; i < numberOfBins; i++)
        if (bins[i] > 1)
            moreThanOne++;

    return moreThanOne;
}

int maxBallsPerBin(int bins[], int numberOfBins)
{
    for (int i = 0; i < numberOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < numberOfBins; i++)
    {
        int ix = randomBin(numberOfBins);
        bins[ix]++;
    }

    int max = 0;

    for (int i = 0; i < numberOfBins; i++)
        if (bins[i] > max)
            max = bins[i];

    return max;
}

int ballsBinsPOTC(int bins[], int numberOfBins)
{
    for (int i = 0; i < numberOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < numberOfBins; i++)
    {
        // Power of Two Choices:
        // choose two bins and place the ball in the less loaded one.
        int ix1 = randomBin(numberOfBins);
        int ix2 = randomBin(numberOfBins);

        if (bins[ix1] > bins[ix2])
            bins[ix2]++;
        else
            bins[ix1]++;
    }

    int moreThanOne = 0;

    for (int i = 0; i < numberOfBins; i++)
        if (bins[i] > 1)
            moreThanOne++;

    return moreThanOne;
}

int maxBallsPerBinPOTC(int bins[], int numberOfBins)
{
    for (int i = 0; i < numberOfBins; i++)
        bins[i] = 0;

    for (int i = 0; i < numberOfBins; i++)
    {
        int ix1 = randomBin(numberOfBins);
        int ix2 = randomBin(numberOfBins);

        if (bins[ix1] > bins[ix2])
            bins[ix2]++;
        else
            bins[ix1]++;
    }

    int max = 0;

    for (int i = 0; i < numberOfBins; i++)
        if (bins[i] > max)
            max = bins[i];

    return max;
}

string mSquareNLessThanHalf(int size)
{
    int numberOfBins = size * size;
    int* bins = new int[numberOfBins];

    int yes = 0;
    int no = 0;

    // Repeat the experiment 100 times.
    for (int experiment = 0; experiment < 100; experiment++)
    {
        for (int i = 0; i < numberOfBins; i++)
            bins[i] = 0;

        // Throw 'size' balls into 'size squared' bins.
        for (int i = 0; i < size; i++)
        {
            int ix = randomBin(numberOfBins);
            bins[ix]++;
        }

        bool found = false;

        for (int i = 0; i < numberOfBins; i++)
        {
            if (bins[i] > 1)
            {
                found = true;
                break;
            }
        }

        if (found)
            yes++;
        else
            no++;
    }

    delete[] bins;

    return "Yes: " + to_string(yes) + " No: " + to_string(no);
}

int main()
{
    cout << "This is balls and bins." << endl;

    int numberOfBins = 5'000'000;

    int* bins = new int[numberOfBins];

    cout << numberOfBins << " Normal     "
        << moreThanOneBallPerBin(bins, numberOfBins) << endl;

    cout << numberOfBins << " POTC       "
        << ballsBinsPOTC(bins, numberOfBins) << endl;

    cout << numberOfBins << " Normal max "
        << maxBallsPerBin(bins, numberOfBins) << endl;

    cout << numberOfBins << " POTC max   "
        << maxBallsPerBinPOTC(bins, numberOfBins) << endl;

    cout << mSquareNLessThanHalf(11) << endl;

    delete[] bins;

    return 0;
}
