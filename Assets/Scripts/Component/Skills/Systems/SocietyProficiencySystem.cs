public partial class Data
{
    public CoreDictionary<Entity, Proficiency> SocietyProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface ISocietyProficiencySystem : IDependency<ISocietyProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(ISocietyProficiencySystem))]
public class SocietyProficiencySystem : EntityTableSystem<Proficiency>, ISocietyProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.SocietyProficiency;
}