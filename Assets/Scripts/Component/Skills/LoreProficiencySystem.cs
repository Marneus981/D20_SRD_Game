public partial class Data
{
    public CoreDictionary<Entity, Proficiency> LoreProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface ILoreProficiencySystem : IDependency<ILoreProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class LoreProficiencySystem : EntityTableSystem<Proficiency>, ILoreProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.LoreProficiency;
}