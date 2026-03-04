using UnityEngine;
using UnityEngine.Tilemaps;

public interface IBoardSystem : IDependency<IBoardSystem>
{
    BoardData BoardData { get; }//Handy reference to the loaded "BoardData"
    void Load(IEncounter encounter);//Load a game board based on an "Encounter" asset
    TileBase GetTile(Point point);//Determine what "TileBase" actually appears at a given "Point"
    //Note: TileBase holds more info than BoardData, such as gatherables, etc.
    bool IsPointOnBoard(Point point);
    int GetTileType(Point point);
}
public class BoardSystem : MonoBehaviour, IBoardSystem
{
    public BoardData BoardData { get; private set; }
    Tilemap tilemap;

    public void Load(IEncounter encounter)
    {
        BoardData = encounter.BoardData;
        encounter.BoardSkin.Load(tilemap, BoardData);
    }

    public TileBase GetTile(Point point)
    {
        return tilemap.GetTile(new Vector3Int(point.x, point.y, 0));
    }
    public bool IsPointOnBoard(Point point)
    {
        return point.x >= 0 && point.y >= 0 &&
            point.x < BoardData.width && point.y < BoardData.height;
    }

    public int GetTileType(Point point)
    {
        var index = point.y * BoardData.width + point.x;
        return BoardData.tiles[index];
    }
    private void OnEnable()
    {
        tilemap = GetComponent<Tilemap>();
        IBoardSystem.Register(this);
    }

    private void OnDisable()
    {
        IBoardSystem.Reset();
    }
}