public partial class Data
{
    public CoreDictionary<Entity, int> Diplomacy = new CoreDictionary<Entity, int>();
}

public interface IDiplomacySystem : IDependency<IDiplomacySystem>, IBaseSkillSystem
{

}

public class DiplomacySystem : BaseSkillSystem, IDiplomacySystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Diplomacy;
    protected override Skill Skill => Skill.Diplomacy;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Charisma;
}

public partial struct Entity
{
    public int Diplomacy
    {
        get { return IDiplomacySystem.Resolve().Get(this); }
        set { IDiplomacySystem.Resolve().Set(this, value); }
    }
}