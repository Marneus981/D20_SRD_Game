using Cysharp.Threading.Tasks;

public partial class Data
{
    public CoreDictionary<Entity, int> dying = new CoreDictionary<Entity, int>();
}
public interface IDyingSystem : IDependency<IDyingSystem>, IEntityTableSystem<int>
{
    UniTask Die(Entity entity);
    UniTask Revive(Entity entity);
}
[Dependency(typeof(IDyingSystem))]
public class DyingSystem : EntityTableSystem<int>, IDyingSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.dying;

    public async UniTask Die(Entity entity)
    {
        Set(entity, 1);
        await PresentDyingState(entity);
    }

    public async UniTask Revive(Entity entity)
    {
        Remove(entity);
        await PresentDyingState(entity);
    }

    async UniTask PresentDyingState(Entity entity)
    {
        var dyingInfo = new DyingPresentationInfo
        {
            entity = entity,
            value = entity.Dying >= 1
        };
        await IDyingPresenter.Resolve().Present(dyingInfo);
    }
}

public partial struct Entity
{
    public int Dying
    {
        get { return IDyingSystem.Resolve().Get(this); }
        set { IDyingSystem.Resolve().Set(this, value); }
    }
}