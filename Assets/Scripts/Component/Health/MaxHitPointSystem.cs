public partial class Data
{
    public CoreDictionary<Entity, int> maxHitPoints = new CoreDictionary<Entity, int>();
}

public interface IMaxHitPointSystem : IDependency<IMaxHitPointSystem>, IEntityTableSystem<int>
{

}

public class MaxHitPointSystem : EntityTableSystem<int>, IMaxHitPointSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.maxHitPoints;
}

public partial struct Entity
{
    public int MaxHitPoints
    {
        get { return IMaxHitPointSystem.Resolve().Get(this); }
        set { IMaxHitPointSystem.Resolve().Set(this, value); }
    }
}