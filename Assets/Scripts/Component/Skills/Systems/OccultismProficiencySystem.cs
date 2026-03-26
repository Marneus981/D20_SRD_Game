public partial class Data
{
    public CoreDictionary<Entity, Proficiency> OccultismProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IOccultismProficiencySystem : IDependency<IOccultismProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IOccultismProficiencySystem))]
public class OccultismProficiencySystem : EntityTableSystem<Proficiency>, IOccultismProficiencySystem
//No partial definition for Entity: Proficiency of a skill will only be used to calculate the skill value; 
//We will use the system directly when needed.
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.OccultismProficiency;
}