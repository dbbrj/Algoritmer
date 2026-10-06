public class ParenthesesChecker
{
    public bool BalPar(string text)
    {
        MyStack<char> stack = new MyStack<char>();

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                if (stack.IsEmpty())
                {
                    return false;
                }
                else
                {
                    stack.Pop();
                }
            }
        }
        return stack.IsEmpty();
    }

}