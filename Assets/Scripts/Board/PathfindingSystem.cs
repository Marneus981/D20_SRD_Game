using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public interface ITraverser
/*
Responsible for knowing what moves are legal from one point to another
and knowing how much it would cost to make the move.
Might need diff traversers for diff type of movements or rulings (enmies are obstacles, etc)
*/
{
    bool TryMove(Point fromPoint, Point toPoint, Size size, out int cost, out Traversal traversal);
}
public interface IPathfindingSystem : IDependency<IPathfindingSystem>
{
    IPathMap Map(Point start, int range, Size size, ITraverser traverser);
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
    public IPathMap Map(Point start, int range, Size size, ITraverser traverser)
    {
        List<Point> checkNow = new List<Point>();
        HashSet<Point> checkNext = new HashSet<Point>();
        Dictionary<Point, PathNode> map = new Dictionary<Point, PathNode>();
        map[start] = new PathNode(start, 0, false, null, Traversal.Open);
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
                    Traversal traversal;
                    if (!traverser.TryMove(point, nextPoint, size, out moveCost, out traversal))
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
                        map[nextPoint] = new PathNode(nextPoint, moveCost, diagonalActive, node, traversal);
                        if (traversal != Traversal.Block)
                            checkNext.Add(nextPoint);
                    }
                    else if (moveCost < map[nextPoint].moveCost)//update with more eff route
                    {
                        map[nextPoint].moveCost = moveCost;
                        map[nextPoint].diagonalActive = diagonalActive;
                        map[nextPoint].previous = node;
                        if (traversal != Traversal.Block)
                            checkNext.Add(nextPoint);
                    }
                }
            }
            checkNow.Clear();
            checkNow.AddRange(checkNext);
            checkNext.Clear();
        }
        return new PathMap(map);
    }
}