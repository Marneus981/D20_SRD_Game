public partial class Data
{
    public CoreDictionary<Entity, string> combatantAsset = new CoreDictionary<Entity, string>();
}

public interface ICombatantAssetSystem : IDependency<ICombatantAssetSystem>, IEntityTableSystem<string>
{

}

public class CombatantAssetSystem : EntityTableSystem<string>, ICombatantAssetSystem
{
    public override CoreDictionary<Entity, string> Table => IDataSystem.Resolve().Data.combatantAsset;
}

public partial struct Entity
{
    public string CombatantAsset
    {
        get { return ICombatantAssetSystem.Resolve().Get(this); }
        set { ICombatantAssetSystem.Resolve().Set(this, value); }
    }
}