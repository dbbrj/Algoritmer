public class MyStack<T>
{
    /* Øvelse(stak)
    Implementér din egen stak og skriv dernæst nedenstående metode. Du skal bruge stakken. Tællere må ikke anvendes:
    boolean balPar(Stringtext);
    Metoden tjekker om parenteser () i parameteren er balanceret. En udvidet version vil kunne omfatte {} og[].*/

    private T[] items = new T[10];
    private int top = -1;
    public void Push(T item)
    {
        if (top == items.Length - 1)
        {
            T[] newItems = new T[items.Length * 2];
            Array.Copy(items, newItems, items.Length);
            items = newItems;
        }
        top++;
        items[top] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stakken er tom, kan ikke poppe.");
        T value = items[top];
        top--;
        return value;
    }

    public bool IsEmpty()
    {
        return top == -1;
    }
}