public class PathNode
{
    public Point point;
    public int moveCost;
    public bool diagonalActive;
    public PathNode previous;

    public PathNode(Point point, int moveCost, bool diagonalActive, PathNode previous)
    {
        this.point = point;
        this.moveCost = moveCost;
        this.diagonalActive = diagonalActive;
        this.previous = previous;
    }
}