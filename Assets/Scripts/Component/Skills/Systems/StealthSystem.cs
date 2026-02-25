public partial class Data
{
    public CoreDictionary<Entity, int> Stealth = new CoreDictionary<Entity, int>();
}

public interface IStealthSystem : IDependency<IStealthSystem>, IBaseSkillSystem
{

}

public class StealthSystem : BaseSkillSystem, IStealthSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Stealth;
    protected override Skill Skill => Skill.Stealth;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Dexterity;
}

public partial struct Entity
{
    public int Stealth
    {
        get { return IStealthSystem.Resolve().Get(this); }
        set { IStealthSystem.Resolve().Set(this, value); }
    }
}