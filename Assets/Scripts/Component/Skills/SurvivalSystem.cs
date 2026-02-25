public partial class Data
{
    public CoreDictionary<Entity, int> Survival = new CoreDictionary<Entity, int>();
}

public interface ISurvivalSystem : IDependency<ISurvivalSystem>, IBaseSkillSystem
{

}

public class SurvivalSystem : BaseSkillSystem, ISurvivalSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Survival;
    protected override Skill Skill => Skill.Survival;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Wisdom;
}

public partial struct Entity
{
    public int Survival
    {
        get { return ISurvivalSystem.Resolve().Get(this); }
        set { ISurvivalSystem.Resolve().Set(this, value); }
    }
}