using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetManager<T> : IDependency<IAssetManager<T>>
/*
Generic asset manager:
    You can create conforming classes that specify the generic type (GameObject, 
    Texture2D, etc.) Whatever it is you want to load. 
*/
{
    UniTask<T> InstantiateAsync(string key); //Creates an instance of an asset in the scene.
    UniTask<T> LoadAssetAsync(string key);//Loads a reference to an asset in memory, 
                                            //but doesn’t create an instance.
                                            //We will use this for things like our “Entry” assets, where we only need them for their data.

}
public abstract class AssetManager<T> : MonoBehaviour, IAssetManager<T> where T : Object
{
    //As we load assets, we store the “handle” in a collection so that we can release them later.
    Dictionary<string, AsyncOperationHandle<T>> assetMap = new Dictionary<string, AsyncOperationHandle<T>>();
    public async UniTask<T> InstantiateAsync(string key)
    //We can Instantiate via these 2 actions bc we put a constraint 
    //on our generic class that the generic type will be a type of Object.
    {
        var asset = await LoadAssetAsync(key);
        return Instantiate(asset);
    }
    public async UniTask<T> LoadAssetAsync(string key)
    //Lazy Load:
    {
        AsyncOperationHandle<T> handle;
        if (assetMap.ContainsKey(key)) 
        //First looks in the collection to see if it has already loaded (or is loading) an asset by the specified “key”.
        {
            handle = assetMap[key];
        }
        else
        //If the “key” is new, then it will obtain a new “handle” and store it in the collection.
        {
            handle = Addressables.LoadAssetAsync<T>(key);
            assetMap[key] = handle;
        }
        //After resolving the handle to use:
        //Manager will simply “await” the handle which gives the asset time to load. 
        if (!handle.IsDone)
            await handle;

        //Assuming the status shows that the asset was loaded successfully, 
        //then we return the “result” which is the asset itself.
        if (handle.Status == AsyncOperationStatus.Succeeded)
            return handle.Result;

        return null;
    }
    //MonoBehaviour:
        //Using OnEnable to register and OnDisable to clear our reference.
    private void OnEnable()
    {
        IAssetManager<T>.Register(this);
    }

    private void OnDisable()
    {
        IAssetManager<T>.Reset();
    }
    private void OnDestroy()
    //Handle memory management for the addressable handles:
        //We just loop over the collection’s values, and call Release on each one.
    {
        foreach (var handle in assetMap.Values)
            Addressables.Release(handle);
    }
}
