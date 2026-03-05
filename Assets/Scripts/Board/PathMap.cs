using System.Collections.Generic;

public interface IPathMap
{
    List<Point> GetPathToPoint(Point point);
    List<Point> AllPoints();
    PathNode this[Point point] { get; }
    bool TryGetNode(out PathNode node, Point point);
    List<Point> OpenPoints(int range);
    Point NearestOpen(Point point, int range);
}

public class PathMap : IPathMap
{
    Dictionary<Point, PathNode> map;

    public PathMap(Dictionary<Point, PathNode> map)
    {
        this.map = map;
    }

    public List<Point> GetPathToPoint(Point point)
    {
        List<Point> result = new List<Point>();
        var node = map.ContainsKey(point) ? map[point] : null;
        while (node != null)
        {
            result.Add(node.point);
            node = node.previous;
        }
        result.Reverse();
        return result;
    }

    public List<Point> AllPoints()
    {
        return new List<Point>(map.Keys);
    }
    public PathNode this[Point point] //only for when we are sure the point exists
    {
        get { return map[point]; }
    }
    public bool TryGetNode(out PathNode node, Point point)
    /*
    Note:  points could be excluded because they may be outside the range that was provided to the pathfinding system,
        or because the pathfinding system will not continue FROM a blocked traversal tile 
        (even though the blocked tile itself will be in the map).
    */
    {
        if (map.ContainsKey(point))
        {
            node = map[point];
            return true;
        }
        node = null;
        return false;
    }
    public List<Point> OpenPoints(int range)//Return every point within the map whose node has a traversal type of "Open" and whose cost is within the specified range
    {
        List<Point> result = new List<Point>();
        foreach (var entry in map)
        {
            var node = entry.Value;
            if (node.traversal == Traversal.Open && node.moveCost <= range)
                result.Add(entry.Key);
        }
        return result;
    }
    public Point NearestOpen(Point point, int range)//Return the Point that is nearest to a specified Point from the perspective of the pathfinder
    {
        var node = map[point];
        while (node.moveCost > range || node.traversal != Traversal.Open)
        {
            node = node.previous;
        }
        return node.point;
    }
}