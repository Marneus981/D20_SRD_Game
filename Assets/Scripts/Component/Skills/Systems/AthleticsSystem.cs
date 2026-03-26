public partial class Data
{
    public CoreDictionary<Entity, int> Athletics = new CoreDictionary<Entity, int>();
}

public interface IAthleticsSystem : IDependency<IAthleticsSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IAthleticsSystem))]
public class AthleticsSystem : BaseSkillSystem, IAthleticsSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Athletics;
    protected override Skill Skill => Skill.Athletics;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Strength;
}

public partial struct Entity
{
    public int Athletics
    {
        get { return IAthleticsSystem.Resolve().Get(this); }
        set { IAthleticsSystem.Resolve().Set(this, value); }
    }
}