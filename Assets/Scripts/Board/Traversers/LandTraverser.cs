using System.Collections.Generic;

public struct LandTraverser : ITraverser
{
    const int landTile = 1;
    const int hillTile = 2;

    HashSet<Point> obstacles;

    public LandTraverser(HashSet<Point> obstacles)
    {
        this.obstacles = obstacles;
    }

    public bool TryMove(Point fromPoint, Point toPoint, out int cost)
    {
        var system = IBoardSystem.Resolve();
        var hitObstacle = obstacles != null && obstacles.Contains(toPoint);
        if (system.IsPointOnBoard(toPoint) && !hitObstacle)
        {
            var type = system.GetTileType(toPoint);
            if (type == landTile || type == hillTile)
            {
                cost = (type == landTile) ? 5 : 10;
                return true;
            }
        }
        cost = int.MaxValue;//Points falling outside the board, hitting an obstacle, or being the wrong type of terrain
        return false;
    }
}