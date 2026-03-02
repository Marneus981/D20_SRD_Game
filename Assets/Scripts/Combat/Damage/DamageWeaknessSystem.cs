[System.Serializable]
public class DamageWeakness
{
    public CoreDictionary<string, int> types = new CoreDictionary<string, int>();
}

public partial class Data
{
    public CoreDictionary<Entity, DamageWeakness> damageWeakness = new CoreDictionary<Entity, DamageWeakness>();
}

public interface IDamageWeaknessSystem : IDependency<IDamageWeaknessSystem>, IEntityTableSystem<DamageWeakness>
{
    int GetWeakness(Entity entity, string damageType);
    void SetWeakness(Entity entity, string damageType, int amount);
    void RemoveWeakness(Entity entity, string damageType);
}

public class DamageWeaknessSystem : EntityTableSystem<DamageWeakness>, IDamageWeaknessSystem
{
    public override CoreDictionary<Entity, DamageWeakness> Table => IDataSystem.Resolve().Data.damageWeakness;

    public int GetWeakness(Entity entity, string damageType)
    {
        DamageWeakness damageWeakness;
        if (TryGetValue(entity, out damageWeakness))
        {
            if (damageWeakness.types.ContainsKey(damageType))
                return damageWeakness.types[damageType];
        }
        return 0;
    }

    public void SetWeakness(Entity entity, string damageType, int amount)
    {
        if (!Has(entity))
            Set(entity, new DamageWeakness());

        Get(entity).types[damageType] = amount;
    }

    public void RemoveWeakness(Entity entity, string damageType)
    {
        DamageWeakness damageWeakness;
        if (TryGetValue(entity, out damageWeakness))
            damageWeakness.types.Remove(damageType);
    }
}