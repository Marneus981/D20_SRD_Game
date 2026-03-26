[System.Serializable]
public class DamageImmunity
{
    public CoreSet<string> types = new CoreSet<string>();
}

public partial class Data
{
    public CoreDictionary<Entity, DamageImmunity> damageImmunity = new CoreDictionary<Entity, DamageImmunity>();
}

public interface IDamageImmunitySystem : IDependency<IDamageImmunitySystem>, IEntityTableSystem<DamageImmunity>
{
    bool HasImmunity(Entity entity, string damageType);
    void AddImmunity(Entity entity, string damageType);
    void RemoveImmunity(Entity entity, string damageType);
}

[Dependency(typeof(IDamageImmunitySystem))]
public class DamageImmunitySystem : EntityTableSystem<DamageImmunity>, IDamageImmunitySystem
{
    public override CoreDictionary<Entity, DamageImmunity> Table => IDataSystem.Resolve().Data.damageImmunity;

    public bool HasImmunity(Entity entity, string damageType)
    {
        DamageImmunity damageImmunity;
        if (TryGetValue(entity, out damageImmunity))
            return damageImmunity.types.Contains(damageType);
        return false;
    }

    public void AddImmunity(Entity entity, string damageType)
    {
        if (!Has(entity))
            Set(entity, new DamageImmunity());

        Get(entity).types.Add(damageType);
    }

    public void RemoveImmunity(Entity entity, string damageType)
    {
        DamageImmunity damageImmunity;
        if (TryGetValue(entity, out damageImmunity))
            damageImmunity.types.Remove(damageType);
    }
}