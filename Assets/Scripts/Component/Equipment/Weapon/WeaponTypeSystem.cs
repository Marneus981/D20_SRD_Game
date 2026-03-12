public enum WeaponType
{
    Melee,
    Ranged
}

public partial class Data
{
    public CoreDictionary<Entity, WeaponType> weaponType = new CoreDictionary<Entity, WeaponType>();
}

public interface IWeaponTypeSystem : IDependency<IWeaponTypeSystem>, IEntityTableSystem<WeaponType>
{

}

public class WeaponTypeSystem : EntityTableSystem<WeaponType>, IWeaponTypeSystem
{
    public override CoreDictionary<Entity, WeaponType> Table => IDataSystem.Resolve().Data.weaponType;
}

public partial struct Entity
{
    public WeaponType WeaponType
    {
        get { return IWeaponTypeSystem.Resolve().Get(this); }
        set { IWeaponTypeSystem.Resolve().Set(this, value); }
    }
}