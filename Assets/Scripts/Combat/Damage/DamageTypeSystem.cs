using System.Collections.Generic;

public class DamageTypeSystem : IDamageTypeSystem
{
    string category;
    HashSet<string> types;

    public DamageTypeSystem(string category, string[] types)
    {
        this.category = category;
        this.types = new HashSet<string>(types);
    }

    public bool GetImmunity(DamageInfo info)
    {
        if (string.Equals(info.type, category) || types.Contains(info.type))
        {
            var system = IDamageImmunitySystem.Resolve();
            return system.HasImmunity(info.target, info.type);
        }
        return false;
    }

    public int GetResistance(DamageInfo info)
    {
        if (string.Equals(info.type, category) || types.Contains(info.type))
        {
            var system = IDamageResistanceSystem.Resolve();
            var excSystem = IDamageResistanceExceptionSystem.Resolve();
            var result = system.GetResistance(info.target, info.type);
            var exception = excSystem.GetException(info.target, info.type);
            return string.Equals(info.material, exception) ? 0 : result;
        }
        return 0;
    }

    public int GetWeakness(DamageInfo info)
    {
        if (string.Equals(info.type, category) || types.Contains(info.type))
        {
            var system = IDamageWeaknessSystem.Resolve();
            return system.GetWeakness(info.target, info.type);
        }
        return 0;
    }
}