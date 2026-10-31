
class BinarySearchTree
{ 
    public BinarySearchTree()
    {
        root = null;
    }

    // Insert into the tree; duplicates are ignored.
    public void Insert(int x)
    {
        root = Insert(x, root);
    }

    public void Remove(int x)
    {
        root = Remove(x, root);
    }

    public BinaryNode FindMin()
    {
        if (IsEmpty())
            return null;

        return FindMin(root);
    }

    public BinaryNode FindMax()
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

    private BinaryNode Insert(int x, BinaryNode t)
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

    private BinaryNode Remove(int x, BinaryNode t)
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
            t.element = FindMin(t.right).element;
            t.right = Remove(t.element, t.right);
        }
        else
        {
            t = (t.left != null) ? t.left : t.right;
        }

        return t;
    }

    private BinaryNode FindMin(BinaryNode t)
    {
        if (t == null)
            return null;
        else 
        if (t.left == null)
            return t;

        return FindMin(t.left);
    }

    private BinaryNode FindMax(BinaryNode t)
    {
        if (t != null)
        {
            while (t.right != null)
            t = t.right;
        }

        return t;
    }

    private bool Contains(int x, BinaryNode t)
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

    private void PrintTree(BinaryNode t)
    {
        if (t != null)
        {
            PrintTree(t.left);
            Console.WriteLine(t.element);
            PrintTree(t.right);
        }
    }

    private int Height(BinaryNode t)
    {
        if (t == null)
            return -1;
        else
            return 1 + Math.Max(Height(t.left), Height(t.right));
    }

    // Basic node stored in unbalanced binary search trees
    public class BinaryNode
    {
        public BinaryNode(int theElement)
            : this(theElement, null, null)
        {
        }

        public BinaryNode(int theElement, BinaryNode lt, BinaryNode rt)
        {
            element = theElement;
            left = lt;
            right = rt;
        }

        public int element;
        public BinaryNode left;
        public BinaryNode right;
    }

    private BinaryNode root;
// Test program
    public static void Main(string[] args)
    {
        BinarySearchTree t = new BinarySearchTree();

        t.Insert(7);
        t.Insert(4);
        t.Insert(28);
        t.Insert(3);
        t.Insert(55);
        t.Insert(2);
        t.Insert(51);
        t.Insert(60);
        t.Insert(1);
        t.Insert(48);
        t.Insert(58);
        t.Insert(69);
        t.Insert(40);
        t.Insert(57);
        t.Insert(35);

        t.Remove(55);
        t.Remove(3);

        Console.WriteLine(t.Contains(80));
        t.PrintTree();
    }
}

