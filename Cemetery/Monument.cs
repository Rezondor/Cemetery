namespace Cemetery;

public class Monument
{
    public Point StartPoint { get; init; }
    public Size Size { get; init; }
    public Monument(Point startPoint, Size size)
    {
        StartPoint = startPoint;
        Size = size;
    }
}
