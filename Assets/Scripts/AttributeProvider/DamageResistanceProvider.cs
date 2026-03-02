using UnityEngine;

public class DamageResistanceProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] string damageType;
    [SerializeField] int amount;
    [SerializeField] string exception;

    public void Setup(Entity entity)
    {
        IDamageResistanceSystem.Resolve().SetResistance(entity, damageType, amount);
        if (!string.IsNullOrEmpty(exception))
            IDamageResistanceExceptionSystem.Resolve().SetException(entity, damageType, exception);
    }
}