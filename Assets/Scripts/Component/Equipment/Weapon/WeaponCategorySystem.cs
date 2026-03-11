[System.Flags]
public enum WeaponCategory
{
    None = 0,
    Advanced = 1 << 0,
    Ammunition = 1 << 1,
    Martial = 1 << 2,
    Simple = 1 << 3,
    Unarmed = 1 << 4
}

public partial class Data
{
    public CoreDictionary<Entity, WeaponCategory> weaponCategory = new CoreDictionary<Entity, WeaponCategory>();
}

public interface IWeaponCategorySystem : IDependency<IWeaponCategorySystem>, IEntityTableSystem<WeaponCategory>
{

}

public class WeaponCategorySystem : EntityTableSystem<WeaponCategory>, IWeaponCategorySystem
{
    public override CoreDictionary<Entity, WeaponCategory> Table => IDataSystem.Resolve().Data.weaponCategory;
}

public partial struct Entity
{
    public WeaponCategory WeaponCategory
    {
        get { return IWeaponCategorySystem.Resolve().Get(this); }
        set { IWeaponCategorySystem.Resolve().Set(this, value); }
    }
}