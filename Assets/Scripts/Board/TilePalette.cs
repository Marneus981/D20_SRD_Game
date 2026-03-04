using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TilePalette : ScriptableObject
{
    public TileBase[] water;
    public TileBase[] beach;
    public TileBase[] ground;
    public TileBase[] hill;
    public TileBase[] mountain;
}