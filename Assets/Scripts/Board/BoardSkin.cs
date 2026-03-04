using UnityEngine;
using UnityEngine.Tilemaps;

public interface IBoardSkin
{
    void Load(Tilemap tilemap, BoardData boardData);
}

[CreateAssetMenu]
public class BoardSkin : ScriptableObject
{
    [SerializeField] TilePalette darkPalette;
    [SerializeField] TilePalette lightPalette;

    public void Load(Tilemap tilemap, BoardData boardData)
    {
        tilemap.ClearAllTiles();
        for (int y = 0; y < boardData.height; ++y)
        {
            for (int x = 0; x < boardData.width; ++x)
            {
                var index = y * boardData.width + x;
                var isEvenColumn = x % 2 == 0;
                var isEvenRow = y % 2 == 0;
                var isLight = isEvenColumn && isEvenRow || !isEvenColumn && !isEvenRow;
                var palette = isLight ? lightPalette : darkPalette;
                var elevation = boardData.tiles[index];

                var tileAbove = elevation;
                if (y + 1 < boardData.height)
                    tileAbove = boardData.tiles[index + boardData.width];
                
                switch (elevation)
                {
                    case 0:
                        if (tileAbove == elevation)
                            tilemap.SetTile(new Vector3Int(x, y, 0), palette.water.Random());
                        else
                            tilemap.SetTile(new Vector3Int(x, y, 0), palette.beach.Random());
                        break;
                    case 1:
                        tilemap.SetTile(new Vector3Int(x, y, 0), palette.ground.Random());
                        break;
                    case 2:
                        tilemap.SetTile(new Vector3Int(x, y, 0), palette.hill.Random());
                        break;
                    case 3:
                        tilemap.SetTile(new Vector3Int(x, y, 0), palette.mountain.Random());
                        break;
                }
            }
        }
    }
}