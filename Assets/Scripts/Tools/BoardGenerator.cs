using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] BoardData data;//Reference to actual project asset
    [SerializeField] int width = 6;//WIP set of data
    [SerializeField] int height = 8;//WIP set of data
    [SerializeField] int[] tiles;//WIP set of data
    [SerializeField] TileBase[] tileViews;//Abstract visualization of our types of tiles
    [SerializeField] float[] elevations = new float[] { 0.3f, 0.6f, 0.7f, 1f };
                                                    //water dirt hill mountain
                                                    
    [SerializeField] Vector2 perlinScale = new Vector2(0.1f, 0.1f);//Perlin noise
    [SerializeField] Vector2 perlinOffset = Vector2.zero;//Perlin noise
    [SerializeField] Tilemap tilemap;
    [SerializeField] Transform marker;//cursor object transform
    [SerializeField] Point markerPosition;//to indicate where the marker should appear over the tile map

    public void Clear()//wipe board
    {
        tiles = null;
        tilemap.ClearAllTiles();
    }
    public void Generate()//Initialize board; small  non-scrollable
    {
        Clear();
        tiles = new int[width * height];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileViews[0]);
            }
        }
    }
    public void GeneratePerlin()
    {
        Clear();
        tiles = new int[width * height];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                var xPos = x * perlinScale.x + perlinOffset.x;
                var yPos = y * perlinScale.y + perlinOffset.y;
                var elevation = Mathf.PerlinNoise(xPos, yPos);
                var tileIndex = IndexForElevation(elevation);
                var tileView = tileViews[tileIndex];
                tilemap.SetTile(new Vector3Int(x, y, 0), tileView);
                tiles[y * width + x] = tileIndex;
                //Note: Index of the tile view, is also assigned as the value for our tiles array
            }
        }
    }
    public void Grow()
    {
        var index = markerPosition.y * width + markerPosition.x;
        tiles[index] = Mathf.Min(tiles[index] + 1, elevations.Length - 1);
        RefreshTile(index);
    }

    public void Shrink()
    {
        var index = markerPosition.y * width + markerPosition.x;
        tiles[index] = Mathf.Max(tiles[index] - 1, 0);
        RefreshTile(index);
    }
    public void MoveMarker(Point offset)
    {
        markerPosition += offset;
        UpdateMarker();
    }
    public void SnapMarker()//change view then match the model
    {
        markerPosition = new Point
        {
            x = Mathf.RoundToInt(marker.transform.position.x),
            y = Mathf.RoundToInt(marker.transform.position.y)
        };
        UpdateMarker();
    }

    public void UpdateMarker()//change model then match the view
    {
        marker.position = markerPosition;
    }
    public void Save()//Copy our local data to whatever BoardData asset has been assigned in the inspector
    {
        if (data == null)
        {
            Debug.LogError("Missing board data - must assign first");
            return;
        }

        Undo.RecordObject(data, "Saved Board");
        data.width = width;
        data.height = height;
        data.tiles = new int[tiles.Length];
        Array.Copy(tiles, data.tiles, tiles.Length);

        EditorUtility.SetDirty(data);
    }

    public void Load()//Copy from the asset to our local data
    {
        Clear();

        if (data == null)
        {
            Debug.LogError("Missing board data - must assign first");
            return;
        }

        width = data.width;
        height = data.height;
        tiles = new int[data.tiles.Length];
        Array.Copy(data.tiles, tiles, data.tiles.Length);
            
        RefreshBoard();//Called to make sure tiles show appropiate elevation
    }
    void RefreshBoard()//Make sure tiles show appropiate elevation
    {
        for (int i = 0; i < tiles.Length; ++i)
            RefreshTile(i);
    }

    void RefreshTile(int index)
    {
        var x = index % width;
        var y = index / width;
        var tileView = tileViews[tiles[index]];
        tilemap.SetTile(new Vector3Int(x, y, 0), tileView);
    }
    int IndexForElevation(float value)
    {
        for (int index = 0; index < elevations.Length; ++index)
        {
            if (value < elevations[index])
            {
                return index;
            }
        }
        return elevations.Length - 1;
    }
}