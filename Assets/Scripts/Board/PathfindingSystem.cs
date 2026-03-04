using System.Collections.Generic;

public interface ITraverser
/*
Responsible for knowing what moves are legal from one point to another
and knowing how much it would cost to make the move.
Might need diff traversers for diff type of movements or rulings (enmies are obstacles, etc)
*/
{
    bool TryMove(Point fromPoint, Point toPoint, out int cost);
}

public interface IPathfindingSystem : IDependency<IPathfindingSystem>
{
    IPathMap Map(Point start, int range, ITraverser traverser);//"flood" search outward from a "start" point
}
public class PathfindingSystem : IPathfindingSystem
{
    Point[] offsets = new Point[]
    {
        new Point(0, 1),
        new Point(1, 0),
        new Point(0, -1),
        new Point(-1, 0),
        new Point(1, 1),
        new Point(1, -1),
        new Point(-1, -1),
        new Point(-1, 1)
    };

    public IPathMap Map(Point start, int range, ITraverser traverser)
    {
        List<Point> checkNow = new List<Point>();
        HashSet<Point> checkNext = new HashSet<Point>();//Set to prevent re-evaluation of points
        Dictionary<Point, PathNode> map = new Dictionary<Point, PathNode>();
        map[start] = new PathNode(start, 0, false, null);
        checkNow.Add(start);

        while (checkNow.Count > 0)
        {
            foreach (var point in checkNow)
            {
                var node = map[point];
                foreach (var offset in offsets)
                {
                    var nextPoint = point + offset;

                    int moveCost;
                    if (!traverser.TryMove(point, nextPoint, out moveCost))
                        continue;

                    var isDiagonal = offset.x != 0 && offset.y != 0;
                    var diagonalPenalty = isDiagonal && node.diagonalActive;
                    var diagonalActive = isDiagonal ? !node.diagonalActive : node.diagonalActive;
                    if (diagonalPenalty)
                        moveCost += 5;

                    moveCost += node.moveCost;
                    if (moveCost > range)
                        continue;

                    if (!map.ContainsKey(nextPoint))
                    {
                        map[nextPoint] = new PathNode(nextPoint, moveCost, diagonalActive, node);
                        checkNext.Add(nextPoint);
                    }
                    else if (moveCost < map[nextPoint].moveCost)//update with more eff route
                    {
                        map[nextPoint].moveCost = moveCost;
                        map[nextPoint].diagonalActive = diagonalActive;
                        map[nextPoint].previous = node;
                        checkNext.Add(nextPoint);
                    }
                }
            }

            checkNow.Clear();
            checkNow.AddRange(checkNext);//Move unto next layer
            checkNext.Clear();
        }
        return new PathMap(map);
    }
}