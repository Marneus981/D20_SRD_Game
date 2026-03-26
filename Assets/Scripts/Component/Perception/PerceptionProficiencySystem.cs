public partial class Data
{
    public CoreDictionary<Entity, Proficiency> perceptionProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IPerceptionProficiencySystem : IDependency<IPerceptionProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IPerceptionProficiencySystem))]
public class PerceptionProficiencySystem : EntityTableSystem<Proficiency>, IPerceptionProficiencySystem
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.perceptionProficiency;
}