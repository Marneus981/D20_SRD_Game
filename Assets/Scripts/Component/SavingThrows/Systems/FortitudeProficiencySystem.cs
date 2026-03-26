public partial class Data
{
    public CoreDictionary<Entity, Proficiency> fortitudeProficiency = new CoreDictionary<Entity, Proficiency>();
}

public interface IFortitudeProficiencySystem : IDependency<IFortitudeProficiencySystem>, IEntityTableSystem<Proficiency>
{

}
[Dependency(typeof(IFortitudeProficiencySystem))]
public class FortitudeProficiencySystem : EntityTableSystem<Proficiency>, IFortitudeProficiencySystem
{
    public override CoreDictionary<Entity, Proficiency> Table => IDataSystem.Resolve().Data.fortitudeProficiency;
}