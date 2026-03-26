public partial class Data
{
    public CoreDictionary<Entity, int> Intimidation = new CoreDictionary<Entity, int>();
}

public interface IIntimidationSystem : IDependency<IIntimidationSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(IIntimidationSystem))]
public class IntimidationSystem : BaseSkillSystem, IIntimidationSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Intimidation;
    protected override Skill Skill => Skill.Intimidation;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Charisma;
}

public partial struct Entity
{
    public int Intimidation
    {
        get { return IIntimidationSystem.Resolve().Get(this); }
        set { IIntimidationSystem.Resolve().Set(this, value); }
    }
}