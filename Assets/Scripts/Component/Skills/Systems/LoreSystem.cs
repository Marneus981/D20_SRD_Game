public partial class Data
{
    public CoreDictionary<Entity, int> Lore = new CoreDictionary<Entity, int>();
}

public interface ILoreSystem : IDependency<ILoreSystem>, IBaseSkillSystem
{

}
[Dependency(typeof(ILoreSystem))]
public class LoreSystem : BaseSkillSystem, ILoreSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.Lore;
    protected override Skill Skill => Skill.Lore;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Intelligence;
}

public partial struct Entity
{
    public int Lore
    {
        get { return ILoreSystem.Resolve().Get(this); }
        set { ILoreSystem.Resolve().Set(this, value); }
    }
}