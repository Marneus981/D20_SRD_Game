public partial class Data
{
    public CoreDictionary<Entity, int> reflex = new CoreDictionary<Entity, int>();
}

public interface IReflexSystem : IDependency<IReflexSystem>, IBaseSavingThrowSystem
{

}

public class ReflexSystem : BaseSavingThrowSystem, IReflexSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.reflex;
    protected override SavingThrow SavingThrow => SavingThrow.Reflex;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Dexterity;
}

public partial struct Entity
{
    public int Reflex
    {
        get { return IReflexSystem.Resolve().Get(this); }
        set { IReflexSystem.Resolve().Set(this, value); }
    }
}