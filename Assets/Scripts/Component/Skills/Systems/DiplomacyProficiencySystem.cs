public partial class Data
{
    public CoreDictionary<Entity, Proficiency> DiplomacyProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IDiplomacyProficiencySystem : IDependency<IDiplomacyProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class DiplomacyProficiencySystem : EntityTableSystem<Proficiency>, IDiplomacyProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.DiplomacyProficiency;
}