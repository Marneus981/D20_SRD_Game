public class MaterialDamageTypeSystem : IDamageTypeSystem
{
    public bool GetImmunity(DamageInfo info)
    {
        if (string.IsNullOrEmpty(info.material))
            return false;
        var system = IDamageImmunitySystem.Resolve();
        return system.HasImmunity(info.target, info.material);
    }

    public int GetResistance(DamageInfo info)
    {
        if (string.IsNullOrEmpty(info.material))
            return 0;
        var system = IDamageResistanceSystem.Resolve();
        return system.GetResistance(info.target, info.material);
    }

    public int GetWeakness(DamageInfo info)
    {
        if (string.IsNullOrEmpty(info.material))
            return 0;
        var system = IDamageWeaknessSystem.Resolve();
        return system.GetWeakness(info.target, info.material);
    }
}