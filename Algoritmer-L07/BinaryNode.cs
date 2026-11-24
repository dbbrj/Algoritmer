/*
 * Node i et (ubalanceret) binært søgetræ.
 * Flyttet ud af den udleverede BinarySearchTree-klasse, så hver klasse har sin egen fil.
 */
public class BinaryNode
{
    public BinaryNode(int theElement)
        : this(theElement, null, null)
    {
    }

    public BinaryNode(int theElement, BinaryNode? lt, BinaryNode? rt)
    {
        element = theElement;
        left = lt;
        right = rt;
    }

    public int element;
    public BinaryNode? left;
    public BinaryNode? right;
}
