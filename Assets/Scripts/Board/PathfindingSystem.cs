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
/*                 UnityEngine.Debug.Log(string.Format("Map: Current Point: x={0},y={1}", point.x,point.y)); */
                var node = map[point];
                foreach (var offset in offsets)
                {
/*                     UnityEngine.Debug.Log(string.Format("Map: Current Offset: x={0},y={1}", offset.x,offset.y)); */
                    var nextPoint = point + offset;
/*                     UnityEngine.Debug.Log(string.Format("Map: nextPoint: x={0},y={1}", nextPoint.x,nextPoint.y)); */
                    int moveCost;
                    if (!traverser.TryMove(point, nextPoint, out moveCost))
                    {
/*                         UnityEngine.Debug.Log(string.Format("Map: TryMove: False, Cant Move there!; MoveCost: {0}", moveCost)); */
                        continue;
                    }
/*                     UnityEngine.Debug.Log(string.Format("Map: TryMove: True; MoveCost: {0}", moveCost)); */
                    var isDiagonal = offset.x != 0 && offset.y != 0;
/*                     UnityEngine.Debug.Log(string.Format("Map: Is offset diagonal? {0}", isDiagonal)); */
                    var diagonalPenalty = isDiagonal && node.diagonalActive;
/*                     UnityEngine.Debug.Log(string.Format("Map: Will a penalty for diagonal movement be incurred? {0}", diagonalPenalty)); */
                    var diagonalActive = isDiagonal ? !node.diagonalActive : node.diagonalActive;
/*                     UnityEngine.Debug.Log(string.Format("Map: diagonalActive: {0}", diagonalActive)); */
                    if (diagonalPenalty)
                        moveCost += 5;
/*                         UnityEngine.Debug.Log(string.Format("Map: diagonalPenalty incurred: MoveCost updated: {0}", moveCost)); */

                    moveCost += node.moveCost;
/*                     UnityEngine.Debug.Log(string.Format("Map: Total path MoveCost updated: {0}", moveCost)); */
                    if (moveCost > range)
                    {
/*                         UnityEngine.Debug.Log(string.Format("Map: Total path MoveCost exceeded range {0}: {1}", range, moveCost)); */
                        continue;
                    }
/*                     UnityEngine.Debug.Log(string.Format("Map: Total path MoveCost does not exceed range {0}: {1}", range, moveCost)); */
                    if (!map.ContainsKey(nextPoint))
                    {
/*                         UnityEngine.Debug.Log("Map: Current map DOES NOT contain nextPoint; Adding nextPoint to map..."); */
                        map[nextPoint] = new PathNode(nextPoint, moveCost, diagonalActive, node);
                        checkNext.Add(nextPoint);
                    }
                    else if (moveCost < map[nextPoint].moveCost)//update with more eff route
                    {
/*                         UnityEngine.Debug.Log("Map: Current map DOES contain nextPoint; Adding more efficient path for nextPoint to map..."); */
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