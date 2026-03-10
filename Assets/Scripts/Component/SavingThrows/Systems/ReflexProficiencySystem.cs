public partial class Data
{
    public CoreDictionary<Entity, Proficiency> reflexProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IReflexProficiencySystem : IDependency<IReflexProficiencySystem>, IEntityTableSystem<Proficiency>
{

}

public class ReflexProficiencySystem : EntityTableSystem<Proficiency>, IReflexProficiencySystem
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.reflexProficiency;
}