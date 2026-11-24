/*
 * Øvelser, lektion 7 (træer del 1) - kodes på den udleverede BST-kode:
 *
 *   1. Skriv metoder til pre-order, in-order, post-order og level order
 *      traversering af binære (søge) træer.
 *   2. Skriv en metode, som kan beregne internal path length af et binært træ.
 *   3. 4.31 (Weiss): Write efficient methods that take only a reference to the
 *      root of a binary tree, T, and compute
 *        a. The number of nodes in T.
 *        b. The number of leaves in T.
 *        c. The number of full nodes in T.
 *      What is the running time of your routines?
 *   4. Skriv en metode, som kan skabe et perfekt binært træ med højden h (>0).
 *
 * Den udleverede kode (Insert, Remove, FindMin, FindMax, Contains, MakeEmpty,
 * IsEmpty, PrintTree, Height) er uændret. Nye metoder står under
 * "ØVELSE 1" - "ØVELSE 4". Main er flyttet til Program.cs.
 * Forklaringer står i md-filerne "Øvelse 1" - "Øvelse 4".
 */
public class BinarySearchTree
{
    public BinarySearchTree()
    {
        root = null;
    }

    // Bruges af øvelse 4 til at lave et træ ud fra en færdig rod.
    private BinarySearchTree(BinaryNode? root)
    {
        this.root = root;
    }

    public BinaryNode? Root => root;

    // Insert into the tree; duplicates are ignored.
    public void Insert(int x)
    {
        root = Insert(x, root);
    }

    public void Remove(int x)
    {
        root = Remove(x, root);
    }

    public BinaryNode? FindMin()
    {
        if (IsEmpty())
            return null;

        return FindMin(root);
    }

    public BinaryNode? FindMax()
    {
        if (IsEmpty())
            return null;

        return FindMax(root);
    }

    public bool Contains(int x)
    {
        return Contains(x, root);
    }

    public void MakeEmpty()
    {
        root = null;
    }

    public bool IsEmpty()
    {
        return root == null;
    }

    public void PrintTree()
    {
        if (IsEmpty())
            Console.WriteLine("Empty tree");
        else
            PrintTree(root);
    }

    public int Height()
    {
        return Height(root);
    }

    // =====================================================================
    // ØVELSE 1: traverseringer
    // Hver metode returnerer elementerne i den rækkefølge, de besøges.
    // Alle fire besøger hver node præcis én gang: O(N).
    // =====================================================================

    // Pre-order: node, venstre, højre
    public List<int> PreOrder()
    {
        var result = new List<int>();
        PreOrder(root, result);
        return result;
    }

    private void PreOrder(BinaryNode? t, List<int> result)
    {
        if (t == null)
            return;

        result.Add(t.element);
        PreOrder(t.left, result);
        PreOrder(t.right, result);
    }

    // In-order: venstre, node, højre (giver sorteret rækkefølge i et BST)
    public List<int> InOrder()
    {
        var result = new List<int>();
        InOrder(root, result);
        return result;
    }

    private void InOrder(BinaryNode? t, List<int> result)
    {
        if (t == null)
            return;

        InOrder(t.left, result);
        result.Add(t.element);
        InOrder(t.right, result);
    }

    // Post-order: venstre, højre, node
    public List<int> PostOrder()
    {
        var result = new List<int>();
        PostOrder(root, result);
        return result;
    }

    private void PostOrder(BinaryNode? t, List<int> result)
    {
        if (t == null)
            return;

        PostOrder(t.left, result);
        PostOrder(t.right, result);
        result.Add(t.element);
    }

    // Level order: niveau for niveau, fra venstre mod højre.
    // Ikke rekursiv - bruger en kø (bredde-først).
    public List<int> LevelOrder()
    {
        var result = new List<int>();
        if (root == null)
            return result;

        var queue = new Queue<BinaryNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            BinaryNode t = queue.Dequeue();
            result.Add(t.element);

            if (t.left != null)
                queue.Enqueue(t.left);
            if (t.right != null)
                queue.Enqueue(t.right);
        }

        return result;
    }

    // =====================================================================
    // ØVELSE 2: internal path length
    // Summen af dybderne af alle noder (Weiss' definition: bladene tæller med).
    // Roden har dybde 0. O(N).
    // =====================================================================

    public int InternalPathLength()
    {
        return InternalPathLength(root, 0);
    }

    private int InternalPathLength(BinaryNode? t, int depth)
    {
        if (t == null)
            return 0;

        return depth
             + InternalPathLength(t.left, depth + 1)
             + InternalPathLength(t.right, depth + 1);
    }

    // =====================================================================
    // ØVELSE 3: opgave 4.31
    // Metoderne tager kun en reference til roden (static), så de virker på
    // ethvert binært træ - ikke kun dette søgetræ. Hver er O(N).
    // =====================================================================

    // a. Antal noder
    public static int CountNodes(BinaryNode? t)
    {
        if (t == null)
            return 0;

        return 1 + CountNodes(t.left) + CountNodes(t.right);
    }

    // b. Antal blade (noder uden børn)
    public static int CountLeaves(BinaryNode? t)
    {
        if (t == null)
            return 0;

        if (t.left == null && t.right == null)
            return 1;

        return CountLeaves(t.left) + CountLeaves(t.right);
    }

    // c. Antal fulde noder (noder med præcis to børn)
    public static int CountFullNodes(BinaryNode? t)
    {
        if (t == null)
            return 0;

        int self = (t.left != null && t.right != null) ? 1 : 0;
        return self + CountFullNodes(t.left) + CountFullNodes(t.right);
    }

    // =====================================================================
    // ØVELSE 4: perfekt binært træ med højden h (> 0)
    // Et perfekt træ med højden h har 2^(h+1) - 1 noder.
    // Nøglerne 1 .. 2^(h+1) - 1 fordeles ved altid at vælge midten som rod,
    // så resultatet også er et gyldigt binært søgetræ. O(N).
    // =====================================================================

    public static BinarySearchTree CreatePerfectTree(int h)
    {
        if (h <= 0)
            throw new ArgumentOutOfRangeException(nameof(h), "Højden skal være > 0.");

        int n = (1 << (h + 1)) - 1; // 2^(h+1) - 1
        return new BinarySearchTree(BuildPerfect(1, n));
    }

    private static BinaryNode? BuildPerfect(int low, int high)
    {
        if (low > high)
            return null;

        int mid = (low + high) / 2;
        return new BinaryNode(mid, BuildPerfect(low, mid - 1), BuildPerfect(mid + 1, high));
    }

    // =====================================================================
    // Udleveret kode (uændret)
    // =====================================================================

    private BinaryNode Insert(int x, BinaryNode? t)
    {
        if (t == null)
            return new BinaryNode(x, null, null);

        if (x < t.element)
            t.left = Insert(x, t.left);
        else
        if (x > t.element)
            t.right = Insert(x, t.right);
        else
            ; // Duplicate; do nothing

        return t;
    }

    private BinaryNode? Remove(int x, BinaryNode? t)
    {
        if (t == null)
            return t; // Item not found; do nothing

        if (x < t.element)
            t.left = Remove(x, t.left);
        else
        if (x > t.element)
            t.right = Remove(x, t.right);
        else
        if (t.left != null && t.right != null) // Two children
        {
            t.element = FindMin(t.right)!.element;
            t.right = Remove(t.element, t.right);
        }
        else
        {
            t = (t.left != null) ? t.left : t.right;
        }

        return t;
    }

    private BinaryNode? FindMin(BinaryNode? t)
    {
        if (t == null)
            return null;
        else
        if (t.left == null)
            return t;

        return FindMin(t.left);
    }

    private BinaryNode? FindMax(BinaryNode? t)
    {
        if (t != null)
        {
            while (t.right != null)
                t = t.right;
        }

        return t;
    }

    private bool Contains(int x, BinaryNode? t)
    {
        if (t == null)
            return false;

        if (x < t.element)
            return Contains(x, t.left);
        else
        if (x > t.element)
            return Contains(x, t.right);
        else
            return true; // Match
    }

    private void PrintTree(BinaryNode? t)
    {
        if (t != null)
        {
            PrintTree(t.left);
            Console.WriteLine(t.element);
            PrintTree(t.right);
        }
    }

    private int Height(BinaryNode? t)
    {
        if (t == null)
            return -1;
        else
            return 1 + Math.Max(Height(t.left), Height(t.right));
    }

    private BinaryNode? root;
}
