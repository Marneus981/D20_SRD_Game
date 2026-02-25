public partial class Data
{
    public CoreDictionary<Entity, Proficiency> MedicineProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IMedicineProficiencySystem : IDependency<IMedicineProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class MedicineProficiencySystem : EntityTableSystem<Proficiency>, IMedicineProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.MedicineProficiency;
}