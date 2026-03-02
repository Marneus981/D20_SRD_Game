[System.Serializable]
public class DamageResistance
{
    public CoreDictionary<string, int> types = new CoreDictionary<string, int>();
}

public partial class Data
{
    public CoreDictionary<Entity, DamageResistance> damageResistance = new CoreDictionary<Entity, DamageResistance>();
}

public interface IDamageResistanceSystem : IDependency<IDamageResistanceSystem>, IEntityTableSystem<DamageResistance>
{
    int GetResistance(Entity entity, string damageType);
    void SetResistance(Entity entity, string damageType, int amount);
    void RemoveResistance(Entity entity, string damageType);
}

public class DamageResistanceSystem : EntityTableSystem<DamageResistance>, IDamageResistanceSystem
{
    public override CoreDictionary<Entity, DamageResistance> Table => IDataSystem.Resolve().Data.damageResistance;

    public int GetResistance(Entity entity, string damageType)
    {
        DamageResistance damageResistance;
        if (TryGetValue(entity, out damageResistance))
        {
            if (damageResistance.types.ContainsKey(damageType))
                return damageResistance.types[damageType];
        }
        return 0;
    }

    public void SetResistance(Entity entity, string damageType, int amount)
    {
        if (!Has(entity))
            Set(entity, new DamageResistance());

        Get(entity).types[damageType] = amount;
    }

    public void RemoveResistance(Entity entity, string damageType)
    {
        DamageResistance damageResistance;
        if (TryGetValue(entity, out damageResistance))
            damageResistance.types.Remove(damageType);
    }
}