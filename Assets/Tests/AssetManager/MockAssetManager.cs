using Cysharp.Threading.Tasks;

public class MockAssetManager<T> : IAssetManager<T>
{
    public T fakeAsset;

    public async UniTask<T> InstantiateAsync(string key)
    {
        await UniTask.CompletedTask;
        return fakeAsset;
    }

    public async UniTask<T> LoadAssetAsync(string key)
    {
        await UniTask.CompletedTask;
        return fakeAsset;
    }
}