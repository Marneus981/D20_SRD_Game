using UnityEngine;
using UnityEngine.Tilemaps;

public interface IBoardSystem : IDependency<IBoardSystem>
{
    BoardData BoardData { get; }//Handy reference to the loaded "BoardData"
    void Load(IEncounter encounter);//Load a game board based on an "Encounter" asset
    TileBase GetTile(Point point);//Determine what "TileBase" actually appears at a given "Point"
    //Note: TileBase holds more info than BoardData, such as gatherables, etc.
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