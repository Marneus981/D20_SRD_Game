public partial class Data
{
    public CoreDictionary<Entity, Proficiency> CraftingProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface ICraftingProficiencySystem : IDependency<ICraftingProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class CraftingProficiencySystem : EntityTableSystem<Proficiency>, ICraftingProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.CraftingProficiency;
}