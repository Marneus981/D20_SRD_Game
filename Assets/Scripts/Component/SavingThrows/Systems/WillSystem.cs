public partial class Data
{
    public CoreDictionary<Entity, int> will = new CoreDictionary<Entity, int>();
}

public interface IWillSystem : IDependency<IWillSystem>, IBaseSavingThrowSystem
{

}
[Dependency(typeof(IWillSystem))]
public class WillSystem : BaseSavingThrowSystem, IWillSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.will;
    protected override SavingThrow SavingThrow => SavingThrow.Will;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Wisdom;
}

public partial struct Entity
{
    public int Will
    {
        get { return IWillSystem.Resolve().Get(this); }
        set { IWillSystem.Resolve().Set(this, value); }
    }
}