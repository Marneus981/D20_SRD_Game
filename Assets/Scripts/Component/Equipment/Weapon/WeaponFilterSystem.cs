[System.Serializable]
public class WeaponFilter
{
    public string name;
    public WeaponCategory category;
    public WeaponGroup group;
}

public interface IWeaponFilterSystem : IDependency<IWeaponFilterSystem>
{
    public bool Matches(WeaponFilter filter, Entity target);
}

public class WeaponFilterSystem : IWeaponFilterSystem
{
    public bool Matches(WeaponFilter filter, Entity target)
    {
        if (!string.IsNullOrEmpty(filter.name) && !string.Equals(filter.name, target.Name))
            return false;
        if (filter.category != WeaponCategory.None)
        {
            var check = filter.category & target.WeaponCategory;
            if (check == WeaponCategory.None)
                return false;
        }
        if (filter.group != WeaponGroup.None)
        {
            var check = filter.group & target.WeaponGroup;
            if (check == WeaponGroup.None)
                return false;
        }
        return true;
    }
}