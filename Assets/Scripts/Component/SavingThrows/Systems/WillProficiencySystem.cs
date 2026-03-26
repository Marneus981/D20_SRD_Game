public partial class Data
{
    public CoreDictionary<Entity, Proficiency> willProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IWillProficiencySystem : IDependency<IWillProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IWillProficiencySystem))]
public class WillProficiencySystem : EntityTableSystem<Proficiency>, IWillProficiencySystem
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.willProficiency;
}