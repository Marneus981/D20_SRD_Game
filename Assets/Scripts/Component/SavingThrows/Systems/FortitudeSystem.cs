public partial class Data
{
    public CoreDictionary<Entity, int> fortitude = new CoreDictionary<Entity, int>();
}

public interface IFortitudeSystem : IDependency<IFortitudeSystem>, IBaseSavingThrowSystem
{

}
[Dependency(typeof(IFortitudeSystem))]
public class FortitudeSystem : BaseSavingThrowSystem, IFortitudeSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.fortitude;
    protected override SavingThrow SavingThrow => SavingThrow.Fortitude;
    protected override AbilityScore.Attribute Attribute => AbilityScore.Attribute.Constitution;
}

public partial struct Entity
{
    public int Fortitude
    {
        get { return IFortitudeSystem.Resolve().Get(this); }
        set { IFortitudeSystem.Resolve().Set(this, value); }
    }
}