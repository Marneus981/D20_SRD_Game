public partial class Data
{
    public CoreDictionary<Entity, int> Thievery = new CoreDictionary<Entity, int>();
}

public interface IThieverySystem : IDependency<IThieverySystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IThieverySystem))]
public class ThieverySystem : BaseSkillSystem, IThieverySystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Thievery;
    protected override Skill Skill => Skill.Thievery;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Dexterity;
}

public partial struct Entity
{
    public int Thievery
    {
        get { return IThieverySystem.Resolve().Get(this); }
        set { IThieverySystem.Resolve().Set(this, value); }
    }
}