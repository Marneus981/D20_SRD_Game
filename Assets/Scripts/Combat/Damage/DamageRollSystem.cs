public partial class Data
{
    public CoreDictionary<Entity, DiceRoll> damageRoll = new CoreDictionary<Entity, DiceRoll>();
}

public interface IDamageRollSystem : IDependency<IDamageRollSystem>, IEntityTableSystem<DiceRoll>
{

}
[Dependency(typeof(IDamageRollSystem))]
public class DamageRollSystem : EntityTableSystem<DiceRoll>, IDamageRollSystem
{
    public override CoreDictionary<Entity, DiceRoll> Table => IDataSystem.Resolve().Data.damageRoll;
}

public partial struct Entity
{
    public DiceRoll DamageRoll
    {
        get { return IDamageRollSystem.Resolve().Get(this); }
        set { IDamageRollSystem.Resolve().Set(this, value); }
    }
}