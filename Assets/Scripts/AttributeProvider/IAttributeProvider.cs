using Cysharp.Threading.Tasks;

public interface IAttributeProvider
{
    //void Setup(Entity entity); //Changed to make room for possible weapon asset loading via IAttributeProvider
    void Setup(Entity entity)//In case we use an async approach
    {
        throw new System.NotImplementedException();//NotImplementedException: The method or operation is not implemented.
    }
    async UniTask SetupFlow(Entity entity)
    {
        Setup(entity);
        await UniTask.CompletedTask;
    }
}