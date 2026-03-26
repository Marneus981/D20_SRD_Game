public partial class Data
{
    public CoreDictionary<Entity, int> Arcana = new CoreDictionary<Entity, int>();
}

public interface IArcanaSystem : IDependency<IArcanaSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IArcanaSystem))]
public class ArcanaSystem : BaseSkillSystem, IArcanaSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Arcana;
    protected override Skill Skill => Skill.Arcana;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Intelligence;
}

public partial struct Entity
{
    public int Arcana
    {
        get { return IArcanaSystem.Resolve().Get(this); }
        set { IArcanaSystem.Resolve().Set(this, value); }
    }
}