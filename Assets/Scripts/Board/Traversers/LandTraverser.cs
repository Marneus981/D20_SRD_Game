using System.Collections.Generic;

public struct LandTraverser : ITraverser
{
    const int landTile = 1;
    const int hillTile = 2;

    HashSet<Point> passable;
    HashSet<Point> block;

    IBoardSystem system;

    public LandTraverser(HashSet<Point> passable, HashSet<Point> block)
    {
        this.passable = passable;
        this.block = block;
        system = IBoardSystem.Resolve();
    }

    public bool TryMove(Point fromPoint, Point toPoint, Size size, out int cost, out Traversal traversal)
    /*
    Note: We return true for any traversal that is still on the board. 
        This allows pathfinding to continue so A.I. can make decisions 
        about how to approach a destination even if it can't occupy it.
    */
    {
        if (size.ToTiles() > 1)
            traversal = SizeTraversal(size, toPoint);
        else
            traversal = SingleTraversal(toPoint);

        cost = int.MaxValue;
        if (traversal != Traversal.OffBoard)
        {
            var type = system.GetTileType(toPoint);
            cost = (type == landTile) ? 5 : 10;
            return true;
        }
        else
        {
            return false;
        }
    }

    Traversal SingleTraversal(Point point)
    {
        if (!system.IsPointOnBoard(point))
            return Traversal.OffBoard;
        if (block != null && block.Contains(point))
            return Traversal.Block;
        var type = system.GetTileType(point);
        if (!(type == landTile || type == hillTile))
            return Traversal.Block;
        if (passable != null && passable.Contains(point))
            return Traversal.Pass;
        return Traversal.Open;
    }

    Traversal SizeTraversal(Size size, Point point)
    /*
    Note: Method loops over all the tiles that would be occupied by an Entity of a given size
        if it were placed at the specified point. For each of its spaces, we call the SingleTraversal.
        If the current "check" point has a traversal type with a higher value (more restrictive) then
        we assign that value to the final "result". 
        Therefore the most restrictive traversal of all the spaces that could be occupied is the result that is returned.
    */
    {
        Traversal result = Traversal.Open;
        var range = size.ToTiles();
        for (int y = point.y; y < point.y + range; ++y)
        {
            for (int x = point.x; x < point.x + range; ++x)
            {
                var check = SingleTraversal(new Point(x, y));
                result = (int)check > (int)result ? check : result;
            }
        }
        return result;
    }
}