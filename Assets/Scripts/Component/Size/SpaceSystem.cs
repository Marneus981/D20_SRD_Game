using System.Collections.Generic;

public interface ISpaceSystem : IDependency<ISpaceSystem>
{
    int SpaceInFeet(Size size);
    int SpaceInTiles(Size size);
    List<Point> SpaceTileOffsets(Size size);
    List<Point> SpaceTiles(Size size, Point position);
}

public class SpaceSystem : ISpaceSystem
{
    const int tileSize = 5;

    public int SpaceInFeet(Size size)
    {
        switch (size)
        {
            case Size.Large: return 10;
            case Size.Huge: return 15;
            case Size.Gargantuan: return 20;
            default: return 5;
        }
    }

    public int SpaceInTiles(Size size)
    {
        var space = SpaceInFeet(size);
        return space / tileSize;
    }

    public List<Point> SpaceTileOffsets(Size size)
    {
        return SpaceTiles(size, new Point(0, 0));
    }

    public List<Point> SpaceTiles(Size size, Point position)
    {
        var space = SpaceInTiles(size);
        var result = new List<Point>();
        for (int y = 0; y < space; ++y)
        {
            for (int x = 0; x < space; ++x)
            {
                result.Add(new Point(x, y) + position);
            }
        }
        return result;
    }
}