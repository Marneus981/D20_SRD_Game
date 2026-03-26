public partial class Data
{
    public CoreDictionary<Entity, Proficiency> ReligionProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IReligionProficiencySystem : IDependency<IReligionProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IReligionProficiencySystem))]
public class ReligionProficiencySystem : EntityTableSystem<Proficiency>, IReligionProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.ReligionProficiency;
}