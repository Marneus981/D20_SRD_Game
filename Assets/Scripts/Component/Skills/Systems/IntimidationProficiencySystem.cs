public partial class Data
{
    public CoreDictionary<Entity, Proficiency> IntimidationProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IIntimidationProficiencySystem : IDependency<IIntimidationProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class IntimidationProficiencySystem : EntityTableSystem<Proficiency>, IIntimidationProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.IntimidationProficiency;
}