/*
 * 4. Cuckoo Hashing (supplement)
 *
 * Skriv din egen implementering af en Cuckoo hash tabel. Din implementering skal kunne indsætte
 * og hente elementer og helst også kunne håndtere den situation, at loadfaktoren overskrider 0,5.
 */
using System;

public class CuckooHashTable
{
    private int?[] table1;
    private int?[] table2;
    private int capacity;
    private int count;
    private int maxKicks;

    public CuckooHashTable(int capacity = 11)
    {
        this.capacity = capacity;
        table1 = new int?[capacity];
        table2 = new int?[capacity];
        maxKicks = capacity;
    }

    public int Count => count;
    public int Capacity => capacity;
    public double LoadFactor => (double)count / (2 * capacity);

    private int Hash1(int key)
    {
        return ((key % capacity) + capacity) % capacity;
    }

    private int Hash2(int key)
    {
        // en anden hashfunktion end Hash1, så nøgler ikke altid kolliderer ens i begge tabeller
        int h = (key * 31 + 7) % capacity;
        return (h + capacity) % capacity;
    }

    public bool Contains(int key)
    {
        int i1 = Hash1(key);
        if (table1[i1].HasValue && table1[i1].Value == key) return true;

        int i2 = Hash2(key);
        if (table2[i2].HasValue && table2[i2].Value == key) return true;

        return false;
    }

    public bool Insert(int key)
    {
        if (Contains(key)) return true; // allerede i tabellen

        // Håndter høj loadfaktor proaktivt: hvis vi nærmer os 0,5, forstør tabellen inden vi forsøger at indsætte
        if ((double)(count + 1) / (2 * capacity) > 0.5)
        {
            Rehash(capacity * 2);
        }

        int current = key;
        for (int kicks = 0; kicks < maxKicks; kicks++)
        {
            int i1 = Hash1(current);
            if (!table1[i1].HasValue)
            {
                table1[i1] = current;
                count++;
                return true;
            }

            // smid den nuværende beboer ud og prøv at placere den i tabel 2
            int evicted = table1[i1].Value;
            table1[i1] = current;
            current = evicted;

            int i2 = Hash2(current);
            if (!table2[i2].HasValue)
            {
                table2[i2] = current;
                count++;
                return true;
            }

            evicted = table2[i2].Value;
            table2[i2] = current;
            current = evicted;
        }

        // Cyklus opdaget (for mange "kicks") -> forstør tabellen og prøv igen
        Rehash(capacity * 2);
        return Insert(current);
    }

    private void Rehash(int newCapacity)
    {
        var oldTable1 = table1;
        var oldTable2 = table2;

        capacity = newCapacity;
        maxKicks = capacity;
        table1 = new int?[capacity];
        table2 = new int?[capacity];
        count = 0;

        foreach (var v in oldTable1)
            if (v.HasValue) Insert(v.Value);

        foreach (var v in oldTable2)
            if (v.HasValue) Insert(v.Value);
    }

    public void Print()
    {
        Console.WriteLine($"Tabel 1 (kapacitet {capacity}):");
        for (int i = 0; i < capacity; i++)
            Console.WriteLine($"  [{i}] {(table1[i].HasValue ? table1[i].Value.ToString() : "-")}");

        Console.WriteLine($"Tabel 2 (kapacitet {capacity}):");
        for (int i = 0; i < capacity; i++)
            Console.WriteLine($"  [{i}] {(table2[i].HasValue ? table2[i].Value.ToString() : "-")}");
    }
}
