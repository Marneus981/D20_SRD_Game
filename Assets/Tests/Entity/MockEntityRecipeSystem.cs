using Cysharp.Threading.Tasks;

public class MockEntityRecipeSystem : IEntityRecipeSystem
{
    public Entity fakeEntity;

    public async UniTask<Entity> Create(string assetName)
    {
        await UniTask.CompletedTask;
        return fakeEntity;
    }
}