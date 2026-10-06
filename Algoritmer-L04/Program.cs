using System;

// Kører eksemplerne fra opgavebeskrivelsen.
public class Program
{
    public static void Main()
    {
        string text = "The cattle were running back and forth, but there was no wolf to be seen, heard or smelled, " +
                      "so the shepherd decided to take a little nap in a bed of grass and early summer flowers. " +
                      "Soon he was awakened by a sound he had never heard before.";
        Console.WriteLine($"Opgave 1: {WordCounter.MostFrequentWord(text)}"); // forventet: a

        int[] votes1 = { 7, 4, 3, 5, 3, 1, 6, 4, 5, 1, 7, 5 };
        Console.WriteLine($"Opgave 2 (eksempel): {Election.Majority(votes1, votes1.Length)}"); // forventet: -1

        int[] votes2 = { 2, 3, 2, 2, 1, 2, 2 };
        Console.WriteLine($"Opgave 2 (flertal): {Election.Majority(votes2, votes2.Length)}"); // forventet: 2
    }
}
