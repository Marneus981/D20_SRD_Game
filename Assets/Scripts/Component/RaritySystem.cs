public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Unique
}

public partial class Data
{
    public CoreDictionary<Entity, Rarity> rarity = new CoreDictionary<Entity, Rarity>();
}

public interface IRaritySystem : IDependency<IRaritySystem>, IEntityTableSystem<Rarity>
{

}

[Dependency(typeof(IRaritySystem))]
public class RaritySystem : EntityTableSystem<Rarity>, IRaritySystem
{
    public override CoreDictionary<Entity, Rarity> Table => IDataSystem.Resolve().Data.rarity;
}

public partial struct Entity
{
    public Rarity Rarity
    {
        get { return IRaritySystem.Resolve().Get(this); }
        set { IRaritySystem.Resolve().Set(this, value); }
    }
}