using UnityEngine;

public class GameObjectAssetManager : AssetManager<GameObject>
{
    /*
    Parent class AssetManager already handles everything necessary, 
    but in order to assign the script to an object in a scene, 
    we needed a subclass to specify the generic type.
    */
}