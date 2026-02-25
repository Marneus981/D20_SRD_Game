public partial class Data
{
    public CoreDictionary<Entity, Proficiency> PerformanceProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IPerformanceProficiencySystem : IDependency<IPerformanceProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class PerformanceProficiencySystem : EntityTableSystem<Proficiency>, IPerformanceProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.PerformanceProficiency;
}