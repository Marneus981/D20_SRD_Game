using System.Collections.Generic;

public interface IPathMap
{
    List<Point> GetPathToPoint(Point point);
    List<Point> AllPoints();
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
}