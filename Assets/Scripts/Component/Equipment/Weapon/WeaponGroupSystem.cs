[System.Flags]
public enum WeaponGroup
{
    None = 0,
    Axe = 1 << 0,
    Bomb = 1 << 1,
    Bow = 1 << 2,
    Brawling = 1 << 3,
    Club = 1 << 4,
    Crossbow = 1 << 5,
    Dart = 1 << 6,
    Firearm = 1 << 7,
    Flail = 1 << 8,
    Hammer = 1 << 9,
    Knife = 1 << 10,
    Pick = 1 << 11,
    Polearm = 1 << 12,
    Shield = 1 << 13,
    Sling = 1 << 14,
    Spear = 1 << 15,
    Sword = 1 << 16
}

public partial class Data
{
    public CoreDictionary<Entity, WeaponGroup> weaponGroup = new CoreDictionary<Entity, WeaponGroup>();
}

public interface IWeaponGroupSystem : IDependency<IWeaponGroupSystem>, IEntityTableSystem<WeaponGroup>
{

}

public class WeaponGroupSystem : EntityTableSystem<WeaponGroup>, IWeaponGroupSystem
{
    public override CoreDictionary<Entity, WeaponGroup> Table => IDataSystem.Resolve().Data.weaponGroup;
}

public partial struct Entity
{
    public WeaponGroup WeaponGroup
    {
        get { return IWeaponGroupSystem.Resolve().Get(this); }
        set { IWeaponGroupSystem.Resolve().Set(this, value); }
    }
}