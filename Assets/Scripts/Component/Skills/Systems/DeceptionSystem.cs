public partial class Data
{
    public CoreDictionary<Entity, int> Deception = new CoreDictionary<Entity, int>();
}

public interface IDeceptionSystem : IDependency<IDeceptionSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IDeceptionSystem))]
public class DeceptionSystem : BaseSkillSystem, IDeceptionSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Deception;
    protected override Skill Skill => Skill.Deception;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Charisma;
}

public partial struct Entity
{
    public int Deception
    {
        get { return IDeceptionSystem.Resolve().Get(this); }
        set { IDeceptionSystem.Resolve().Set(this, value); }
    }
}