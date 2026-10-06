/*
 * Opgave 2 (anslået løsningstid 35 minutter)
 *
 * Nedenstående array repræsenterer de afgivne stemmer ved et valg.
 *     {7,4,3,5,3,1,6,4,5,1,7,5}
 * Der er i eksemplet opstillet 7 kandidater (1-7), og der er afgivet i alt 12 stemmer. Kandidat 6 har
 * fået 1 stemme, kandidaterne 1, 3, 4 og 7 har fået hver 2 stemmer, kandidat 5 har fået 3 stemmer,
 * og kandidat 2 har fået 0 stemmer.
 *
 * Opgaven går ud på at skrive en algoritme som kaldt med arrayet (og evt. arrayets længde) kan
 * afgøre om en kandidat har fået over 50 % af stemmerne. I givet fald returneres kandidatens
 * nummer. Hvis ingen kandidat har fået flertal returneres -1. Der kan være opstillet et vilkårligt antal
 * kandidater, dog mindst to.
 *
 * I ovenstående eksempel har ingen af kandidaterne opnået flertal, og der skulle således returneres
 * -1.
 *
 * Angiv tillige din algoritmes tidskompleksitet.
 *
 * ---------------------------------------------------------------------------------------------
 * Løsning: Boyer-Moore majority vote med et verificerende andet gennemløb.
 * Tidskompleksitet: O(n) tid (to lineære gennemløb), O(1) ekstra plads.
 */
public static class Election
{
    // Returnerer kandidaten med over 50 % af stemmerne, ellers -1.
    // Boyer-Moore majority vote: O(n) tid, O(1) ekstra plads.
    public static int Majority(int[] votes, int n)
    {
        // Fase 1: find den eneste kandidat, der KAN have flertal.
        int candidate = -1;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            if (count == 0)
            {
                candidate = votes[i];
                count = 1;
            }
            else if (votes[i] == candidate)
            {
                count++;
            }
            else
            {
                count--;
            }
        }

        // Fase 2: tjek at kandidaten faktisk har over halvdelen.
        count = 0;
        for (int i = 0; i < n; i++)
        {
            if (votes[i] == candidate) count++;
        }

        return count * 2 > n ? candidate : -1;
    }
}
