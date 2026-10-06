/*
 * Øvelse (kø)
 *
 * Skriv en klasse, som implementerer en cirkulær kø og test den.
 *
 * En cirkulær kø kan implementeres som et array (med fast størrelse). Når man når til det sidste
 * element, gås videre til det første element, hvis der er plads. Køen kan således løbe fuld.
 * En udvidet version kunne udvide arrayet, når det løber fuldt.
 */
public class CircularQueue<T>
{
    private T[] items;
    private int front = 0;
    private int rear = -1;
    private int count = 0;

    public CircularQueue(int capacity)
    {
        items = new T[capacity];
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public bool IsFull()
    {
        return count == items.Length;
    }

    public void Enqueue(T item)
    {
        if (IsFull())
            throw new InvalidOperationException("Køen er fuld - kan ikke enqueue.");

        rear = (rear + 1) % items.Length;
        items[rear] = item;
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Køen er tom - kan ikke dequeue.");

        T value = items[front];
        front = (front + 1) % items.Length;
        count--;
        return value;
    }

}
