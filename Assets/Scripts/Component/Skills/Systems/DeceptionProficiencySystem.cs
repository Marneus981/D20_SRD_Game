public partial class Data
{
    public CoreDictionary<Entity, Proficiency> DeceptionProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IDeceptionProficiencySystem : IDependency<IDeceptionProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IDeceptionProficiencySystem))]
public class DeceptionProficiencySystem : EntityTableSystem<Proficiency>, IDeceptionProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.DeceptionProficiency;
}