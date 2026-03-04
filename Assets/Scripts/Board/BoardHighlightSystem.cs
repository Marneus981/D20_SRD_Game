using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IBoardHighlightSystem : IDependency<IBoardHighlightSystem>
{
    void Highlight(List<Point> points, Color color);
    void ClearHighlights();
}

public class BoardHighlightSystem : MonoBehaviour, IBoardHighlightSystem
{
    [SerializeField] TileBase highlight;
    Tilemap tilemap;

    public void Highlight(List<Point> points, Color color)
    {
        ClearHighlights();
        foreach (Point point in points)
            tilemap.SetTile(new Vector3Int(point.x, point.y, 0), highlight);
        tilemap.color = color;
    }

    public void ClearHighlights()
    {
        tilemap.ClearAllTiles();
        tilemap.color = Color.white;
    }

    private void OnEnable()
    {
        tilemap = GetComponent<Tilemap>();//Grab reference to Tilemap where the highlight tiles will be on
        IBoardHighlightSystem.Register(this);
    }

    private void OnDisable()
    {
        IBoardHighlightSystem.Reset();
    }
}