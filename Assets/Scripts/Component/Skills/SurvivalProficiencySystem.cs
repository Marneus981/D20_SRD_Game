public partial class Data
{
    public CoreDictionary<Entity, Proficiency> SurvivalProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface ISurvivalProficiencySystem : IDependency<ISurvivalProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class SurvivalProficiencySystem : EntityTableSystem<Proficiency>, ISurvivalProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.SurvivalProficiency;
}