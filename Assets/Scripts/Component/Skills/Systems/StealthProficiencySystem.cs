public partial class Data
{
    public CoreDictionary<Entity, Proficiency> StealthProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IStealthProficiencySystem : IDependency<IStealthProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class StealthProficiencySystem : EntityTableSystem<Proficiency>, IStealthProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.StealthProficiency;
}