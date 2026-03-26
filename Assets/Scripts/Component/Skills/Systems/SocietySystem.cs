public partial class Data
{
    public CoreDictionary<Entity, int> Society = new CoreDictionary<Entity, int>();
}

public interface ISocietySystem : IDependency<ISocietySystem>, IBaseSkillSystem
{

}
[Dependency(typeof(ISocietySystem))]
public class SocietySystem : BaseSkillSystem, ISocietySystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Society;
    protected override Skill Skill => Skill.Society;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Intelligence;
}

public partial struct Entity
{
    public int Society
    {
        get { return ISocietySystem.Resolve().Get(this); }
        set { ISocietySystem.Resolve().Set(this, value); }
    }
}