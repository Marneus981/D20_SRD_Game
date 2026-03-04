using UnityEngine;

[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    public int width;//Dimension x
    public int height;//Dimension y
    public int[] tiles;//Tile array: 0 sea level, 1 ground level, etc...
    /*
    Note: Unity dnk how to serialize 2D arrays int[,] jagged arrays int[][]
    Note: CreateAssetMenu + ScriptableObject enable us to create assets of this
    type from Unity's menu bar
    */
}