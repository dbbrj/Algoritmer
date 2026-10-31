/*
 * Bruges af opgave 7.38: et punkt i planen med heltalskoordinater.
 */
public readonly struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Ordning: først efter X, derefter efter Y.
    // Bruges til at finde det "mindste" punkt på en linje.
    public int CompareTo(Point other)
    {
        if (X != other.X)
            return X.CompareTo(other.X);
        return Y.CompareTo(other.Y);
    }

    public override string ToString() => $"({X}, {Y})";
}
