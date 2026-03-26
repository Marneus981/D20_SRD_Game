public partial class Data
{
    public CoreDictionary<Entity, int> Religion = new CoreDictionary<Entity, int>();
}

public interface IReligionSystem : IDependency<IReligionSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IReligionSystem))]
public class ReligionSystem : BaseSkillSystem, IReligionSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Religion;
    protected override Skill Skill => Skill.Religion;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Wisdom;
}

public partial struct Entity
{
    public int Religion
    {
        get { return IReligionSystem.Resolve().Get(this); }
        set { IReligionSystem.Resolve().Set(this, value); }
    }
}