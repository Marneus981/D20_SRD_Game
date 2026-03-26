public partial class Data
{
    public CoreDictionary<Entity, Proficiency> NatureProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface INatureProficiencySystem : IDependency<INatureProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(INatureProficiencySystem))]
public class NatureProficiencySystem : EntityTableSystem<Proficiency>, INatureProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.NatureProficiency;
}