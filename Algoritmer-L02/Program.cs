public class Program
{
    public static void Main()
    {
        var checker = new ParenthesesChecker();

        // Hver testcase: (streng, forventet resultat)
        var testCases = new (string text, bool expected)[]
        {
            ("(a(b)c)", true),   // balanceret
            ("(()", false),      // mangler en lukke-parentes
            (")(", false),       // starter med lukke-parentes
            ("", true),          // tom streng er balanceret
            ("abc", true),       // ingen parenteser overhovedet
            ("(())", true),      // nestede, balancerede
            ("(()))", false),    // en ekstra lukke-parentes til sidst
            ("((", false),       // to åbne, ingen lukket
        };

        int passed = 0;
        foreach (var (text, expected) in testCases)
        {
            bool actual = checker.BalPar(text);
            bool ok = actual == expected;
            if (ok) passed++;

            string label = ok ? "OK  " : "FEJL";
            string display = text == "" ? "\"\" (tom streng)" : $"\"{text}\"";
            Console.WriteLine($"{label} - BalPar({display,-20}) = {actual,-5} (forventet {expected})");
        }

        Console.WriteLine();
        Console.WriteLine($"{passed}/{testCases.Length} test bestået.");

        Console.WriteLine();
        Console.WriteLine("--- Test af CircularQueue ---");
        var queue = new CircularQueue<int>(4);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Console.WriteLine("Enqueued 1, 2, 3");

        Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // forventer 1
        Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // forventer 2

        // Her er der plads igen (front er rykket), så vi kan enqueue
        // og "wrappe rundt" til starten af arrayet
        queue.Enqueue(4);
        queue.Enqueue(5);
        Console.WriteLine("Enqueued 4, 5 (5 bliver placeret ved arrayets start)");

        Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // forventer 3
        Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // forventer 4
        Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // forventer 5

        Console.WriteLine($"Er køen tom nu? {queue.IsEmpty()}"); // forventer true
        
        Console.WriteLine();
        Console.WriteLine("--- Test af LinkedList ---");
        var list = new LinkedList<int>();

        list.Add(5);
        list.Add(10);
        list.Add(15);
        Console.Write("Efter Add(5), Add(10), Add(15): ");
        list.Print();

        Console.WriteLine($"Size: {list.Size()}");                 // forventer 3
        Console.WriteLine($"Contains(10): {list.Contains(10)}");   // forventer true
        Console.WriteLine($"Contains(99): {list.Contains(99)}");   // forventer false

        list.Add(10); // findes allerede - bør ikke ændre noget
        Console.WriteLine($"Size efter Add(10) igen: {list.Size()}"); // forventer stadig 3

        list.Remove(10);
        Console.Write("Efter Remove(10): ");
        list.Print();
        Console.WriteLine($"Size: {list.Size()}"); // forventer 2
    }
}
