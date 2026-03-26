public partial class Data
{
    public CoreDictionary<Entity, int> perception = new CoreDictionary<Entity, int>();
}

public interface IPerceptionSystem : IDependency<IPerceptionSystem>, IEntityTableSystem<int>
{
    void Setup(Entity entity);
}
[Dependency(typeof(IPerceptionSystem))]
public class PerceptionSystem : EntityTableSystem<int>, IPerceptionSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.perception;

    public void Setup(Entity entity)
    {
        Table[entity] = Calculate(entity);
    }

    int Calculate(Entity entity)
    {
        int result = entity[AbilityScore.Attribute.Wisdom].Modifier;
        var proficiency = IPerceptionProficiencySystem.Resolve().Get(entity);
        if (proficiency != Proficiency.Untrained)
            result += (int)proficiency * 2 + entity.Level;
        UnityEngine.Debug.Log(string.Format("Proficiency: {0}", result));
        return result;
    }
}

public partial struct Entity
{
    public int Perception
    {
        get { return IPerceptionSystem.Resolve().Get(this); }
        set { IPerceptionSystem.Resolve().Set(this, value); }
    }
}