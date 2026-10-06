/*
 * Øvelse (liste) - Øvelse 3.11 i lærebogen
 *
 * Find en implementering af linked list på nettet eller skriv din egen.
 *
 * Øvelse 3.11 (Weiss), gengivet i kort form: en enkelthægtet liste implementeres med en
 * header-node (men uden tail-node). Skriv en klasse med metoder, der kan:
 *   a. returnere listens størrelse
 *   b. udskrive listen
 *   c. teste om en værdi x findes i listen
 *   d. tilføje x, hvis den ikke allerede findes
 *   e. fjerne x, hvis den findes
 */
public class LinkedList<T>
{
    private class Node<T>
    {
        public T Value;
        public Node<T> Next;

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node<T> head;

    public LinkedList()
    {
        head = new Node<T>(default(T));
    }

    // a. størrelse
    public int Size()
    {
        int count = 0;
        Node<T> current = head.Next;
        while (current != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }

    // b. udskriv listen
    public void Print()
    {
        Node<T> current = head.Next;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    // c. indeholder listen x?
    public bool Contains(T x)
    {
        Node<T> current = head.Next;
        while (current != null)
        {
            if (current.Value.Equals(x))
                return true;
            current = current.Next;
        }
        return false;
    }

    // d. tilføj x, hvis den ikke allerede findes
    public void Add(T x)
    {
        if (Contains(x))
            return;

        Node<T> newNode = new Node<T>(x);
        newNode.Next = head.Next;
        head.Next = newNode;
    }

    // e. fjern x, hvis den findes
    public void Remove(T x)
    {
        Node<T> current = head;
        while (current.Next != null)
        {
            if (current.Next.Value.Equals(x))
            {
                current.Next = current.Next.Next;
                return;
            }
            current = current.Next;
        }
    }
}
