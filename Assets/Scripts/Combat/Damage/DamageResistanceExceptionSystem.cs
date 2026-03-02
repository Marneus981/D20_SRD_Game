[System.Serializable]
public class DamageResistanceException
{
    public CoreDictionary<string, string> types = new CoreDictionary<string, string>();
}

public partial class Data
{
    public CoreDictionary<Entity, DamageResistanceException> damageResistanceException = new CoreDictionary<Entity, DamageResistanceException>();
}

public interface IDamageResistanceExceptionSystem : IDependency<IDamageResistanceExceptionSystem>, IEntityTableSystem<DamageResistanceException>
{
    string GetException(Entity entity, string damageType);
    void SetException(Entity entity, string damageType, string exception);
    void RemoveException(Entity entity, string damageType);
}

public class DamageResistanceExceptionSystem : EntityTableSystem<DamageResistanceException>, IDamageResistanceExceptionSystem
{
    public override CoreDictionary<Entity, DamageResistanceException> Table => IDataSystem.Resolve().Data.damageResistanceException;

    public string GetException(Entity entity, string damageType)
    {
        DamageResistanceException value;
        if (TryGetValue(entity, out value))
        {
            if (value.types.ContainsKey(damageType))
                return value.types[damageType];
        }
        return null;
    }

    public void SetException(Entity entity, string damageType, string exception)
    {
        if (!Has(entity))
            Set(entity, new DamageResistanceException());

        Get(entity).types[damageType] = exception;
    }

    public void RemoveException(Entity entity, string damageType)
    {
        DamageResistanceException value;
        if (TryGetValue(entity, out value))
            value.types.Remove(damageType);
    }
}