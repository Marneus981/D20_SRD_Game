public partial class Data
{
    public CoreDictionary<Entity, int> Nature = new CoreDictionary<Entity, int>();
}

public interface INatureSystem : IDependency<INatureSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(INatureSystem))]
public class NatureSystem : BaseSkillSystem, INatureSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Nature;
    protected override Skill Skill => Skill.Nature;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Wisdom;
}

public partial struct Entity
{
    public int Nature
    {
        get { return INatureSystem.Resolve().Get(this); }
        set { INatureSystem.Resolve().Set(this, value); }
    }
}