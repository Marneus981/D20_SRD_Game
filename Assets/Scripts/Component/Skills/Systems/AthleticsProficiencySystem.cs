public partial class Data
{
    public CoreDictionary<Entity, Proficiency> AthleticsProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IAthleticsProficiencySystem : IDependency<IAthleticsProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IAthleticsProficiencySystem))]
public class AthleticsProficiencySystem : EntityTableSystem<Proficiency>, IAthleticsProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.AthleticsProficiency;
}