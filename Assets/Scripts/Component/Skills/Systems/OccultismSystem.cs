public partial class Data
{
    public CoreDictionary<Entity, int> Occultism = new CoreDictionary<Entity, int>();
}

public interface IOccultismSystem : IDependency<IOccultismSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IOccultismSystem))]
public class OccultismSystem : BaseSkillSystem, IOccultismSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Occultism;
    protected override Skill Skill => Skill.Occultism;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Intelligence;
}

public partial struct Entity
{
    public int Occultism
    {
        get { return IOccultismSystem.Resolve().Get(this); }
        set { IOccultismSystem.Resolve().Set(this, value); }
    }
}