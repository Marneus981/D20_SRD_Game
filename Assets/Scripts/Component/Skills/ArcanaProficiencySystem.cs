public partial class Data
{
    public CoreDictionary<Entity, Proficiency> ArcanaProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IArcanaProficiencySystem : IDependency<IArcanaProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class ArcanaProficiencySystem : EntityTableSystem<Proficiency>, IArcanaProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.ArcanaProficiency;
}