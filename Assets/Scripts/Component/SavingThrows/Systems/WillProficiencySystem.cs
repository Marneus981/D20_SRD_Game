public partial class Data
{
    public CoreDictionary<Entity, Proficiency> willProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IWillProficiencySystem : IDependency<IWillProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class WillProficiencySystem : EntityTableSystem<Proficiency>, IWillProficiencySystem
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.willProficiency;
}