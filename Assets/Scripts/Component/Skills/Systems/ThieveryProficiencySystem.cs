public partial class Data
{
    public CoreDictionary<Entity, Proficiency> ThieveryProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IThieveryProficiencySystem : IDependency<IThieveryProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IThieveryProficiencySystem))]
public class ThieveryProficiencySystem : EntityTableSystem<Proficiency>, IThieveryProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.ThieveryProficiency;
}