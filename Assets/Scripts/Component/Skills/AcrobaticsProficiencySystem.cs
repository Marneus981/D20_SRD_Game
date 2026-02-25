public partial class Data
{
    public CoreDictionary<Entity, Proficiency> AcrobaticsProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IAcrobaticsProficiencySystem : IDependency<IAcrobaticsProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class AcrobaticsProficiencySystem : EntityTableSystem<Proficiency>, IAcrobaticsProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.AcrobaticsProficiency;
}