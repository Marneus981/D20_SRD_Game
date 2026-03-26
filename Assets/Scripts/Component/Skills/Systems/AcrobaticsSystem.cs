public partial class Data
{
    public CoreDictionary<Entity, int> Acrobatics = new CoreDictionary<Entity, int>();
}

public interface IAcrobaticsSystem : IDependency<IAcrobaticsSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IAcrobaticsSystem))]
public class AcrobaticsSystem : BaseSkillSystem, IAcrobaticsSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Acrobatics;
    protected override Skill Skill => Skill.Acrobatics;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Dexterity;
}

public partial struct Entity
{
    public int Acrobatics
    {
        get { return IAcrobaticsSystem.Resolve().Get(this); }
        set { IAcrobaticsSystem.Resolve().Set(this, value); }
    }
}